using AetherMail.Contracts;
using MassTransit;

namespace AetherMail.Web
{
    public class MassTransitPublisher(IPublishEndpoint publishEndpoint) : IMessagePublisher
    {
        public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
