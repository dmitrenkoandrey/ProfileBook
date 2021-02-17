﻿using Newtonsoft.Json;

namespace ProfileBook.Service
{
    public class JsonConvert
    {
        public static T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize<T>(T value)
        {

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            return Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
        }
    }
}
