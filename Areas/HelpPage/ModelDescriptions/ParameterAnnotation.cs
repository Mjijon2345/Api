using System;

namespace Api.Areas.HelpPage.ModelDescriptions
{

    /// Clase que representa una anotación de parámetro.

    public class ParameterAnnotation
    {

        /// Obtiene o establece el atributo de anotación.

        public Attribute AnnotationAttribute { get; set; }


        /// Obtiene o establece la documentación asociada a la anotación.

        public string Documentation { get; set; }
    }
}
