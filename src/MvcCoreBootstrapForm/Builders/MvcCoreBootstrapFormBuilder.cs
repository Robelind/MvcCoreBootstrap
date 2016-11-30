﻿using MvcCoreBootstrap.Building;
using MvcCoreBootstrapForm.Config;

namespace MvcCoreBootstrapForm.Builders
{
    public class MvcCoreBootstrapFormBuilder : BuilderBase
    {
        private readonly FormConfig _config;

        internal MvcCoreBootstrapFormBuilder(FormConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the form.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the form.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Configures the form type.
        /// </summary>
        /// <param name="type">Form type.</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Type(FormType type)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() => _config.Type = type));
        }

        /// <summary>
        /// Configures the form for horizontal layout.
        /// </summary>
        /// <param name="type">Form type.</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder Horizontal(ColumnWidth left, ColumnWidth right)
        {
            return(this.SetConfigProp<MvcCoreBootstrapFormBuilder>(() =>
                _config.ColumnWidths = new ColumnWidths {LeftColumn = left, RightColumn = right}));
        }

        /// <summary>
        /// Sets a css class for the form.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the form element.</param>
        /// <returns>The form builder instance.</returns>
        public MvcCoreBootstrapFormBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapFormBuilder>(_config.CssClasses, cssClass, condition));
        }
    }
}