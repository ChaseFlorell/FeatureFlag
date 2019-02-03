using System;
using System.Globalization;

namespace FutureFlag
{
    public class BetweenDatesFutureFlag : Base.DateFutureFlag
    {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public override bool IsEnabled
            {
                get
                {
                    var start = DateTime.Parse(StartDate, CultureInfo.DateTimeFormat);
                    var end = DateTime.Parse(EndDate, CultureInfo.DateTimeFormat);
                    var current = UseUtc ? UtcNow : Now;
                    return current >= start && current <= end;
                }
            }
    }
}