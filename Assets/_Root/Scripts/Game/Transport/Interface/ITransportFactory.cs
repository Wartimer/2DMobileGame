using Game.Car.TransportRepository;
using Scripts.Enums;

namespace Game.Transport
{
    internal interface ITransportFactory
    {
        TransportConfig GetTransport(TransportType type);
    }
}