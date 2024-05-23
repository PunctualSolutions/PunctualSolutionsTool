using Newtonsoft.Json;

namespace ZhengDianWaiBao.Tool
{
    public static class JsonTool
    {
        public static T JsonToObject<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
        public static string ToJson<T>(this T @object) => JsonConvert.SerializeObject(@object);
    }
}