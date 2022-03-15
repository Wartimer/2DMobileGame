using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tool;

namespace Game
{
    internal class GameInitController : BaseController
    {
        public GameInitController(ProfilePlayer profilePlayer)
        {
            var leftMoveDiff = new SubscriptionProperty<float>();
            var rightMoveDiff = new SubscriptionProperty<float>();

            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);
            
            switch (profilePlayer.CarType)
            {
                case CarType.RedCar:
                    AddController(new WheelsInitController(new RedCarInitController().Car, leftMoveDiff, rightMoveDiff));
                    break;
                case CarType.SchoolBus:
                    AddController(new WheelsInitController(new SchoolBusInitController().Car, leftMoveDiff, rightMoveDiff));
                    break;
            }
            
        }
    }
}
