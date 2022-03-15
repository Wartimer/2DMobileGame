using Game.Car;
using Tool;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        internal CarType CarType;

        public ProfilePlayer(float carSpeed, GameState initialState) : this(carSpeed)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float carSpeed)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(carSpeed);
        }
    }
}
