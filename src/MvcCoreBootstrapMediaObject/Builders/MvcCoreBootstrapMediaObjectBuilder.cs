using MvcCoreBootstrap;
using MvcCoreBootstrap.Building;
using MvcCoreBootstrapMediaObject.Config;

namespace MvcCoreBootstrapMediaObject.Builders
{
    public class MvcCoreBootstrapMediaObjectBuilder : BuilderBase
    {
        private readonly MediaObjectConfig _config;

        internal MvcCoreBootstrapMediaObjectBuilder(MediaObjectConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Sets the id attribute for the media object.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>The media object builder instance.</returns>
        public MvcCoreBootstrapMediaObjectBuilder Id(string id)
        {
            return(this.SetConfigProp<MvcCoreBootstrapMediaObjectBuilder>(() => _config.Id = id));
        }

        /// <summary>
        /// Sets the name attribute for the media object.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>The media object builder instance.</returns>
        public MvcCoreBootstrapMediaObjectBuilder Name(string name)
        {
            return(this.SetConfigProp<MvcCoreBootstrapMediaObjectBuilder>(() => _config.Name = name));
        }

        /// <summary>
        /// Sets a css class for the media object element.
        /// </summary>
        /// <param name="cssClass">Name of css class.</param>
        /// <param name="condition">If true, the css class will be set for the media object element.</param>
        /// <returns>The media object builder instance.</returns>
        public MvcCoreBootstrapMediaObjectBuilder CssClass(string cssClass, bool condition = true)
        {
            return(this.AddCssClass<MvcCoreBootstrapMediaObjectBuilder>(_config.CssClasses, cssClass, condition));
        }

        /// <summary>
        /// Sets the id attribute for the media object.
        /// </summary>
        /// <returns>The media object builder instance.</returns>
        public MvcCoreBootstrapMediaObjectBuilder Image(string path, VerticalAlignment alignment = VerticalAlignment.Top, string alt = null)
        {
            return(this.SetConfigProp<MvcCoreBootstrapMediaObjectBuilder>(() =>
            {
                _config.ImagePath = path;
                _config.ImageAlignment = alignment;
                _config.ImageAlt = alt;
            }));
        }
    }
}