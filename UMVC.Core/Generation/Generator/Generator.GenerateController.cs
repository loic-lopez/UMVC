// ReSharper disable once RedundantUsingDirective
using System.Collections.Generic;
using System.IO;
using UMVC.Core.Templates;


namespace UMVC.Core.Generation.Generator
{
    public static partial class Generator
    {
        public static void GenerateController(GeneratorParameters.GeneratorParameters generatorParameters)
        {
            ControllerTemplate template = new()
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

            File.WriteAllText($"{generatorParameters.OutputDir}/{generatorParameters.Controller.Name}.cs",
                template.TransformText());
        }
    }
}