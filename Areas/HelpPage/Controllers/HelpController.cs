using System;
using System.Web.Http;
using System.Web.Mvc;
using Api.Areas.HelpPage.ModelDescriptions;
using Api.Areas.HelpPage.Models;

namespace Api.Areas.HelpPage.Controllers
{
    /// <summary>
    /// El controlador que manejar� las solicitudes para la p�gina de ayuda.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        // Constructor predeterminado
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        // Constructor con par�metro para la configuraci�n de la aplicaci�n
        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        // Propiedad que almacena la configuraci�n de la aplicaci�n
        public HttpConfiguration Configuration { get; private set; }

        // Acci�n para mostrar la p�gina de �ndice de ayuda
        public ActionResult Index()
        {
            // Asigna el proveedor de documentaci�n y las descripciones de la API a la vista
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        // Acci�n para mostrar la p�gina de detalles de una API espec�fica
        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                // Obtiene el modelo de ayuda para la API espec�fica
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            // Si no se encuentra la API, muestra la vista de error
            return View(ErrorViewName);
        }

        // Acci�n para mostrar la p�gina de detalles de un modelo de recurso
        public ActionResult ResourceModel(string modelName)
        {
            if (!String.IsNullOrEmpty(modelName))
            {
                // Obtiene la descripci�n del modelo a partir del generador de descripciones de modelo
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
