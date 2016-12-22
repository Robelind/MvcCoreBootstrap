using MvcCoreBootstrap;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapModal.Config
{
    internal class ModalConfig : ConfigBase
    {
        public ModalConfig()
        {
            Animation = true;
        }

        public bool Dismissable { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string CloseBtnText { get; set; }
        public ContextualState CloseBtnState { get; set; }
        public bool Animation { get; set; }
    }
}
