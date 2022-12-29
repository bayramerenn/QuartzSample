namespace QuartzSample.Models
{
    public class PropertyOwner
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? CreatedOn { get; set; }
    }
}