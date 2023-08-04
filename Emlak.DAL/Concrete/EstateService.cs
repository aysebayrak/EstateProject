using Emlak.DAL.Abstract;
using Emlak.Entity.Concrete;
using Emlak.Entity.DBSettings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Emlak.DAL.Concrete
{
    public class EstateService : IEstateService
    {
        private readonly IMongoCollection<Estate> _estate;

        public EstateService(IDbSettings dbSettings, IMongoClient mongoClient)
        {

            var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _estate = database.GetCollection<Estate>(dbSettings.EstateCollectionName);
        }

        public Estate Create(Estate estate)
        {

            estate.EstateID = ObjectId.GenerateNewId().ToString();
            _estate.InsertOne(estate);
            return estate;
        }

        public void Delete(string id)
        {
             
            _estate.DeleteOne(estate => estate.EstateID == id);
        }

       

        public List<Estate> GetAll()
        {
            return _estate.Find(estate => true).ToList();
        }

        public List<Estate> GetByFilter(string? city, string? medicine, int? room, string? title, int? price, string? buildYear)
        {
            var filterBuilder = Builders<Estate>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(city))
            {
                filter = filter & filterBuilder.Where(estate => estate.City.ToLower().Contains(city.ToLower()));
            }

            if (!string.IsNullOrEmpty(medicine))
            {
                filter = filter & filterBuilder.Where(estate => estate.Medicine.ToLower().Contains(medicine.ToLower()));
            }

            if (room.HasValue)
            {
                filter = filter & filterBuilder.Eq(estate => estate.Room, room.Value);
            }

            if (!string.IsNullOrEmpty(title))
            {
                filter = filter & filterBuilder.Where(estate => estate.Title.ToLower().Contains(title.ToLower()));
            }

            if (price.HasValue)
            {
                filter = filter & filterBuilder.Eq(estate => estate.Price, price.Value);
            }

            if (!string.IsNullOrEmpty(buildYear))
            {
                filter = filter & filterBuilder.Eq(estate => estate.Buildyear, buildYear);
            }

            return _estate.Find(filter).ToList();
        }

        public Estate GetById(string id)
        {
            return _estate.Find(estate => estate.EstateID == id).FirstOrDefault();
        }

        public void Update(string id, Estate estate)
        {
            
            _estate.ReplaceOne(estate => estate.EstateID == id, estate);
        }
    }
}
