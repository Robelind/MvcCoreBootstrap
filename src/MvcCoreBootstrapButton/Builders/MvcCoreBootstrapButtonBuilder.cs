using System;
using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapButton.Config;
using MvcCoreBootstrapModal.Builders;
using MvcCoreBootstrapModal.Config;

namespace MvcCoreBootstrapButton.Builders
{
    public class MvcCoreBootstrapButtonBuilder : BuilderBase
    {
        private readonly ButtonConfig _config;

        internal MvcCoreBootstrapButtonBuilder(ButtonConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the table.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Id(string id)
        {
            _config.Id = id;
            return(this);
        }

        /// <summary>
        /// Sets the name attribute for the table.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Name(string name)
        {
            _config.Name = name;
            return(this);
        }

        /// <summary>
        /// Sets the button text.
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Text(string text)
        {
            _config.Text = text;
            return(this);
        }

        /// <summary>
        /// Sets the <see cref="ContextualState"/> of the button.
        /// </summary>
        /// <param name="state">Contextual state</param>
        /// <param name="condition">If true, the contextual state will be applied.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Contextual(ContextualState state, bool condition = true)
        {
            _config.State = condition ? state : ContextualState.Default;
            return(this);
        }

        /// <summary>
        /// Sets the button size.
        /// </summary>
        /// <param name="size">Size</param>
        /// <param name="condition">If true, the size will be applied.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Size(MvcCoreBootstrapButtonSize size, bool condition = true)
        {
            _config.Size = condition ? size : MvcCoreBootstrapButtonSize.Default;
            return(this);
        }

        /// <summary>
        /// Sets the button to block level.
        /// </summary>
        /// <param name="condition">If true, the button will be block level.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Block(bool condition = true)
        {
            _config.Block = condition;
            return(this);
        }

        /// <summary>
        /// Sets the button to active.
        /// </summary>
        /// <param name="condition">If true, the button will be set to active.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Active(bool condition = true)
        {
            _config.Active = condition;
            return(this);
        }

        /// <summary>
        /// Sets the button to disabled.
        /// </summary>
        /// <param name="condition">If true, the button will be set to disabled.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Disabled(bool condition = true)
        {
            _config.Disabled = condition;
            return(this);
        }

        /// <summary>
        /// Adds a badge to the button.
        /// </summary>
        /// <param name="text">Badge text.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Badge(string text)
        {
            return(this.SetConfigProp<MvcCoreBootstrapButtonBuilder>(() => _config.Badge = text));
        }

        /// <summary>
        /// Renders a submit button.
        /// </summary>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Submit()
        {
            if(_config.Url != null)
            {
                throw(new InvalidOperationException("Not applicable when button is navigational"));
            }
            if(_config.Ajax != null)
            {
                throw(new InvalidOperationException("Not applicable when button is AJAX"));
            }
            _config.Submit = true;
            return(this);
        }

        /// <summary>
        /// Configures a java script function to be called the button is clicked.
        /// </summary>
        /// <param name="jsFunc">Name of java script function.</param>
        /// <param name="condition">If true, the java script function will be called.</param>
        /// <returns>The button builder instance.</returns>
        /// <remarks>The java script function will receive the buttons id as a parameter.</remarks>
        public MvcCoreBootstrapButtonBuilder Click(string jsFunc, bool condition = true)
        {
            if(condition)
            {
                _config.Click = jsFunc;
            }
            return(this);
        }

        /// <summary>
        /// Renders the button for navigation.
        /// </summary>
        /// <param name="url">Url to navigate to.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Navigate(string url)
        {
            if(_config.Submit)
            {
                throw(new InvalidOperationException("Not applicable when button is submit"));
            }
            if(_config.Ajax != null)
            {
                throw(new InvalidOperationException("Not applicable when button is AJAX"));
            }
            if(url == null)
            {
                throw(new ArgumentNullException(nameof(url)));
            }
            _config.Url = url;
            return(this);
        }

        /// <summary>
        /// Sets a css class for the table element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the button element.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder CssClass(string cssClass, bool condition = true)
        {
            if(condition)
            {
                _config.CssClasses.Add(cssClass);
            }
            return(this);
        }

        /// <summary>
        /// Configures the buttons AJAX behavior.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Ajax(Action<MvcCoreBootstrapButtonAjaxBuilder> configAction)
        {
            if(_config.Submit)
            {
                throw(new InvalidOperationException("Not applicable when button is submit"));
            }
            if(_config.Url != null)
            {
                throw(new InvalidOperationException("Not applicable when button is navigational"));
            }
            if(configAction == null)
            {
                throw(new ArgumentNullException(nameof(configAction)));
            }
            _config.Ajax = new AjaxConfig();
            configAction(new MvcCoreBootstrapButtonAjaxBuilder(_config.Ajax));

            return(this);
        }

        /// <summary>
        /// Configures the buttons AJAX behavior.
        /// </summary>
        /// <param name="configAction">Configuration action</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder Dropdown(Action<MvcCoreBootstrapButtonDropdownBuilder> configAction)
        {
            if(_config.Submit)
            {
                throw(new InvalidOperationException("Not applicable when button is submit"));
            }
            /*if(_config.Url != null)
            {
                throw(new InvalidOperationException("Not applicable when button is link"));
            }
            if(_config.Ajax != null)
            {
                throw(new InvalidOperationException("Not applicable when button has AJAX functionality"));
            }*/
            if(configAction == null)
            {
                throw(new ArgumentNullException(nameof(configAction)));
            }
            _config.Dropdown = new DropdownConfig();
            configAction(new MvcCoreBootstrapButtonDropdownBuilder(_config.Dropdown));

            return(this);
        }

        /// <summary>
        /// Configures the button to trigger display of a modal.
        /// </summary>
        /// <param name="configAction">Action that implements modal configuration.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder TriggerModal(Action<MvcCoreBootstrapModalBuilder> configAction)
        {
            _config.Modal = new ModalConfig();
            configAction(new MvcCoreBootstrapModalBuilder(_config.Modal));

            return(this);
        }

        /// <summary>
        /// Configures the button to trigger display of a modal.
        /// </summary>
        /// <param name="id">Id of a modal.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder TriggerModal(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapButtonBuilder>(() => _config.ModalId = id));
        }

        //Problem: The modal created by this API needs to be rendered outside of the button.
        /// <summary>
        /// Configures the button to trigger display of a modal.
        /// </summary>
        /// <param name="configAction">Action that implements modal configuration.</param>
        /// <returns>The button builder instance.</returns>
        //public MvcCoreBootstrapButtonBuilder TriggerModal(Action<MvcCoreBootstrapModalBuilder> configAction)
        //{
        //    _config.Modal = new ModalConfig();
        //    configAction(new MvcCoreBootstrapModalBuilder(_config.Modal));

        //    return(this);
        //}

        /// <summary>
        /// Configures the button to trigger display of a modal.
        /// </summary>
        /// <param name="id">Id of a modal.</param>
        /// <returns>The button builder instance.</returns>
        public MvcCoreBootstrapButtonBuilder TriggerModal(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapButtonBuilder>(() => _config.ModalId = id, nameof(id)));
        }
    }
}