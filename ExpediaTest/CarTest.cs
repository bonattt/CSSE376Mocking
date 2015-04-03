using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}
        [TestMethod]
        public void TestThatCarIsParkedInRightSpot()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();

            String location1 = "Cincinati";
            String location2 = "Nowhere";

            Expect.Call(mockDatabase.getCarLocation(5)).Return(location1);
            Expect.Call(mockDatabase.getCarLocation(1025)).Return(location2);

            mocks.ReplayAll();
            Car target = new Car(17);
            target.Database = mockDatabase;
            String result;
            result = target.getCarLocation(1025);
            Assert.AreEqual(location2, result);

            result = target.getCarLocation(5);
            Assert.AreEqual(location1, result);

            mocks.VerifyAll();
        }
        [TestMethod]
        public void TestMilageProperty()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            Int32 expected1 = 1000;
            Int32 expected2 = 2000;
            Expect.Call(mockDB.Miles).Return(expected1);
            Expect.Call(mockDB.Miles).Return(expected2);

            mocks.ReplayAll();
            Car car1 = new Car(2);
            car1.Database = mockDB;
            Car car2 = new Car(5);
            car2.Database = mockDB;

            Int32 result = car1.Mileage;
            Assert.AreEqual(expected1, result);
            
            result = car2.Mileage;
            Assert.AreEqual(expected2, result);

            mocks.VerifyAll();            
        }
        [TestMethod]
        public void TestObjectMother(){
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            Int32 expected1 = 1509;
            Int32 expected2 = 1900;
            Expect.Call(mockDB.Miles).Return(expected1);
            Expect.Call(mockDB.Miles).Return(expected2);

            mocks.ReplayAll();
            var car1 = ObjectMother.Saab();
            car1.Database = mockDB;
            var car2 = ObjectMother.BMW();
            car2.Database = mockDB;

            Int32 result = car1.Mileage;
            Assert.AreEqual(expected1, result);
            
            result = car2.Mileage;
            Assert.AreEqual(expected2, result);

            mocks.VerifyAll();    
        }
	}
}
