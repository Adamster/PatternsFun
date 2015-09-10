// File: CarFixture.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 07 08 2015

using Domain;
using Domain.CarTypes;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public abstract class CarFixture
    {
        [SetUp]
        public void SetUp()
        {
            //     _car = new Car(100, 1500, new GasolineEngine(350, EngineTypes.V6), "Test Car", null);
        }

        private Car _car;

        [TestFixture]
        public class CarAccelerateIsTargetAchived : CarFixture
        {
            public void ActAccelerateTheCar()
            {
                //      _car = new Car(0, 1200, new GasolineEngine(500, EngineTypes.V8), "Prototype", null);

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
            public void ActContinousAccelerateThanStop(Car car)
            {
                do
                {
                    car.ContinousAccelerate();
                } while (car.Mileage < 1000);
                car.StopWatch();
            }

            //   [Test, TestCaseSource("TestCar")]
            public void ItShouldTravelLittleMoreThanDistance(Car car)
            {
                ActContinousAccelerateThanStop(car);
                Assert.Greater(car.Mileage, 1000);
            }
        }
    }
}