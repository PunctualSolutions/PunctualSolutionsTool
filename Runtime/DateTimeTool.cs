using System;

namespace ZhengDianWaiBao.Tool
{
    public static class DateTimeTool
    {
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp) =>
            new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
    }
}