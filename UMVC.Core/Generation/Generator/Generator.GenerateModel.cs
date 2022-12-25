// ReSharper disable once RedundantUsingDirective
using System.Collections.Generic;
using System.IO;
using UMVC.Core.Templates;

namespace UMVC.Core.Generation.Generator
{
    public static partial class Generator
    {
        public static void GenerateModel(GeneratorParameters.GeneratorParameters generatorParameters)
        {
            ModelTemplate template = new()
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };

            template.Session["ClassName"] = generatorParameters.Model.Name;
            template.Session["Namespace"] = generatorParameters.NamespaceName;
            template.Session["Extends"] = generatorParameters.Model.Extends;
            template.Session["BaseNamespace"] = generatorParameters.Model.BaseNamespace;
            template.Session["Fields"] = generatorParameters.Model.Fields;

            template.Initialize();

            File.WriteAllText($"{generatorParameters.OutputDir}/{generatorParameters.Model.Name}.cs",
                template.TransformText());
        }
    }
}