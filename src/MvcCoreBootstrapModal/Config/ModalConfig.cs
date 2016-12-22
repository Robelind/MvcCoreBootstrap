using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapModal.Config
{
    internal class ModalConfig : ConfigBase
    {
        public string Text { get; set; }
        public bool Dismissable { get; set; }
    }
}
