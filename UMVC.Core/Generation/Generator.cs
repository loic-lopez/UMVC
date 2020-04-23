using System.Collections.Generic;
using System.IO;
// ReSharper disable once RedundantUsingDirective
using UMVC.Core.Templates;


namespace UMVC.Core.Generation
{
  
    
    public static class Generator
    {
        public static void GenerateModel(string modelName, string namespaceName, string outputDir)
        {
            ModelTemplate model = new ModelTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            model.Session["ClassName"] = modelName;
            model.Session["Namespace"] = namespaceName;

            model.Initialize();

            string classDef = model.TransformText();

            File.WriteAllText($"{outputDir}/{modelName}.cs", classDef);
        }
    }
}