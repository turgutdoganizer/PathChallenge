namespace PathProjectChallenge.Service.Infrastructure.Mapper.Models
{
    /// <summary>
    /// Represents base nopCommerce entity model
    /// </summary>
    public partial record BasePathEntityModel : BasePathModel
    {
        /// <summary>
        /// Gets or sets model identifier
        /// </summary>
        public virtual int Id { get; set; }
    }
}
