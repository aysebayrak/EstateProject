using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emlak.Entity.Concrete
{

	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string UserID { get; set; }

		//[BsonElement("userName")]
		public string UserName { get; set; }

		//[BsonElement("password")]
		public string Password { get; set; }
	}
}
