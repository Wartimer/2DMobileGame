using Game.Car.TransportRepository;
using Scripts.Enums;
using Tool;

namespace Game.Transport
{
    internal class TransportCharacteristicsFactory : ITransportCharFactory
    {
        private readonly ResourcePath _transportCharacteristicsDataPath = new ResourcePath("Configs/Transport/TransportCharacteristicsDataSource");
        private TransportCharacteristicsDataSource _transportCharacteristicsDataSource;
        
        internal TransportCharacteristicsFactory()=>
            _transportCharacteristicsDataSource = ResourcesLoader.LoadObject<TransportCharacteristicsDataSource>(_transportCharacteristicsDataPath);


        public TransportCharacteristicsConfig GetTransportCharacteristics(TransportType type) =>
            _transportCharacteristicsDataSource.GetTransportCharacteristics(type);
    }
}