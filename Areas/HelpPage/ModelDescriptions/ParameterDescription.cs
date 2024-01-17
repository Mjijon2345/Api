using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Api.Areas.HelpPage.ModelDescriptions
{

    /// Clase que describe un parámetro.

    public class ParameterDescription
    {

        /// Inicializa una nueva instancia de la clase ParameterDescription.
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        /// Obtiene las anotaciones asociadas al parámetro.
        public Collection<ParameterAnnotation> Annotations { get; private set; }

        /// Obtiene o establece la documentación asociada al parámetro.
        public string Documentation { get; set; }

        /// Obtiene o establece el nombre del parámetro.
        public string Name { get; set; }

        /// Obtiene o establece la descripción del tipo de parámetro.
        public ModelDescription TypeDescription { get; set; }
    }
}
