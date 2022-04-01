using Scripts.Enums;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal sealed class SettingsMenuController : BaseController
    {
        private readonly ResourcePath _settingsMenuPath = new ResourcePath("Prefabs/UI/settingsMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly SettingsMenuView _view;

        public SettingsMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(MainMenu);
        }

        private SettingsMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_settingsMenuPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<SettingsMenuView>();
        }

        private void MainMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}