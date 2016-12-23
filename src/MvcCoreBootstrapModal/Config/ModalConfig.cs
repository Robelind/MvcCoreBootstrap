using System.Collections.Generic;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Config;

namespace MvcCoreBootstrapModal.Config
{
    internal class ModalButton
    {
        public string Text { get; set; }
        public ContextualState State { get; set; }
        public string JsFunc { get; set; }
        //public ButtonConfig Config { get; set; }
    }

    internal class ModalConfig : ConfigBase
    {
        public ModalConfig()
        {
            Animation = true;
            Size = MvcCoreBootstrapModalSize.Default;
            Buttons = new List<ModalButton>();
        }

        public bool Dismissable { get; set; }
        public bool Animation { get; set; }
        public MvcCoreBootstrapModalSize Size { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IList<ModalButton> Buttons { get; set; } 
    }
}
