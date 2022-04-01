using Inventory.Items;
using Game.Car.TransportRepository;
using Game.Transport;
using Tool;

namespace Scripts.Enums
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        private TransportModel _currentTransport;
        public readonly InventoryModel Inventory;

        internal TransportModel CurrentTransport => _currentTransport;

        public ProfilePlayer(GameState initialState)
        {
            CurrentState = new SubscriptionProperty<GameState>(initialState);
            _currentTransport = new TransportModel();
            Inventory = new InventoryModel();
        }
    }
}
