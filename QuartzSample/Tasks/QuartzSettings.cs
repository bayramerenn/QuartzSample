namespace QuartzSample.Tasks
{

    public class QuartzSettings
    {
        
        public string JobName { get; set; } = string.Empty;
        public bool TaskIsActive { get; set; }
        public string Cron { get; set; } = string.Empty;
    }

}
