using System.Collections.Generic;
using System.IO;
// ReSharper disable once RedundantUsingDirective
using UMVC.Core.Templates;

namespace UMVC.Core.Generation.Generator
{
    public static partial class Generator
    {
        public static void GenerateModel(GeneratorParameters.GeneratorParameters generatorParameters)
        {
            ModelTemplate template = new ModelTemplate
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = generatorParameters.Model.Name;
            template.Session["Namespace"] = generatorParameters.NamespaceName;
            template.Session["Extends"] = generatorParameters.Model.Extends;
            template.Session["BaseNamespace"] = generatorParameters.Model.BaseNamespace;

            template.Initialize();

            string classDef = template.TransformText();

            File.WriteAllText($"{generatorParameters.OutputDir}/{generatorParameters.Model.Name}.cs", classDef);
        }
    }
}