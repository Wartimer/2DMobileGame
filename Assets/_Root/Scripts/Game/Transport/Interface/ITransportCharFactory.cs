using Game.Car.TransportRepository;
using Scripts.Enums;

namespace Game.Transport
{
    internal interface ITransportCharFactory
    {
        TransportCharacteristicsConfig GetTransportCharacteristics(TransportType type);
    }
}