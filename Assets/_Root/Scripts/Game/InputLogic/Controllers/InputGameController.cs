using Game.Transport;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _pcInputPath = new ResourcePath("Prefabs/Inputs/PCKeyboardInput");
        private readonly ResourcePath _joysticInputPath = new ResourcePath("Prefabs/Inputs/MobileSingleStickControl");
       
        private BaseInputView _view;
        internal InputKeyboardView PCIntputView => _view as InputKeyboardView;

        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            TransportModel transport)
        {
            if(Application.platform == RuntimePlatform.WindowsEditor ||
               Application.platform == RuntimePlatform.WindowsPlayer)
                _view = LoadView(_pcInputPath);
            
            else _view = LoadView(_joysticInputPath);
            
            _view.Init(leftMove, rightMove, transport.Speed);
            
            // var joystickPrefab = ResourcesLoader.LoadPrefab(_joysticInputPath);
            // var joystick = Object.Instantiate(joystickPrefab, placeForUi);
            // placeForUi.gameObject.AddComponent<InputJoystickView>().SetJoystick((joystick.GetComponent<Joystick>()));
            // placeForUi.gameObject.GetComponent<InputJoystickView>().Init(leftMove, rightMove, car.Speed);
        }

        private BaseInputView LoadView(ResourcePath path)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(path);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            BaseInputView view = objectView.GetComponent<BaseInputView>();
            return view;
        }
    }
}
