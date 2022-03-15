using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class MainMenuController : BaseController
    {
        private readonly ResourcePath _mainMenuViewPath = new ResourcePath("Prefabs/UI/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;


        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.CarSelectInit(CarSelect);
            _view.OpenSettingsInit(Settings);
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_mainMenuViewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void CarSelect() =>
            _profilePlayer.CurrentState.Value = GameState.SelectCar;

        private void Settings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

    }
}
