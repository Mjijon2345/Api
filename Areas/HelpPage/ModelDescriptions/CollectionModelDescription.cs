namespace Api.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Descripción de modelo para colecciones.
    /// Extiende la clase base ModelDescription.
    /// </summary>
    public class CollectionModelDescription : ModelDescription
    {
        /// <summary>
        /// Obtiene o establece la descripción del elemento en la colección.
        /// </summary>
        public ModelDescription ElementDescription { get; set; }
    }
}
