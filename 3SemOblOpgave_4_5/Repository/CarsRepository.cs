using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _3SemOblOpgave1
{
    public class CarsRepository
    {
        private static int NextId;
        private static List<Car> cars = new();

        public CarsRepository()
        {
            NextId = 1;
            cars = new List<Car>
            {
                new Car(NextId++, "Ford", 10000, "fo2354"),
                new Car(NextId++, "BMW", 20000, "bm2354"),
                new Car(NextId++, "Audi", 30000, "au2354"),
                new Car(NextId++, "VW", 40000, "vw2354"),
                new Car(NextId++, "Mercedes", 50000, "me2354"),
                new Car(NextId++, "Volvo", 60000, "vo2354"),
                new Car(NextId++, "Skoda", 70000, "sk2354"),
                new Car(NextId++, "Fiat", 80000, "fi2354"),
                new Car(NextId++, "Opel", 90000, "op2354"),
                new Car(NextId++, "Toyota", 100000, "to2354"),
            };
        }

        public List<Car> GetAll()
        {
            return new List<Car>(cars);
        }

        public Car? GetById(int id)
        {
            return cars.Find(car => car.Id == id);
        }

        public Car CreateCar(Car newCar)
        {
            newCar.Id = NextId++;
            cars.Add(newCar);
            return newCar;
        }

        public Car? UpdateCar(int id, Car carTobeUpdated)
        {
            Car carToUpdate = GetById(id);
            if (carToUpdate == null)
            {
                throw new ArgumentNullException();
            }
            Type type = carToUpdate.GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                property.SetValue(carToUpdate, property.GetValue(carTobeUpdated));
            }
            return carToUpdate;
        }

        public Car? DeleteCar(int id)
        {
            Car carToDelete = GetById(id);
            if (carToDelete == null) throw new ArgumentNullException();
            cars.Remove(carToDelete);
            return carToDelete;
            
        }
    }
}
