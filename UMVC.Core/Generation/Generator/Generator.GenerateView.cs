using System.Collections.Generic;
using System.IO;
// ReSharper disable once RedundantUsingDirective
using UMVC.Core.Templates;

namespace UMVC.Core.Generation.Generator
{
    public static partial class Generator
    {
        public static void GenerateView(GeneratorParameters.GeneratorParameters generatorParameters)
        {
            ViewTemplate template = new ViewTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = generatorParameters.View.Name;
            template.Session["Namespace"] = generatorParameters.NamespaceName;
            template.Session["Model"] = generatorParameters.Model.Name;
            template.Session["Controller"] = generatorParameters.Controller.Name;
            template.Session["Extends"] = generatorParameters.View.Extends;
            template.Session["BaseNamespace"] = generatorParameters.View.BaseNamespace;

            template.Initialize();

            string classDef = template.TransformText();

            File.WriteAllText($"{generatorParameters.OutputDir}/{generatorParameters.View.Name}.cs", classDef);
        }
    }
}