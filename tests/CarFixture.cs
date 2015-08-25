// File: CarFixture.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 07 08 2015

using Domain;
using Domain.CarTypes;
using Domain.EnginesTypes;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public abstract class CarFixture
    {
        [SetUp]
        public void SetUp()
        {
            _car = new Car(100, 1500, new GasolineEngine(350, EngineTypes.V6), "Test Car", null);
        }

        private Car _car;

        [TestFixture]
        public class CarAccelerateIsTargetAchived : CarFixture
        {
           public void ActAccelerateTheCar()
            {
                _car = new Car(0, 1200, new GasolineEngine(500, EngineTypes.V8), "Prototype", null);

                _car.Accelerate(100);
            }

            [TestCase]
            public void ItShouldThrowFuelException()
            {
                var exception = Assert.Throws<FuelException>(() => ActAccelerateTheCar());
                Assert.AreEqual("Run out of Fuel!", exception.Message);
            }
        }

        [TestFixture]
        public class CarCalculateTraveledDistance : CarFixture
        {
            #region TestCars
            private static readonly object[] TestCar =
            {
                new object[]
                {
                    new Car(100, 1200, new GasolineEngine(500, EngineTypes.V8), "SportCar", null)
                },
                new object[]
                {
                    new Car(100, 1200, new GasolineEngine(200, EngineTypes.V8), "testCar2", null)
                },
                new object[]
                {
                    new Car(100, 1200, new GasolineEngine(1200, EngineTypes.V8), "testCar3", null)
                },
                new object[]
                {
                    new Car(100, 1200, new GasolineEngine(120, EngineTypes.V8), "BudgetCar", null)
                },
                new object[]
                {
                    new Car(100, 2000, new GasolineEngine(500, EngineTypes.V8), "testCar5", null)
                },
                new object[]
                {
                    new Car(100, 3500, new GasolineEngine(900, EngineTypes.V8), "Truck", null)
                },
                new object[]
                {
                    new Car(100, 1200, new GasolineEngine(500, EngineTypes.V8), "testCar7", null)
                },
                new object[]
                {
                    new Car(100, 1200, new GasolineEngine(500, EngineTypes.V8), "testCar8", null)
                }
            };
#endregion
            public void ActContinousAccelerateThanStop(Car car)
            {

                
                do
                {
                    car.ContinousAccelerate();
                } while (car.Mileage < 1000);
                car.StopWatch();
            }

            [Test, TestCaseSource("TestCar")]
            public void ItShouldTravelLittleMoreThanDistance(Car car)
            {
                ActContinousAccelerateThanStop(car);
                Assert.Greater(car.Mileage, 1000);
            }
        }
    }
}