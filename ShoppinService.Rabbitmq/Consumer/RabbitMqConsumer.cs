using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitmqService.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Newtonsoft.Json;
using DataAccess.UoW;
using Serilog;

namespace RabbitmqService.Consumer
{
    public class RabbitMqConsumer
    {
        public RabbitMqConsumer()
        {
        }
        private IModel channel;

        private EventingBasicConsumer eventingBasicConsumer;

        private RabbitMqConnection rabbitMqServices;

        public RabbitMqConnection RabbitMqServices
        {
            get
            {
                if (rabbitMqServices == null)
                {
                    rabbitMqServices = new RabbitMqConnection();
                }
                return rabbitMqServices;

            }
            set => rabbitMqServices = value;
        }

        public void RabbitmqConsumer(string queue)
        {
            using (var connection = RabbitMqServices.GetConnection())
            {
                try
                {
                    if (string.IsNullOrEmpty(queue))
                    {
                        Log.Information("Consumer QueueName boştu");
                    }
                    channel = RabbitMqServices.GetChannel(queue);
                    channel.BasicQos(0, 250, false);
                    eventingBasicConsumer = new EventingBasicConsumer(channel);
                    eventingBasicConsumer.Received += EventingBasicConsumerOnReceived;
                    channel.BasicConsume(queue, false, eventingBasicConsumer);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "RabbitMQ/RabbitmqConsumer");
                }

            }
        }
        private void EventingBasicConsumerOnReceived(object sender, BasicDeliverEventArgs e)
        {
            Basket model = JsonConvert.DeserializeObject<Basket>(Encoding.UTF8.GetString(e.Body.ToArray()));
            try
            {
                using (var uow = new UnitofWork())
                {
                    uow.GetRepository<Basket>().Add(model);
                    uow.SaveChange();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Consumer kayıt edemedi!");
            }
            channel.BasicAck(e.DeliveryTag, true);
        }
    }
}
