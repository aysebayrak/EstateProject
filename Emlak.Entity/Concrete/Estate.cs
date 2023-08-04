using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Emlak.Entity.Concrete
{
    public class Estate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EstateID { get; set; }

        //[BsonElement("title")]
        public string Title { get; set; }

        //[BsonElement("price")]
        public int Price { get; set; }

        //[BsonElement("city")]
        public string City { get; set; }


        //[BsonElement("buildYear")]
        public string Buildyear { get; set; }



        //[BsonElement("medicine")]
        public string Medicine { get; set; }


        //[BsonElement("room")]
        public int Room { get; set; }


    }
}
