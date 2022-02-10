using System;

namespace WorkdayCalendarNS
{
    public class WorkdayCalendar
    {
        // Consider Nager.Date instead if many individual holidays need to be set
        /// <summary>
        /// Set holiday at a specific date
        /// </summary>
        public void SetHoliday(DateTime date)
        {
            // Only store holiday if it is unique, not in the weekend or a recurring holiday
            if (IsWorkday(date) == true)
            {
                m_IndividualHolidays.Add(date);
            }
        }

        /// <summary>
        /// Set holiday recurring at the same date every year
        /// </summary>
        public void SetRecurringHoliday(DateTime date)
        {
            // Only store if unique
            if (IsRecurringHoliday(date) == false)
            {
                m_RecurringHolidays.Add(date);
            }
        }

        /// <summary>
        /// Set workday start and stop in hours and minutes
        /// </summary>
        public void SetWorkdayStartAndStop(DateTime start, DateTime stop)
        {
            if (start.Hour == stop.Hour && start.Minute == stop.Minute)
            {
                throw new ArgumentException("ERROR: Start time cannot be the same as stop time");
            }
            else
            {
                m_Start = start.TimeOfDay;
                m_Stop = stop.TimeOfDay;
            }
        }

        /// <summary>
        /// Get date and time of a workday an increment distance from the start date
        /// </summary>
        public DateTime GetWorkdayIncrement(DateTime startDate, float incrementInWorkdays)
        {
            DateTime startDateAdjusted;
            DateTime workday;
            float incrementAdjusted = incrementInWorkdays;

            if (m_Start.Hours < 0 || m_Stop.Hours < 0)
            {
                throw new NotSupportedException("ERROR: SetWorkdayStartAndStop needs to be called first");
            }
            else
            {
                // If the start date and time is within a workday, add its increment and set start to start of workday
                if (IsWorkday(startDate) && IsWithinWorkday(startDate.TimeOfDay) == true)
                {
                    incrementAdjusted += GetStartDateIncrement(startDate.TimeOfDay);
                    startDateAdjusted = new DateTime(startDate.Year, startDate.Month, startDate.Day,
                                                     m_Start.Hours, m_Start.Minutes, m_Start.Seconds);
                    
                    workday = GetUpdatedWorkday(startDateAdjusted, incrementAdjusted);
                }
                else
                {
                    startDateAdjusted = GetStartDateAdjusted(startDate, ref incrementAdjusted);
                    workday = GetUpdatedWorkday(startDateAdjusted, incrementAdjusted);
                }
            }

            return workday;
        }

        private bool IsWorkday(DateTime date)
        {
            bool isWorkday = true;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                isWorkday = false;
            }
            else if (IsRecurringHoliday(date) == true)
            {
                isWorkday = false;
            }
            else if (IsIndividualHoliday(date) == true)
            {
                isWorkday = false;
            }

            return isWorkday;
        }

        private bool IsRecurringHoliday(DateTime date)
        {
            bool isRecurring = false;
            int i = 0;

            while (isRecurring == false && i < m_RecurringHolidays.Count)
            {
                if (date.Month == m_RecurringHolidays[i].Month && date.Day == m_RecurringHolidays[i].Day)
                {
                    isRecurring = true;
                }
                i++;
            }

            return isRecurring;
        }

        private bool IsIndividualHoliday(DateTime date)
        {
            bool isIndividual = false;
            int i = 0;

            while (isIndividual == false && i < m_IndividualHolidays.Count)
            {
                if (date.Date == m_IndividualHolidays[i].Date)
                {
                    isIndividual = true;
                }
                i++;
            }

            return isIndividual;
        }

        private bool IsWithinWorkday(TimeSpan time)
        {
            return (m_Start <= time) && (time <= m_Stop);
        }

        private float GetStartDateIncrement(TimeSpan startDateTime)
        {
            TimeSpan workdaySpan = m_Stop - m_Start;
            TimeSpan startDateSpan = startDateTime - m_Start;

            return (float)(startDateSpan.TotalSeconds / workdaySpan.TotalSeconds);
        }

        private DateTime GetStartDateAdjusted(DateTime date, ref float increment)
        {
            DateTime startDateAdjusted;

            if (IsWorkday(date) == true)
            {
                // Before work hours and negative increment, update to workday in past
                if ((date.TimeOfDay < m_Start) && (increment < 0))
                {
                    startDateAdjusted = GetPreviousWorkdayStart(date);
                    increment = UpdateNegativeIncrement(increment);
                }
                // Before work hours and positive increment, update to current workday start
                else if ((date.TimeOfDay < m_Start) && (increment >= 0))
                {
                    startDateAdjusted = new DateTime(date.Year, date.Month, date.Day,
                                                     m_Start.Hours, m_Start.Minutes, m_Start.Seconds);
                }
                // After work hours and negative increment, update to current workday start
                else if ((m_Stop < date.TimeOfDay) && (increment < 0))
                {
                    startDateAdjusted = new DateTime(date.Year, date.Month, date.Day,
                                                     m_Start.Hours, m_Start.Minutes, m_Start.Seconds);
                    increment = UpdateNegativeIncrement(increment);
                }
                // After work hours and positive increment, update to next workday start
                else if ((m_Stop < date.TimeOfDay) && (increment >= 0))
                {
                    startDateAdjusted = GetNextWorkdayStart(date);
                }
                else
                {
                    throw new ArgumentException("ERROR: Unexpected date: " + date);
                }
            }
            else
            {
                // Date is not a workday, find next workday or previous workday with updated increment
                if (increment >= 0)
                {
                    startDateAdjusted = GetNextWorkdayStart(date);
                }
                else
                {
                    startDateAdjusted = GetPreviousWorkdayStart(date);
                    increment = UpdateNegativeIncrement(increment);
                }
            }

            return startDateAdjusted;
        }

        private DateTime GetPreviousWorkdayStart(DateTime date)
        {
            DateTime previous = date;

            previous = previous.AddDays(-1);
            while (IsWorkday(previous) == false)
            {
                previous = previous.AddDays(-1);
            }

            return new DateTime(previous.Year, previous.Month, previous.Day, m_Start.Hours, m_Start.Minutes, m_Start.Seconds);
        }

        private DateTime GetNextWorkdayStart(DateTime date)
        {
            DateTime next = date;

            next = next.AddDays(1);
            while (IsWorkday(next) == false)
            {
                next = next.AddDays(1);
            }

            return new DateTime(next.Year, next.Month, next.Day, m_Start.Hours, m_Start.Minutes, m_Start.Seconds);
        }

        private static float UpdateNegativeIncrement(float increment)
        {
            // Update increment from workday stop to workday start by adding the distance of a workday
            return increment += 1.0F;
        }

        private DateTime GetUpdatedWorkday(DateTime start, float increment)
        {
            DateTime updatedDate = start;

            if (increment >= 0)
            {
                while (IsPositiveFraction(increment) == false)
                {
                    updatedDate = GetNextWorkdayStart(updatedDate);
                    increment -= 1.0F;
                }
                updatedDate = UpdateTimeFromIncrement(updatedDate, increment);
            }
            else
            {
                while (IsPositiveFraction(increment) == false)
                {
                    updatedDate = GetPreviousWorkdayStart(updatedDate);
                    increment += 1.0F;
                }
                updatedDate = UpdateTimeFromIncrement(updatedDate, increment);
            }

            return updatedDate;
        }

        private static bool IsPositiveFraction(float value)
        {
            return ((0 <= value) && (value < 1));
        }

        private DateTime UpdateTimeFromIncrement(DateTime workdayStart, float increment)
        {
            TimeSpan workdaySpan = m_Stop - m_Start;
            float secondsToUpdate = (float)(workdaySpan.TotalSeconds * increment);

            return workdayStart + TimeSpan.FromSeconds(secondsToUpdate);
        }

        private TimeSpan m_Start = new TimeSpan(-1, -1, -1);
        private TimeSpan m_Stop = new TimeSpan(-1, -1, -1);

        private List<DateTime> m_IndividualHolidays = new List<DateTime>();
        private List<DateTime> m_RecurringHolidays = new List<DateTime>();
    }
}
