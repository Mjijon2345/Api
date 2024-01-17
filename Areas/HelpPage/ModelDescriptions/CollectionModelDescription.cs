namespace Api.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Descripci�n de modelo para colecciones.
    /// Extiende la clase base ModelDescription.
    /// </summary>
    public class CollectionModelDescription : ModelDescription
    {
        /// <summary>
        /// Obtiene o establece la descripci�n del elemento en la colecci�n.
        /// </summary>
        public ModelDescription ElementDescription { get; set; }
    }
}
