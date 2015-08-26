using Domain.CarTypes;
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
            _carActionOnCreationMock = new Mock<ICarActionOnCreation>();
            _electroACtion = new Mock<IElectroCarActionOnCreation>();
            _carFactory = new CarFactory(_carActionOnCreationMock.Object, _electroACtion.Object);
        }

        private Mock<ICarActionOnCreation> _carActionOnCreationMock;
        private CarFactory _carFactory;
        private Mock<IElectroCarActionOnCreation> _electroACtion;

        public void ActCreateProdcut()
        {
            //   _carFactory.CreateNewSportCar(0, 1000, 250, EngineTypes.V10, "TestCar", null);
        }

        [Test]
        public void ItShouldFillTankOnCarCreation()
        {
            _carActionOnCreationMock.Setup(x => x.FillCarTank(It.IsAny<Car>()));

            ActCreateProdcut();
            _carActionOnCreationMock.Verify(creation => creation.FillCarTank(It.IsAny<Car>()), Times.Once);
        }
    }
}