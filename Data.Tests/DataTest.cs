using Models;

namespace Data.Tests
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void PassingTest()
        {
            Assert.AreEqual(1, 1); //Make sure my test suite still runs correctly
        }

        [TestMethod]
        public void DataCollector_AddDay()
        {
            //Arrange
            var collector = new MockDatacollector();
            var mockday = new Day()
            {
                Date = "one",
                Workstart = "two",
                Workend = "three",
                Lunchstart = "four",
                Lunchend = "five",
                Worktime = "six",
                Lunchworktime = "seven"
            };

            //Act
            collector.delete_withCondition("one='one'"); //Delete any previous test inputs
            var temporary_result = collector.getData();
            Assert.AreEqual(0, temporary_result.Count()); //make sure the deletion worked

            collector.Add(mockday);

            //Assert
            var result = collector.getData();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
         
        }
    }
}