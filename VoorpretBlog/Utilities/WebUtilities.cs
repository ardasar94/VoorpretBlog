using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VoorpretBlog.Utilities
{
    public static class WebUtilities
    {
        public static int[] Pagination(int current, int last)
        {
            int delta = 1;
            int left = current - delta;
            int right = current + delta + 1;
            var range = new List<int>();
            var rangeWithDots = new List<int>();
            int? l = null;

            for (var i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || i >= left && i < right)
                {
                    range.Add(i);
                }
            }

            foreach (var i in range)
            {
                if (l != null)
                {
                    if (i - l == 2)
                    {
                        rangeWithDots.Add(l.Value + 1);
                    }
                    else if (i - l != 1)
                    {
                        rangeWithDots.Add(-1);
                    }
                }
                rangeWithDots.Add(i);
                l = i;
            }

            return rangeWithDots.ToArray();
        }
    }
}