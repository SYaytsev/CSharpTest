using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (dayCount < 0)
            {
                throw new ArgumentException("period is negative");
            }

            int weekEndsCount = CountWeekEnds(weekEnds);

            return startDate.AddDays(dayCount - weekEndsCount);
        }

        private int CountWeekEnds(WeekEnd[] weekEnds)
        {
            int count = 0;
            if (weekEnds != null && weekEnds.Length != 0)
            {
                foreach (WeekEnd item in weekEnds)
                {
                    if (item.StartDate.Date == item.EndDate.Date)
                    {
                        ++count;
                    }
                    else
                    {
                        count += (item.EndDate.Date - item.StartDate.Date).Days + 1;
                    }
                }
            }
            return count;
        }
    }
}
