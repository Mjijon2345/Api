using System;
using System.Web.Http;
using System.Web.Mvc;
using Api.Areas.HelpPage.ModelDescriptions;
using Api.Areas.HelpPage.Models;

namespace Api.Areas.HelpPage.Controllers
{
    /// <summary>
    /// El controlador que manejará las solicitudes para la página de ayuda.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        // Constructor predeterminado
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        // Constructor con parámetro para la configuración de la aplicación
        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        // Propiedad que almacena la configuración de la aplicación
        public HttpConfiguration Configuration { get; private set; }

        // Acción para mostrar la página de índice de ayuda
        public ActionResult Index()
        {
            // Asigna el proveedor de documentación y las descripciones de la API a la vista
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        // Acción para mostrar la página de detalles de una API específica
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                // Obtiene el modelo de ayuda para la API específica
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            // Si no se encuentra la API, muestra la vista de error
            return View(ErrorViewName);
        }

        // Acción para mostrar la página de detalles de un modelo de recurso
        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                // Obtiene la descripción del modelo a partir del generador de descripciones de modelo
                ModelDescriptionGenerator modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return View(modelDescription);
                }
            }

            // Si no se encuentra el modelo, muestra la vista de error
            return View(ErrorViewName);
        }
    }
}
