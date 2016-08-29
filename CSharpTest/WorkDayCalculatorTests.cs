using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {
        // Example
        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count), result);
        }

        [TestMethod]
        public void Calculate_When_given_appropriate_data_Then_returns_expected_result()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;
            WeekEnd weekEnd1 = new WeekEnd(new DateTime(2014, 12, 2), new DateTime(2014, 12, 3));
            WeekEnd weekEnd2 = new WeekEnd(new DateTime(2014, 12, 4), new DateTime(2014, 12, 7));
            WeekEnd weekEnd3 = new WeekEnd(new DateTime(2014, 12, 9), new DateTime(2014, 12, 9));
            WeekEnd[] weekEnds = { weekEnd1, weekEnd2, weekEnd3 };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);

            // Assert
            Assert.AreEqual(new DateTime(2014, 12, 4), result);
        }

        [TestMethod]
        public void Calculate_When_given_empty_weekEnds_array_Then_return_expected_date()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            // Assert
            Assert.AreEqual(startDate.AddDays(count), result);
        }

        [TestMethod]
        public void Calculate_When_given_period_lower_then_num_weekEnds_Then_returns_date_lower_then_start()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 5;
            WeekEnd weekEnd1 = new WeekEnd(new DateTime(2014, 12, 2), new DateTime(2014, 12, 3));
            WeekEnd weekEnd2 = new WeekEnd(new DateTime(2014, 12, 4), new DateTime(2014, 12, 7));
            WeekEnd weekEnd3 = new WeekEnd(new DateTime(2014, 12, 9), new DateTime(2014, 12, 9));
            WeekEnd[] weekEnds = { weekEnd1, weekEnd2, weekEnd3 };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);

            // Assert
            Assert.AreEqual(new DateTime(2014, 11, 29), result);
        }

        // Exceptions

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_When_given_negative_period_Then_trows_ArgumentException()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = -10;

            // Act & Assert
            new WorkDayCalculator().Calculate(startDate, count, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Calculate_When_given_min_start_date_and_min_period_Then_trows_ArgumentOutOfRangeException()
        {
            // Arrange
            DateTime startDate = DateTime.MinValue;
            int count = 5;
            WeekEnd weekEnd1 = new WeekEnd(new DateTime(2014, 12, 2), new DateTime(2014, 12, 3));
            WeekEnd weekEnd2 = new WeekEnd(new DateTime(2014, 12, 4), new DateTime(2014, 12, 7));
            WeekEnd weekEnd3 = new WeekEnd(new DateTime(2014, 12, 9), new DateTime(2014, 12, 9));
            WeekEnd[] weekEnds = { weekEnd1, weekEnd2, weekEnd3 };

            // Act & Assert
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Calculate_When_given_max_start_date_Then_trows_ArgumentOutOfRangeException()
        {
            // Arrange
            DateTime startDate = DateTime.MaxValue;
            int count = 10;
            WeekEnd weekEnd1 = new WeekEnd(new DateTime(2014, 12, 2), new DateTime(2014, 12, 3));
            WeekEnd weekEnd2 = new WeekEnd(new DateTime(2014, 12, 4), new DateTime(2014, 12, 7));
            WeekEnd weekEnd3 = new WeekEnd(new DateTime(2014, 12, 9), new DateTime(2014, 12, 9));
            WeekEnd[] weekEnds = { weekEnd1, weekEnd2, weekEnd3 };

            // Act & Assert
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Calculate_When_given_max_period_Then_trows_ArgumentOutOfRangeException()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = int.MaxValue;
            WeekEnd weekEnd1 = new WeekEnd(new DateTime(2014, 12, 2), new DateTime(2014, 12, 3));
            WeekEnd weekEnd2 = new WeekEnd(new DateTime(2014, 12, 4), new DateTime(2014, 12, 7));
            WeekEnd[] weekEnds = { weekEnd1, weekEnd2 };

            // Act & Assert
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Calculate_When_given_array_with_max_values_Then_trows_ArgumentOutOfRangeException()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;
            WeekEnd weekEnd1 = new WeekEnd(new DateTime(2014, 12, 2), new DateTime(2014, 12, 3));
            WeekEnd weekEnd2 = new WeekEnd(new DateTime(2014, 12, 9), new DateTime(2014, 12, 9));
            WeekEnd weekEnd3 = new WeekEnd(new DateTime(2014, 12, 10), (DateTime.MaxValue));
            WeekEnd[] weekEnds = { weekEnd1, weekEnd2, weekEnd3 };

            // Act & Assert
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Calculate_When_given_empty_array_Then_trows_NullReferenceException()
        {
            // Arrange
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;
            WeekEnd[] weekEnds = new WeekEnd[5];

            // Act & Assert
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekEnds);
        }
    }
}
