namespace Emlak.Entity.DBSettings
{
    public class DbSettings : IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
      
        public string EstateCollectionName { get; set; }
        public string UserCollectionName { get ; set; }

        //public string CustomerCollectionName { get; set; } = string.Empty;

    }
}

