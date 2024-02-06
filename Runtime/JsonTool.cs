using Newtonsoft.Json;

namespace ZhengDianWaiBao.Tool
{
    public static class JsonTool
    {
        public static T DeserializeObject<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}