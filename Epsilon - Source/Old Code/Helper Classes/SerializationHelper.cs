using Newtonsoft.Json;
using System;
namespace Epsilon
{
    public static class SerializationHelper
    {
        public static object Deserialize(string data, Type type)
        {
            return JsonConvert.DeserializeObject(data, type);
        }
        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
