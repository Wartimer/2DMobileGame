using Game.Car.TransportRepository;
using Tool;
using Scripts.Enums;


namespace Game.Transport
{
    internal class TransportFactory : ITransportFactory
    {
        private readonly ResourcePath _transportConfigDataSourcePath = new ResourcePath("Configs/Transport/TransportConfigDataSource");
        private TransportConfigDataSource _transportConfigDataSource;

        internal TransportFactory()
        {
            _transportConfigDataSource =
                ResourcesLoader.LoadObject<TransportConfigDataSource>(_transportConfigDataSourcePath);
        }

        public TransportConfig GetTransport(TransportType type) =>
            _transportConfigDataSource.GetTransport(type);
    }
}