using MvcCoreBootstrap;

namespace MvcCoreBootstrapListGroup.Config
{
    internal class ListGroupItem
    {
        public ListGroupItem()
        {
            State = ContextualState.Default;
        }

        public string Content { get; set; }
        public ContextualState State { get; set; }
        public string Badge { get; set; }
        public string Url { get; set; }
        public bool Button { get; set; }
        public string Id { get; set; }
        public string ClickHandler { get; set; }
        public bool Disabled { get; set; }
        public AjaxConfig Ajax { get; set; }
    }
}
