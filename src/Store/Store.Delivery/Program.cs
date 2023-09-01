using Confluent.Kafka;

string bootstrapServers = "localhost:9092";
string nomeTopic = "Delivery";

Console.WriteLine($"BootstrapServers = {bootstrapServers}");
Console.WriteLine($"Topic = {nomeTopic}");

var config = new ConsumerConfig
{
    BootstrapServers = bootstrapServers,
    GroupId = $"{nomeTopic}-group-0",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
    {
        consumer.Subscribe(nomeTopic);

        try
        {
            while (true)
            {
                var cr = consumer.Consume(cts.Token);
                Console.WriteLine($"Pedido comprado: {cr.Message.Value} ");
                Console.WriteLine("Pedido saiu para entrega");
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
            Console.WriteLine("Cancelada a execução do Consumer...");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Exceção: {ex.GetType().FullName} | " +
                 $"Mensagem: {ex.Message}");
}
        
