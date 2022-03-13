using Tool;
using UnityEngine;

namespace Game.Car
{
    internal class RedCarInitController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Cars/RedCar");
        private readonly CarView _view;

        public CarView Car => _view;

        public RedCarInitController()
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
