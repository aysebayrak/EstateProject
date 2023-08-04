namespace Emlak.Entity.DBSettings
{
    public interface IDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    
       public string EstateCollectionName { get; set; }
        public string UserCollectionName { get; set; }
    }
}
