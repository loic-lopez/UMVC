// ReSharper disable once RedundantUsingDirective
using System.Collections.Generic;
using System.IO;
using UMVC.Core.Templates;


namespace UMVC.Core.Generation
{
    public static partial class Generator
    {
        public static void GenerateView(
            string viewName,
            string controllerName, 
            string modelName, 
            string namespaceName,
            string extends,
            string outputDir
        )
        {
            ViewTemplate template = new ViewTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = viewName;
            template.Session["Namespace"] = namespaceName;
            template.Session["Model"] = modelName;
            template.Session["Controller"] = controllerName;
            template.Session["Extends"] = extends;

            template.Initialize();

            string classDef = template.TransformText();

            File.WriteAllText($"{outputDir}/{viewName}.cs", classDef);
        }
    }
}