using System;

namespace Api.Areas.HelpPage.ModelDescriptions
{

    /// Clase que representa una anotaci�n de par�metro.

    public class ParameterAnnotation
    {

        /// Obtiene o establece el atributo de anotaci�n.

        public Attribute AnnotationAttribute { get; set; }


        /// Obtiene o establece la documentaci�n asociada a la anotaci�n.

        public string Documentation { get; set; }
    }
}
