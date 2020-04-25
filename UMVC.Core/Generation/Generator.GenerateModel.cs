// ReSharper disable once RedundantUsingDirective
using System.Collections.Generic;
using System.IO;
using UMVC.Core.Templates;


namespace UMVC.Core.Generation
{
    public static partial class Generator
    {
        public static void GenerateModel(string modelName, string namespaceName, string extends, string outputDir)
        {
            ModelTemplate template = new ModelTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = modelName;
            template.Session["Namespace"] = namespaceName;
            template.Session["Extends"] = extends;

            template.Initialize();

            string classDef = template.TransformText();

            File.WriteAllText($"{outputDir}/{modelName}.cs", classDef);
        }
    }
}