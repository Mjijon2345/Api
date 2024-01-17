using System;

namespace Api.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Utilice este atributo para cambiar el nombre de la <see cref="ModelDescription"/> generada para un tipo.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase ModelNameAttribute con el nombre proporcionado.
        /// </summary>
        /// <param name="name">El nuevo nombre de la descripción del modelo.</param>
        public ModelNameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Obtiene el nombre de la descripción del modelo.
        /// </summary>
        public string Name { get; private set; }
    }
}
