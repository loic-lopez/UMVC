using System.Collections.Generic;
using System.IO;
using CodeGeneration.Templates;
using UMVC.Templating.Templates;

namespace UMVC.Templating
{
    /// <summary>
    /// The partial keywords tells the compiler to combine this class and Layers
    /// Generator into when the project gets compiled. 
    /// </summary>
    public static class LayersGeneratorInterface
    {
        public static void GenerateLayers()
        {

            //Create a new instance of our generator. 
            LayersGenerator generator = new LayersGenerator
            {
                //Create our session.
                Session = new Dictionary<string, object>()
            };


            //Get the class name
            string className = Path.GetFileNameWithoutExtension(outputPath);

            //Save it to our session. 
            generator.Session["m_ClassName"] = className;

            //Grab all our layers from Unity. 
            List<string> layers = new List<string>(InternalEditorUtility.layers);
    
            //Add our layers to our generator. 
            generator.Session["m_UnityLayers"] = layers.ToArray();

            //Initialize the template (loads the values from the session into the template)
            generator.Initialize();

            //Generate the definition
            string classDef = generator.TransformText();

            //Write the class to disk
            File.WriteAllText(outputPath, classDef);
    
        }

    }
}


