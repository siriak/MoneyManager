using System;

namespace Core
{
    public static class DateTimeHelpers
    {
        public static Date ToDate(this DateTime dt) => new Date(dt);
    }
}
