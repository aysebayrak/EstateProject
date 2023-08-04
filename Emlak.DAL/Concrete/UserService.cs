using Emlak.DAL.Abstract;
using Emlak.Entity.Concrete;
using Emlak.Entity.DBSettings;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emlak.DAL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IDbSettings dbSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(dbSettings.DatabaseName);
            _users = database.GetCollection<User>(dbSettings.UserCollectionName);
        }

        public User Create(User user)
        {
            user.UserID = ObjectId.GenerateNewId().ToString();
            _users.InsertOne(user);
            return user;
        }

        public void Delete(string id)
        {
            _users.DeleteOne(user => user.UserID == id);
        }

        public List<User> GetAll()
        {
            return _users.Find(user => true).ToList();
        }

        public List<User> GetByFilter(string? userName)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(userName))
            {
                filter = filter & filterBuilder.Where(user => user.UserName.ToLower().Contains(userName.ToLower()));
            }

            return _users.Find(filter).ToList();
        }

        public User GetById(string id)
        {
            return _users.Find(user => user.UserID == id).FirstOrDefault();
        }

        public void Update(string id, User user)
        {
            _users.ReplaceOne(user => user.UserID == id, user);
        }
    }
}
