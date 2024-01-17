using System;

namespace Api.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Describe un modelo de tipo.
    /// </summary>
    public abstract class ModelDescription
    {
        /// <summary>
        /// Obtiene o establece la documentación asociada al modelo.
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Obtiene o establece el tipo del modelo.
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del modelo.
        /// </summary>
        public string Name { get; set; }
    }
}
