using UMVC.Core.Components;

namespace UMVC.Core.Generation.GeneratorParameters
{
    public class GeneratorParameters
    {
        public Component View { get; private set; }
        public ModelComponent Model { get; private set; }
        public Component Controller { get; private set; }
        public string NamespaceName { get; private set; }
        public string OutputDir { get; private set; }

        public class Builder
        {

            private Component _view;
            private ModelComponent _model;
            private Component _controller;
            private string _namespaceName;
            private string _outputDir;

            public Builder WithView(Component view)
            {
                _view = view;
                return this;
            }

            public Builder WithController(Component controller)
            {
                _controller = controller;
                return this;
            }
            
            public Builder WithModel(ModelComponent model)
            {
                _model = model;
                return this;
            }

            public Builder WithNamespaceName(string value)
            {
                _namespaceName = value;
                return this;
            }
            
            public Builder WithOutputDir(string value)
            {
                _outputDir = value;
                return this;
            }
        
            public GeneratorParameters Build()
            {
                return new GeneratorParameters
                {
                    OutputDir = _outputDir,
                    NamespaceName = _namespaceName,
                    View = _view,
                    Controller = _controller,
                    Model = _model
                };
            }
        }
    }
}