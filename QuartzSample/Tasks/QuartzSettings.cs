namespace QuartzSample.Tasks
{

    public class QuartzSettings
    {
        public const string QuartzConfig = "QuartzConfig";
        public string JobName { get; set; } = string.Empty;
        public bool TaskIsActive { get; set; }
        public string Cron { get; set; } = string.Empty;
    }

}
