namespace FacebookAPI.Repositories
{
    public class FacebookDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;
        public string CommentsCollectionName { get; set; } = null!;
        public string PublicationsCollectionName { get; set; } = null!;
    }
}
