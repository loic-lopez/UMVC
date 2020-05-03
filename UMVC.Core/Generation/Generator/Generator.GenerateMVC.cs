namespace UMVC.Core.Generation.Generator
{
    public static partial class Generator
    {
        public static void GenerateMVC(GeneratorParameters.GeneratorParameters generatorParameters)
        {
            GenerateModel(generatorParameters);
            GenerateController(generatorParameters);
            GenerateView(generatorParameters);
        }
    }
}