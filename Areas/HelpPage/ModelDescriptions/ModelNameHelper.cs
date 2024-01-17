using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Api.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Clase de ayuda interna para obtener nombres de modelo personalizados.
    /// </summary>
    internal static class ModelNameHelper
    {
        // Modificar este m�todo para proporcionar un mapeo personalizado de nombres de modelo.
        public static string GetModelName(Type type)
        {
            // Obtiene el atributo ModelNameAttribute del tipo, si existe.
            ModelNameAttribute modelNameAttribute = type.GetCustomAttribute<ModelNameAttribute>();
            if (modelNameAttribute != null && !String.IsNullOrEmpty(modelNameAttribute.Name))
            {
                return modelNameAttribute.Name;
            }

            // Si no hay atributo, utiliza el nombre del tipo como nombre de modelo.
            string modelName = type.Name;

            // Si el tipo es gen�rico, formatea el nombre del tipo gen�rico.
            if (type.IsGenericType)
            {
                // Obtiene el tipo gen�rico y sus argumentos.
                Type genericType = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string genericTypeName = genericType.Name;

                // Elimina los conteos de par�metros gen�ricos del nombre.
                genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));

                // Obtiene los nombres de los argumentos de tipo.
                string[] argumentTypeNames = genericArguments.Select(t => GetModelName(t)).ToArray();

                // Formatea el nombre del modelo para tipos gen�ricos.
                modelName = String.Format(CultureInfo.InvariantCulture, "{0}Of{1}", genericTypeName, String.Join("And", argumentTypeNames));
            }

            return modelName;
        }
    }
}
