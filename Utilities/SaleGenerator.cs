using Models;

namespace Utilities
{
    public class SaleGenerator
    {
        static Random random = new Random();
        static string[] paymentTypes = { "CreditCard", "Pix", "BankPaymentSlip" };
        public static Sale GenerateSale(List<Car> cars, List<Client> clients, List<Employee> employees)
        {
            string type = paymentTypes[random.Next(paymentTypes.Length - 1)];
            Sale sale = new Sale();

            sale.Car = cars[random.Next(cars.Count - 1)];
            sale.SaleDate = DateTime.Now;
            sale.Value = random.Next(10000, 100000);
            sale.Client = clients[random.Next(clients.Count - 1)];
            sale.Employee = employees[random.Next(employees.Count - 1)];
            sale.Payment = PaymentGenerator.PaymentGenerate(type);
            return sale;
        }
    }
}
