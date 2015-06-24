using System;

namespace TravelpayoutsAPI.Library.Infostructures
{
    public static class LongHelpers
    {
        public static DateTime ToDateTime(this long timestamp)
        {
            var unixYearMin = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = timestamp * TimeSpan.TicksPerSecond;

            return new DateTime(unixYearMin.Ticks + unixTimeStampInTicks);
        }
    }
}
