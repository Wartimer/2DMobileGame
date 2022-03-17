using Game.Car;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _pcInputPath = new ResourcePath("Prefabs/Inputs/PCKeyboardInput");
        private readonly ResourcePath _joysticInputPath = new ResourcePath("Prefabs/Inputs/Joystick");
       
        private BaseInputView _view;

        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            CarModel car)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, car.Speed);

            // var joystickPrefab = ResourcesLoader.LoadPrefab(_joysticInputPath);
            // var joystick = Object.Instantiate(joystickPrefab, placeForUi);
            // placeForUi.gameObject.AddComponent<InputJoystickView>().SetJoystick((joystick.GetComponent<Joystick>()));
            // placeForUi.gameObject.GetComponent<InputJoystickView>().Init(leftMove, rightMove, car.Speed);

        }

        private BaseInputView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_pcInputPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            BaseInputView view = objectView.GetComponent<BaseInputView>();
            return view;
        }
    }
}
