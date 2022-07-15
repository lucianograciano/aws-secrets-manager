namespace aws_secrets_manager
{
    public class DatabaseSettings
    {
        public const string SectionName = "Database";
        public string? ConnectionString { get; set; }

    }
}
