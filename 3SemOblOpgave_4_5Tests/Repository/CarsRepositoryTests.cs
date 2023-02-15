using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3SemOblOpgave1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace _3SemOblOpgave1.Tests
{
    [TestClass()]
    public class CarsRepositoryTests
    {
        CarsRepository _repo;
        private List<Car> testListOfCars;
        private Car? testCar;
        private int expectedId;
        private int expectedPrice;
        private string expectedModel;
        private string expectedLicensePlate;

        public CarsRepositoryTests() 
        { 
            _repo = new();
            expectedId = 3;
            expectedPrice = 30000;
            expectedModel = "Audi";
            expectedLicensePlate = "au2354";
            testCar = _repo.GetById(expectedId);
            testListOfCars = _repo.GetAll();
        }


        [TestMethod()]
        public void GetAllTest()
        {
            Assert.IsNotNull(testListOfCars, "List of Cars is null");
            Assert.IsInstanceOfType(testListOfCars, typeof(List<Car>), "Wrong Type of List");
            Assert.AreEqual(10, testListOfCars.Count(), "Wrong number of Cars in List");
            Assert.AreEqual(expectedId, testListOfCars[2].Id, "Id in List is not the same");
            Assert.AreEqual(
                expected: 1, 
                actual: testListOfCars.FindAll(car => car.Id == 3).Count(),
                message: $"Id: {testCar.Id} is not unipue, appears {testListOfCars.FindAll(car => car.Id == 3).Count()} times");
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull(testCar, "Car is null");
            Assert.AreEqual(expectedId, testCar.Id, "Id is not the same");
            Assert.AreEqual(expectedPrice, testCar.Price, "Price is not the same");
            Assert.AreEqual(expectedModel, testCar.Model, "Model is not the same");
            Assert.AreEqual(expectedLicensePlate, testCar.LicensePlate, "LicensePlates is not the same");
        }

        [TestMethod()]
        public void CreateCarTest()
        {
            Car createdCar = _repo.CreateCar(new Car(0, "Test2", 1000, "te2354"));
            testListOfCars = _repo.GetAll();
            Assert.AreEqual(11, testListOfCars.Count(), $"List of Cars is not one longer, Car meant to be added: {createdCar} the newest Car is: {testListOfCars.Last()}");
            Assert.AreEqual(createdCar.Id, testListOfCars.Last().Id, "Id is not the same");
            Assert.AreEqual(createdCar.Price, testListOfCars.Last().Price, "Price is not the same");
            Assert.AreEqual(createdCar.Model, testListOfCars.Last().Model, "Model is not the same");
            Assert.AreEqual(createdCar.LicensePlate, testListOfCars.Last().LicensePlate, "LicensePlates is not the same");
            Assert.IsInstanceOfType(createdCar, typeof(Car), "Is not type of: Car");
        }

        [TestMethod()]
        public void UpdateCarTest()
        {
            Car carWithTheUpdatedData = new Car(expectedId, "Test", 1000, "te2354");
            Car carWithTheUpdatedDataFromRepo = _repo.UpdateCar(expectedId, carWithTheUpdatedData);
            Assert.AreEqual(expectedId, carWithTheUpdatedDataFromRepo.Id, "Id is not the same");
            Assert.AreEqual(carWithTheUpdatedData.Price, carWithTheUpdatedDataFromRepo.Price, "Price is not the same");
            Assert.AreEqual(carWithTheUpdatedData.Model, carWithTheUpdatedDataFromRepo.Model, "Model is not the same");
            Assert.AreEqual(carWithTheUpdatedData.LicensePlate, carWithTheUpdatedDataFromRepo.LicensePlate, "LicensePlates is not the same");
            Assert.IsInstanceOfType(carWithTheUpdatedDataFromRepo, typeof(Car), "Is not type of: Car");
        }

        [TestMethod()]
        public void DeleteCarTest()
        {
            _repo.DeleteCar(expectedId);
            Assert.IsNull(_repo.GetById(expectedId), "Car is not deleted");
            Assert.AreNotEqual(testListOfCars.Count() - 1, testListOfCars.Count(), "List of Cars is not one shorter");
            Assert.ThrowsException<ArgumentNullException>(() => _repo.DeleteCar(expectedId), "Car is not deleted");
        }
    }
}