using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Address
    {
        [BsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        [Key]
        public int Id;
        public string TypeStreet { get; set; }

        [JsonProperty("logradouro")]
        public string Street { get; set; }
        
        [JsonProperty("cep")]
        public string ZipCode { get; set; }
        [JsonProperty("bairro")]
        public string Neighborhood { get; set; }

        [JsonProperty("localidade")]
        public string City { get; set; }
        
        [JsonProperty("uf")]
        public string State { get; set; }

        public int Number { get; set; }

        [JsonProperty("complemento")]
        public string Complement { get; set; }

        public override string ToString()
        {
            return $"Street: {Street}, Number: {Number}, Complement: {Complement}, Neighborhood: {Neighborhood}, City: {City}, State: {State}, ZipCode: {ZipCode}";
        }
    }
}
