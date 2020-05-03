using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using UMVC.Core.Generation;
using UMVC.Core.Generation.Generator;
using UMVC.Core.Generation.GeneratorParameters;
using UMVC.Core.MVC;
using UnityEngine;

namespace UMVC.Core.Tests
{
    public static class TestsUtils
    {
        private const string DefaultBaseNamespace = "UMVC.Core.MVC";
        
        private static Type CompileInRAMGeneratedClass(
            string generatedFile,
            string desiredClass,
            IReadOnlyCollection<Type> additionalAssemblies = null
        )
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(generatedFile));

            return CompileFiles(new[] {syntaxTree}, desiredClass, additionalAssemblies);
        }

        private static Type CompileInRAMGeneratedClasses(
            IEnumerable<string> generatedFiles,
            string desiredClass,
            IReadOnlyCollection<Type> additionalAssemblies = null
        )
        {
            var syntaxTreeList = generatedFiles
                .Select(generatedFile => CSharpSyntaxTree.ParseText(File.ReadAllText(generatedFile))).ToList();


            return CompileFiles(syntaxTreeList, desiredClass, additionalAssemblies);
        }

        private static Type CompileFiles(
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
                    return assembly.GetType(desiredClass);
                }
            }

            return null;
        }


        public static object GenerateModel(GeneratorParameters generatorParameters)
        {
            // Generate Model
            Generator.GenerateModel(generatorParameters);
            var generatedFile = generatorParameters.OutputDir + $"/{generatorParameters.Model.Name}.cs";
            var desiredClass = $"{generatorParameters.NamespaceName}.{generatorParameters.Model.Name}";
            Assert.IsTrue(File.Exists(generatedFile));

            var additionalTypesToCompile = new List<Type> {typeof(BaseModel)};

            return Activator.CreateInstance(CompileInRAMGeneratedClass(generatedFile, desiredClass, additionalTypesToCompile));
        }

        public static object GenerateController(GeneratorParameters generatorParameters)
        {
            Generator.GenerateController(generatorParameters);
            var generatedFile = generatorParameters.OutputDir + $"/{generatorParameters.Controller.Name}.cs";
            var desiredClass = $"{generatorParameters.NamespaceName}.{generatorParameters.Controller.Name}";
            Assert.IsTrue(File.Exists(generatedFile));

            // Generate Model
            Generator.GenerateModel(generatorParameters);
            var generatedModelFile = generatorParameters.OutputDir + $"/{generatorParameters.Model.Name}.cs";

            var additionalTypesToCompile = new List<Type> {typeof(BaseModel)};

            return Activator.CreateInstance(CompileInRAMGeneratedClasses(
                new[] {generatedFile, generatedModelFile},
                desiredClass,
                additionalTypesToCompile
            ));
        }
        
        public static Type GenerateView(GeneratorParameters generatorParameters)
        {
            // Generate View
            Generator.GenerateView(generatorParameters);
            var generatedFile = generatorParameters.OutputDir + $"/{generatorParameters.View.Name}.cs";
            var desiredClass = $"{generatorParameters.NamespaceName}.{generatorParameters.View.Name}";
            Assert.IsTrue(File.Exists(generatedFile));
            
            // Generate controller
            Generator.GenerateController(generatorParameters);
            var generatedControllerFile = generatorParameters.OutputDir + $"/{generatorParameters.Controller.Name}.cs";

            // Generate Model
            Generator.GenerateModel(generatorParameters);
            var generatedModelFile = generatorParameters.OutputDir + $"/{generatorParameters.Model.Name}.cs";

            var additionalTypesToCompile = new List<Type> {typeof(BaseModel), typeof(MonoBehaviour)};

            return CompileInRAMGeneratedClasses(
                new[] {generatedFile, generatedModelFile, generatedControllerFile},
                desiredClass,
                additionalTypesToCompile
            );
        }
    }
}