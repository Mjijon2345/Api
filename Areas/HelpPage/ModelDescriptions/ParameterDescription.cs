using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Api.Areas.HelpPage.ModelDescriptions
{

    /// Clase que describe un par�metro.

    public class ParameterDescription
    {

        /// Inicializa una nueva instancia de la clase ParameterDescription.
        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        /// Obtiene las anotaciones asociadas al par�metro.
        public Collection<ParameterAnnotation> Annotations { get; private set; }

        /// Obtiene o establece la documentaci�n asociada al par�metro.
        public string Documentation { get; set; }

        /// Obtiene o establece el nombre del par�metro.
        public string Name { get; set; }

        /// Obtiene o establece la descripci�n del tipo de par�metro.
        public ModelDescription TypeDescription { get; set; }
    }
}
