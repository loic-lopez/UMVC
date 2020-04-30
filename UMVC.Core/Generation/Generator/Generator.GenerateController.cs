using System.Collections.Generic;
using System.IO;
// ReSharper disable once RedundantUsingDirective
using UMVC.Core.Templates;


namespace UMVC.Core.Generation.Generator
{
    public static partial class Generator
    {
        public static void GenerateController(GeneratorParameters.GeneratorParameters generatorParameters)
        {
            ControllerTemplate template = new ControllerTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = generatorParameters.Controller.Name;
            template.Session["Namespace"] = generatorParameters.NamespaceName;
            template.Session["Model"] = generatorParameters.Model.Name;
            template.Session["Extends"] = generatorParameters.Controller.Extends;
            template.Session["BaseNamespace"] = generatorParameters.Controller.BaseNamespace;

            template.Initialize();

            string classDef = template.TransformText();

            File.WriteAllText($"{generatorParameters.OutputDir}/{generatorParameters.Controller.Name}.cs", classDef);
        }
    }
}