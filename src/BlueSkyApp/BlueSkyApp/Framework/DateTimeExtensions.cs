using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSkyApp.Framework;

static class DateTimeExtensions
{
    public static string? ToShortDateTimeString(this DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return null;
        }

        var diff = DateTime.Now - dateTime.Value;

        if (diff.TotalMinutes <= 1)
        {
            return "<1m";
        }
        else if (diff.TotalMinutes < 60)
        {
            return $"{(int)diff.TotalMinutes}m";
        }
        else if (diff.TotalHours < 24)
        {
            return $"{(int)diff.TotalHours}h";
        }
        else if (diff.TotalDays < 365)
        {
            return $"{(int)diff.TotalDays}d";
        }
        else
        {
            return $"{(int)(diff.TotalDays / 365)}y";
        }
    }
}
