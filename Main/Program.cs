using Models;
using Repositories;
using Utilities;

namespace Main
{
    internal class Program
    {
        static int sizeList = 30;
        static Random random = new Random();

        static List<Car> cars = new List<Car>();
        static List<Buy> buys = new List<Buy>();
        static List<Job> jobs = new List<Job>()
        {
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
        static List<Client> clients = new List<Client>();
        static List<Employee> employees = new List<Employee>();
        static List<Address> addresses = new List<Address>();

        static List<PositionCompany> positionsCompany = new List<PositionCompany>()
        {
            new PositionCompany { Description = "Manager" },
            new PositionCompany { Description = "Sales Consultant" },
            new PositionCompany { Description = "Purchasing Manager" },
            new PositionCompany { Description = "Car Mechanic" },
            new PositionCompany { Description = "Cleaning Assistant" },
            new PositionCompany { Description = "Receptionist"},
            new PositionCompany { Description = "Detailer" }
        };

        static List<CreditCard> creditCards = new List<CreditCard>();
        static List<Pix> pixs = new List<Pix>();
        static List<BankPaymentSlip> bankPaymentSlips = new List<BankPaymentSlip>();

        static List<Payment> payments = new List<Payment>();
        static List<Sale> sales = new List<Sale>();

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
        static void CreateListPeoples(string type)
        {
            //for (int i = 0; i < sizeList; i++)
            //{
            //    Address address = AddressGenerator.GenerateAdress();
            //    AddressRepository addressRepository = new AddressRepository();
            //    addressRepository.Insert(address);
            //    addresses = addressRepository.GetAll();
            //    if (type == "Client")
            //    {
            //        Client client = new Client
            //        (
            //            document: PeopleGenerator.GenerateDocument(),
            //            name: PeopleGenerator.GenerateName(),
            //            birthDate: PeopleGenerator.GenerateBirth(),
            //            address: addresses[addresses.Count - 1],
            //            telephone: PeopleGenerator.GenerateTelephone(),
            //            email: PeopleGenerator.GenerateEmail(),
            //            personIncome: random.Next(1000, 10000),
            //            pdf: "PDF.pdf"
            //        );
            //        clients.Add(client);
            //    }
            //    else
            //    {
            //        Employee employee = new Employee
            //        (
            //            document: PeopleGenerator.GenerateDocument(),
            //            name: PeopleGenerator.GenerateName(),
            //            birthDate: PeopleGenerator.GenerateBirth(),
            //            address: addresses[addresses.Count - 1],
            //            telephone: PeopleGenerator.GenerateTelephone(),
            //            email: PeopleGenerator.GenerateEmail(),
            //            positionCompany: positionsCompany[random.Next(positionsCompany.Count - 1)],
            //            commissionPercentage: random.Next(0, 100),
            //            commission: random.Next(1000, 10000)
            //        );
            //        employees.Add(employee);
            //    }
            //}
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

            Console.WriteLine("Gerar clientes:");
            CreateListPeoples("Client");
            ClientRepository clientRepository = new ClientRepository();
            clientRepository.InsertAll(clients);
            clients.Clear();
            clients = clientRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar Cargos");
            PositionCompanyRepository positionCompanyRepository = new PositionCompanyRepository();
            positionCompanyRepository.InsertAll(positionsCompany);
            positionsCompany.Clear();
            positionsCompany = positionCompanyRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar empregados");
            CreateListPeoples("Employee");
            EmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.InsertAll(employees);
            employees.Clear();
            employees = employeeRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar Cartão de crédito");
            CreditCard creditCard = PaymentGenerator.CreditCardGenerate();
            creditCards.Add(creditCard);
            CreditCardRepository creditCardRepository = new CreditCardRepository();
            creditCardRepository.InsertAll(creditCards);
            creditCards.Clear();
            creditCards = creditCardRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar Pix");
            Pix pix = PaymentGenerator.PixGenerate();
            pixs.Add(pix);
            PixRepository pixRepository = new PixRepository();
            pixRepository.InsertAll(pixs);
            pixs.Clear();
            pixs = pixRepository.GetAll();
            Console.ReadKey();


            Console.WriteLine("Gerar boleto");
            BankPaymentSlip bankPaymentSlip = PaymentGenerator.BankPaymentSlipGenerate();
            bankPaymentSlips.Add(bankPaymentSlip);
            BankPaymentSlipRepository bankPaymentSlipRepository = new BankPaymentSlipRepository();
            bankPaymentSlipRepository.InsertAll(bankPaymentSlips);
            bankPaymentSlips.Clear();
            bankPaymentSlips = bankPaymentSlipRepository.GetAll();
            Console.ReadKey();

            Console.WriteLine("Gerar Venda");
            Sale sale = SaleGenerator.GenerateSale(cars, clients, employees);
            sales.Add(sale);
            SaleRepository saleRepository = new SaleRepository();
            saleRepository.InsertAll(sales);
            sales.Clear();



            //payment 
            //sales




            //if (JsonRepository.InsertJson(cars, Jobs, carJobs))
            //    Console.WriteLine("Registros inseridos com sucesso!");
        }
    }
}
