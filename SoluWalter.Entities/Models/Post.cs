using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoluWalter.Entities.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //[BsonElement("titulo")]
        public string Titulo { get; set; }
        //[BsonElement("descripcion")]
        public string Descripcion { get; set; }
        //[BsonElement("autor")]
        public string Autor { get; set; }
        //[BsonElement("categoria")]
        public int Categoria { get; set; }
    }
}
