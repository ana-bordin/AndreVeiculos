using Models;
using System;
using Utilities;

namespace Main
{
    internal class Program
    {
        static int sizeCarsList = 30;
        static Random random = new Random();

        static List<Car> cars = new List<Car>();
        static List<CarJob> carJobs = new List<CarJob>();
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

        static void CreateListCar()
        {
            for (int i = 0; i < sizeCarsList; i++)
            {
                cars.Add(CarGenerator.GenerateCar());
            }
        }
        static void CreateListCarJob()
        {
            for (int i = 0; i < sizeCarsList; i++)
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


        static void Main(string[] args)
        {
            Console.WriteLine("Gerar carros:");
            CreateListCar();
            Console.ReadKey();


            Console.WriteLine("Gerar serviços:");
            CreateListCarJob();



            //Console.WriteLine("Inserir dados nos arquivo.json:");



            //CreateListCarJob();

            //if (JsonRepository.InsertJson(cars, Jobs, carJobs))
            //    Console.WriteLine("Registros inseridos com sucesso!");
        }
    }
}
