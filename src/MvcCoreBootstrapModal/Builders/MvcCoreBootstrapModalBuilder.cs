using System;
using Microsoft.AspNetCore.Html;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapModal.Builders
{
    public class MvcCoreBootstrapModalBuilder : BuilderBase
    {
        private readonly ModalConfig _config;

        internal MvcCoreBootstrapModalBuilder(ModalConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the modal.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the modal.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Configures the modal to not use animation when displayed.
        /// </summary>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder NoAnimation()
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Animation = false));
        }

        /// <summary>
        /// Sets the size of the modal.
        /// </summary>
        /// <param name="size">Modal size</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Size(MvcCoreBootstrapModalSize size)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Size = size));
        }

        /// <summary>
        /// Configures the modal header.
        /// </summary>
        /// <param name="configAction">Action that implements modal header configuration.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Header(Action<MvcCoreBootstrapModalHeaderBuilder> configAction)
        {
            configAction(new MvcCoreBootstrapModalHeaderBuilder(_config));
            return(this);
        }

        /// <summary>
        /// Sets the body of the modal.
        /// </summary>
        /// <param name="body">Body content.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Body(string body)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Body = body));
        }

        /// <summary>
        /// Sets the body of the modal.
        /// </summary>
        /// <param name="body">Body content.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Body(IHtmlContent body)
        {
            return (this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.BodyHtml = body));
        }

        /// <summary>
        /// Configures the modal footer.
        /// </summary>
        /// <param name="configAction">Action that implements footer configuration.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Footer(Action<MvcCoreBootstrapModalFooterBuilder> configAction)
        {
            configAction(new MvcCoreBootstrapModalFooterBuilder(_config));
            return(this);
        }

        /// <summary>
        /// Sets whether the modal header will contain a means for dismissing the modal.
        /// </summary>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Dismissible()
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.Dismissable = true));
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the modal.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The modal builder instance.</returns>
        public MvcCoreBootstrapModalBuilder Contextual(ContextualState state, bool condition = true)
        {
            return(this.SetConfigProp<MvcCoreBootstrapModalBuilder>(() => _config.State = condition ? state : ContextualState.Default));
        }

        /// <summary>
        /// Configures a basic modal with title, body and a close button.
        /// </summary>
        /// <param name="title">Modal title.</param>
        /// <param name="body">Modal body.</param>
        /// <param name="closeBtnText">Close button text.</param>
        /// <param name="closeBtnState">Close button contextual state.</param>
        public void Modal(string title, string body, string closeBtnText,
            ContextualState closeBtnState = ContextualState.Default)
        {
            _config.Title = title;
            _config.Body = body;
            _config.Buttons.Add(new ModalButton { Text = closeBtnText, State = closeBtnState });
            _config.Dismissable = true;
        }
    }
}