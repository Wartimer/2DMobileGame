using Tool;
using UnityEngine;

namespace Game.Car
{
    internal sealed class SchoolBusInitController: BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Cars/SchoolBus");
        private readonly CarView _view;

        public CarView Car => _view;

        public SchoolBusInitController()
        {
            _view = LoadView();
        }

        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}