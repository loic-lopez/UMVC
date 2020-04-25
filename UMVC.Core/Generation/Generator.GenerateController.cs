// ReSharper disable once RedundantUsingDirective
using System.Collections.Generic;
using System.IO;
using UMVC.Core.Templates;


namespace UMVC.Core.Generation
{
    public static partial class Generator
    {
        public static void GenerateController(
            string controllerName, 
            string modelName, 
            string namespaceName,
            string extends,
            string outputDir
        )
        {
            ControllerTemplate template = new ControllerTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = controllerName;
            template.Session["Namespace"] = namespaceName;
            template.Session["Model"] = modelName;
            template.Session["Extends"] = extends;

            template.Initialize();

            string classDef = template.TransformText();

            File.WriteAllText($"{outputDir}/{controllerName}.cs", classDef);
        }
    }
}