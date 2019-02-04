using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace ConfigurationReader
{
    public class ConfigurationEntitiy
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BsonObjectId _id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BsonInt32 ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int IsActive { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ApplicationName { get; set; }
    }
}
