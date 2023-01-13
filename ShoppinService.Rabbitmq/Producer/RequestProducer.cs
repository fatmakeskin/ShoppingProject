using RabbitmqService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmqService.Producer
{
    public class RequestProducer
    {
        public void PublishData(string message, string queueName)
        {
            RabbitMqConnection rabbitmqConnection = new RabbitMqConnection();
            var body = Encoding.UTF8.GetBytes(message);
            rabbitmqConnection.GetChannel(queueName).BasicPublish("", "ApiQueue", false, null, body);

        }
    }
}
