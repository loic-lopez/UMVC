using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using UMVC.Core.Generation;
using UMVC.Core.MVC;

namespace Tests
{
    public static class TestsUtils
    {
        private static object CompileInRAMGeneratedClass(
            string generatedFile,
            string desiredClass,
            IReadOnlyCollection<Type> additionalAssemblies = null
        )
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(generatedFile));

            return CompileFiles(new[] {syntaxTree}, desiredClass, additionalAssemblies);
        }

        private static object CompileInRAMGeneratedClasses(
            IEnumerable<string> generatedFiles,
            string desiredClass,
            IReadOnlyCollection<Type> additionalAssemblies = null
        )
        {
            var syntaxTreeList = generatedFiles
                .Select(generatedFile => CSharpSyntaxTree.ParseText(File.ReadAllText(generatedFile))).ToList();


            return CompileFiles(syntaxTreeList, desiredClass, additionalAssemblies);
        }

        private static object CompileFiles(
            IEnumerable<SyntaxTree> syntaxTreeList,
            string desiredClass,
            IReadOnlyCollection<Type> additionalAssemblies = null
        )
        {
            // define other necessary objects for compilation
            var assemblyName = Path.GetRandomFileName();
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            if (additionalAssemblies != null)
                foreach (var typeToCompile in additionalAssemblies)
                    if (typeToCompile.Assembly.Location != "")
                        references.Add(MetadataReference.CreateFromFile(typeToCompile.Assembly.Location));

            // analyse and generate IL code from syntax tree
            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTreeList,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                // write IL code into memory
                var result = compilation.Emit(ms);

                if (!result.Success)
                {
                    // handle exceptions
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (var diagnostic in failures)
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());

                    Assert.Fail("See compiler errors!");
                }
                else
                {
                    // load this 'virtual' DLL so that we can use
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = Assembly.Load(ms.ToArray());

                    // create instance of the desired class and call the desired function
                    var type = assembly.GetType(desiredClass);
                    return Activator.CreateInstance(type);
                }
            }

            return null;
        }


        public static object GenerateModel(string modelName, string namespaceName, string currentDir)
        {
            // Generate Model
            Generator.GenerateModel(modelName, namespaceName, currentDir);
            var generatedFile = currentDir + $"/{modelName}.cs";
            var desiredClass = $"{namespaceName}.{modelName}";
            Assert.IsTrue(File.Exists(generatedFile));

            var additionalTypesToCompile = new List<Type> {typeof(BaseModel)};

            return CompileInRAMGeneratedClass(generatedFile, desiredClass, additionalTypesToCompile);
        }

        public static object GenerateController(
            string controllerName,
            string modelName,
            string namespaceName,
            string currentDir
        )
        {
            Generator.GenerateController(controllerName, modelName, namespaceName, currentDir);
            var generatedFile = currentDir + $"/{controllerName}.cs";
            var desiredClass = $"{namespaceName}.{controllerName}";
            Assert.IsTrue(File.Exists(generatedFile));

            // Generate Model
            Generator.GenerateModel(modelName, namespaceName, currentDir);
            var generatedModelFile = currentDir + $"/{modelName}.cs";

            var additionalTypesToCompile = new List<Type> {typeof(BaseModel)};

            return CompileInRAMGeneratedClasses(
                new[] {generatedFile, generatedModelFile},
                desiredClass,
                additionalTypesToCompile
            );
        }
    }
}