using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Pay(string product)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092"
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(
                        "Delivery",
                        new Message<Null, string>
                        { Value = product });
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}