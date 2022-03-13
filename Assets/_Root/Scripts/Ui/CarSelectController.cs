using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class CarSelectController: BaseController
    {
        private readonly ResourcePath _carSelectView = new ResourcePath("Prefabs/UI/selectCarMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly CarSelectView _view;


        public CarSelectController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.ReturnToMainMenuInit(MainMenu);
            _view.StartGameInit(StartGame);
            _view.SelectRedCar(SetRedCar);
            _view.SelectSchoolBus(SetSchoolBus);
        }


        private CarSelectView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_carSelectView);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CarSelectView>();
        }

        private void SetSchoolBus()
        {
            _profilePlayer.CarType = CarType.SchoolBus;
            PrintText(_profilePlayer.CarType.ToString());
        }
        
        private void SetRedCar(){
            
            _profilePlayer.CarType = CarType.RedCar;
            PrintText(_profilePlayer.CarType.ToString());
        }

        private void StartGame()
        {
            if (_profilePlayer.CarType == CarType.None)
            {
                PrintText("Select your car");
                return;
            }
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void MainMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;

        private void PrintText(string text)
        {
            _view.Text.text = text;
        }
    }
}