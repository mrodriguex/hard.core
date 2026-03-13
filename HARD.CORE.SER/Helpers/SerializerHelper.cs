using HARD.CORE.OBJ;

using Newtonsoft.Json;

namespace HARD.CORE.SER.Helpers
{
    public static class SerializerHelper
    {

        public static T? DeserializeObject<T>(string json) => JsonConvert.DeserializeObject<T>(json);
        public static string SerializeObject(object obj) => JsonConvert.SerializeObject(obj);

        public static bool TryDeserializeObject<T>(string json, out T result)
        {
            //try
            //{
            result = JsonConvert.DeserializeObject<T>(json);
            return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    result = default(T);
            //    return false;
            //}
        }

        public static bool TryDeserializeWebResult<T>(string json, out WebResult<T> result)
        {
            //try
            //{
            result = JsonConvert.DeserializeObject<WebResult<T>>(json);
            return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    result = default(T);
            //    return false;
            //}
        }
    }
}
