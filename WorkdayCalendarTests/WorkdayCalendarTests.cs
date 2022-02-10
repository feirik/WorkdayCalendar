using NUnit.Framework;
using System;
using WorkdayCalendarNS;

namespace WorkdayCalendarTests
{
    public class Tests
    {
        [Test]
        public void StartTimeSameAsStopTime_Throws()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime date = new DateTime(2004, 05, 24, 18, 5, 0);

            Assert.Throws<ArgumentException>(() => workdayCalendar.SetWorkdayStartAndStop(date, date));
        }

        [Test]
        public void SetWorkdayStartAndStopNotCalled_Throws()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime date = new DateTime(2004, 05, 24, 18, 5, 0);
            float increment = 0;

            Assert.Throws<NotSupportedException>(() => workdayCalendar.GetWorkdayIncrement(date, increment));
        }

        [Test]
        public void TestCase1()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));
            workdayCalendar.SetHoliday(new DateTime(2004, 05, 27, 0, 0, 0));

            DateTime start = new DateTime(2004, 05, 24, 18, 05, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, -5.5F);

            Assert.AreEqual(result.Year, 2004);
            Assert.AreEqual(result.Month, 05);
            Assert.AreEqual(result.Day, 14);
            Assert.AreEqual(result.Hour, 12);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void TestCase2()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));
            workdayCalendar.SetHoliday(new DateTime(2004, 05, 27, 0, 0, 0));

            DateTime start = new DateTime(2004, 05, 24, 19, 03, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 44.723656F);

            Assert.AreEqual(result.Year, 2004);
            Assert.AreEqual(result.Month, 07);
            Assert.AreEqual(result.Day, 27);
            Assert.AreEqual(result.Hour, 13);
            Assert.AreEqual(result.Minute, 47);
        }

        [Test]
        public void TestCase4()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));
            workdayCalendar.SetHoliday(new DateTime(2004, 05, 27, 0, 0, 0));

            DateTime start = new DateTime(2004, 05, 24, 08, 03, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 12.782709F);

            Assert.AreEqual(result.Year, 2004);
            Assert.AreEqual(result.Month, 06);
            Assert.AreEqual(result.Day, 10);
            Assert.AreEqual(result.Hour, 14);
            Assert.AreEqual(result.Minute, 18);
        }

        [Test]
        public void TestCase5()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));
            workdayCalendar.SetHoliday(new DateTime(2004, 05, 27, 0, 0, 0));

            DateTime start = new DateTime(2004, 05, 24, 07, 03, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 8.276628F);

            Assert.AreEqual(result.Year, 2004);
            Assert.AreEqual(result.Month, 06);
            Assert.AreEqual(result.Day, 04);
            Assert.AreEqual(result.Hour, 10);
            Assert.AreEqual(result.Minute, 12);
        }

        [Test]
        public void TestCase6()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));
            workdayCalendar.SetHoliday(new DateTime(2004, 05, 27, 0, 0, 0));

            DateTime start = new DateTime(2004, 05, 24, 15, 07, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 0.25F);

            Assert.AreEqual(result.Year, 2004);
            Assert.AreEqual(result.Month, 05);
            Assert.AreEqual(result.Day, 25);
            Assert.AreEqual(result.Hour, 09);
            Assert.AreEqual(result.Minute, 07);
        }

        [Test]
        public void IncrementBeforeStartOfWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 07, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 0.0F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 09);
            Assert.AreEqual(result.Hour, 08);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void DecrementBeforeStartOfWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 07, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, -0.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 08);
            Assert.AreEqual(result.Hour, 12);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void IncrementAfterEndOfWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 18, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 0.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 10);
            Assert.AreEqual(result.Hour, 12);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void DecrementAfterEndOfWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 18, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, -0.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 09);
            Assert.AreEqual(result.Hour, 12);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void IncrementInWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 12, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 0.25F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 09);
            Assert.AreEqual(result.Hour, 14);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void DecrementInWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 12, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, -0.25F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 09);
            Assert.AreEqual(result.Hour, 10);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void IncrementFromHoliday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);
            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));

            DateTime start = new DateTime(2022, 05, 17, 12, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 0.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 05);
            Assert.AreEqual(result.Day, 18);
            Assert.AreEqual(result.Hour, 12);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void DecrementFromHoliday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 8, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 16, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);
            workdayCalendar.SetRecurringHoliday(new DateTime(2004, 05, 17, 0, 0, 0));

            DateTime start = new DateTime(2022, 05, 17, 12, 00, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, -0.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 05);
            Assert.AreEqual(result.Day, 16);
            Assert.AreEqual(result.Hour, 12);
            Assert.AreEqual(result.Minute, 00);
        }

        [Test]
        public void ShortWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 0, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 0, 10, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 0, 0, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 1.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 10);
            Assert.AreEqual(result.Hour, 00);
            Assert.AreEqual(result.Minute, 05);
        }

        [Test]
        public void LongWorkday()
        {
            WorkdayCalendar workdayCalendar = new WorkdayCalendar();

            DateTime workdayStart = new DateTime(2019, 01, 01, 0, 0, 0);
            DateTime workdayStop = new DateTime(2019, 01, 01, 20, 0, 0);

            workdayCalendar.SetWorkdayStartAndStop(workdayStart, workdayStop);

            DateTime start = new DateTime(2022, 02, 09, 0, 0, 0);

            DateTime result = workdayCalendar.GetWorkdayIncrement(start, 1.5F);

            Assert.AreEqual(result.Year, 2022);
            Assert.AreEqual(result.Month, 02);
            Assert.AreEqual(result.Day, 10);
            Assert.AreEqual(result.Hour, 10);
            Assert.AreEqual(result.Minute, 00);
        }
    }
}
