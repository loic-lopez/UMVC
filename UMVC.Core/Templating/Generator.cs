using System.Collections.Generic;
using System.IO;
using UMVC.Templating.Templates;


namespace UMVC.Templating
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

            model.Session["_ClassNameField"] = modelName;
            model.Session["_NamespaceField"] = namespaceName;

            model.Initialize();

            string classDef = model.TransformText();

            File.WriteAllText($"{outputDir}/{modelName}.cs", classDef);
        }
    }
}