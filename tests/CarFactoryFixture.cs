// File: CarFactoryFixture.cs in
// PatternsFun by Serghei Adam 
// Created 06 08 2015 
// Edited 06 08 2015

using Domain.CarTypes;
using Domain.EnginesTypes;
using Factories;
using InterfacesActions;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class CarFactoryFixture
    {
        [SetUp]
        public void SetUp()
        {
            _ICarActionOnCreationMock = new Mock<ICarActionOnCreation>();
            _carFactory = new CarFactory(_ICarActionOnCreationMock.Object);
        }

        private Mock<ICarActionOnCreation> _ICarActionOnCreationMock;
        private CarFactory _carFactory;


        public void ActCreateProdcut()
        {
            _carFactory.CreateNewSportCar(0, 1000, 250, EngineTypes.V10, "TestCar", null);

        }

        [Test]
        public void ItShouldFillTankOnCarCreation()
        {
            _ICarActionOnCreationMock.Setup(x=> x.FillCarTank(It.IsAny<Car>()));

            ActCreateProdcut();
            _ICarActionOnCreationMock.Verify(creation => creation.FillCarTank(It.IsAny<Car>()), Times.Once);
        }

    }
}