// File: CarFixture.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 06 08 2015

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
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
            private static readonly object[] TestCar =
            {
                new object[]
                {
                    new Car(0, 1200, new GasolineEngine(500, EngineTypes.V8), "testCar", null)
                }
            };

            public void ActAccelerateTheCar()
            {
                _car = new Car(0, 1200, new GasolineEngine(500, EngineTypes.V8), "Prototype", null);

                _car.Accelerate(100);
            }

            [Test, TestCaseSource("TestCar")]
            public void ItShouldThrowFuelException(Car car)
            {
                var exception = Assert.Throws<FuelException>(() => ActAccelerateTheCar());
                Assert.AreEqual("Run out of Fuel!", exception.Message);
            }
        }


        [TestFixture]
        public class CarCalculateTraveledDistance : CarFixture
        {
            private static readonly object[] TestCar =
            {
                new object[]
                {
                    new Car(0, 1200, new GasolineEngine(500, EngineTypes.V8), "testCar", null)
                }
            };
           
            
            private static  readonly  object Time = TimeSpan.FromSeconds(30);

            public void ActContinousAccelerateForTenSecondsAndStop()
            {
               var timer = new Stopwatch();
                timer.Start();
                while (timer.Elapsed == TimeSpan.FromSeconds(10))
                {
                    _car.Accelerate(); 
                }
                _car.StopTheCar();
                
            }

            [TestCase("Time")]
            public void ItShouldTravelFixedDistance(double v , double t, double s)
            {
               ActContinousAccelerateForTenSecondsAndStop();
                Assert.AreEqual(v*t, s);
            }

        

        }
    }
}