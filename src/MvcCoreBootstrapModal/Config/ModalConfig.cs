using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapModal.Config
{
    internal class ModalConfig : ConfigBase
    {
        public bool Dismissable { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
