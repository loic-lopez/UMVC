using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using NUnit.Framework;
using UMVC.Core.Templating;

namespace Tests
{
    [TestFixture]
    public class GeneratorTests
    {
        [Test]
        public void TestIfModelAsBeenGenerated()
        {
            var currentDir = Directory.GetCurrentDirectory();
            var namespaceName = "TestNamespace";
            var modelName = "TestModel";
            
            Generator.GenerateModel(modelName, namespaceName, currentDir);
            var generatedFile = currentDir + "/TestModel.cs";
            Assert.IsTrue(File.Exists(generatedFile));
            
            //SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(generatedFile));
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(generatedFile));
            
            // define other necessary objects for compilation
            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            // analyse and generate IL code from syntax tree
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                // write IL code into memory
                EmitResult result = compilation.Emit(ms);
                
                if (!result.Success)
                {
                    // handle exceptions
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic => 
                        diagnostic.IsWarningAsError || 
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                    Assert.Fail("See compiler errors!");
                }
                else
                {
                    // load this 'virtual' DLL so that we can use
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    var desiredClass = $"{namespaceName}.{modelName}";
                    // create instance of the desired class and call the desired function
                    Type type = assembly.GetType(desiredClass);
                    object obj = Activator.CreateInstance(type);
                    Console.WriteLine(obj);
                }
            }

        }
    }
}