using MvcCoreBootstrap;
using MvcCoreBootstrap.Config;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapButton.Config
{
    internal class ButtonConfig : TooltipConfigBase
    {
        public ButtonConfig()
        {
            Size = MvcCoreBootstrapButtonSize.Default;
            State = ContextualState.Primary;
        }

        public string Text { get; set; }
        public MvcCoreBootstrapButtonSize Size { get; set; }
        public bool Block { get; set; }
        public bool Active { get; set; }
        public bool Disabled { get; set; }
        public bool Submit { get; set; }
        public string Click { get; set; }
        public string Url { get; set; }
        public AjaxConfig Ajax { get; set; }
        public DropdownConfig Dropdown { get; set; }
        public string Badge { get; set; }
        public bool Outline { get; set; }
        public ModalConfig Modal { get; set; }
        public string ModalId { get; set; }
        public string CollapseId { get; set; }
    }
}
