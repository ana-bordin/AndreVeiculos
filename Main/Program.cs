using Microsoft.Identity.Client;
using Models;
using Repositories;
using System;
using System.IO;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Transactions;
using Utilities;

namespace Main
{
    internal class Program
    {
        static int sizeList = 30;
        static Random random = new Random();

        static List<Car> cars = new List<Car>();
        static List<Buy> buys = new List<Buy>();
        static List<Job> jobs = new List<Job>() {
            new Job { Description = "Limpeza exterior e interior do carro." },
            new Job { Description = "Substituição do óleo do motor." },
            new Job { Description = "Ajuste do equilíbrio dos pneus." },
            new Job { Description = "Correção do alinhamento das rodas." },
            new Job { Description = "Inspeção e substituição de pastilhas e discos de freio." },
            new Job { Description = "Substituição dos pneus antigos por novos." },
            new Job { Description = "Substituição de filtros de óleo, ar, combustível e cabine." },
            new Job { Description = "Restauração do brilho da pintura e aplicação de cera protetora." }
        };
        static List<CarJob> carJobs = new List<CarJob>();
        static List<People> peoples = new List<People>();
        static List<Address> addresses = new List<Address>();




        static void CreateListCar()
        {
            for (int i = 0; i < sizeList; i++)
            {
                cars.Add(CarGenerator.GenerateCar());
            }
        }
        static void CreateListCarBuy()
        {
            for (int i = 0; i < sizeList; i++)
            {
                Buy buy = new Buy
                {
                    Car = cars[random.Next(0, cars.Count - 1)],
                    Value = random.Next(10000, 100000),
                    Date = DateTime.Now
                };
                buys.Add(buy);
            }
        }
        static void CreateListCarJob()
        {
            for (int i = 0; i < sizeList; i++)
            {
                CarJob carService = new CarJob
                {
                    Car = cars[random.Next(0, cars.Count - 1)],
                    Job = jobs[random.Next(0, jobs.Count - 1)],
                    Status = random.Next(0, 2) == 0 ? false : true
                };
                carJobs.Add(carService);
            }
        }
        static void CreateListPeoples()
        {
            for (int i = 0; i < sizeList; i++)
            {
                Address address = AddressGenerator.GenerateAdress();
                AddressRepository addressRepository = new AddressRepository();
                addressRepository.Insert(address);
                addresses = addressRepository.GetAll();
               
                People people = new People
                {
                    Document = PeopleGenerator.GenerateDocument(),
                    Name = PeopleGenerator.GenerateName(),
                    BirthDate = PeopleGenerator.GenerateBirth(),
                    Address = addresses[addresses.Count-1],
                    Telephone = PeopleGenerator.GenerateTelephone(),
                    Email = PeopleGenerator.GenerateEmail()
                };
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Gerar carros:");
            CreateListCar();
            CarRepository carRepository = new CarRepository();
            carRepository.InsertAll(cars);
            cars.Clear();
            cars = carRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar compra de carro:");
            CreateListCarBuy();
            BuyRepository buyRepository = new BuyRepository();
            buyRepository.InsertAll(buys);
            buys.Clear();
            buys = buyRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar serviços:");
            CreateListCarJob();
            JobRepository JobRepository = new JobRepository();
            JobRepository.InsertAll(jobs);
            jobs.Clear();
            jobs = JobRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar serviço de carros:");
            CreateListCarJob();
            CarJobRepository carJobRepository = new CarJobRepository();
            carJobRepository.InsertAll(carJobs);
            carJobs.Clear();
            carJobs = carJobRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar pessoas:");
            CreateListPeoples();
            PeopleRepository peopleRepository = new PeopleRepository();
            peopleRepository.InsertAll(peoples);
            peoples.Clear();
            peoples = peopleRepository.GetAll();
            Console.ReadKey();


            //if (JsonRepository.InsertJson(cars, Jobs, carJobs))
            //    Console.WriteLine("Registros inseridos com sucesso!");
        }
    }
}
