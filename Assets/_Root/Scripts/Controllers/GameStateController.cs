using Ui;
using Game;
using Profile;
using UnityEngine;

internal class GameStateController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private CarSelectController _carSelectController;
    
    private GameInitController _gameInitController;
    private CarType _carType;

    public GameStateController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                DisposeAllControllers();
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                DisposeAllControllers();
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                DisposeAllControllers();
                _gameInitController = new GameInitController(_profilePlayer);
                break;
            case GameState.SelectCar:
                DisposeAllControllers();
                _carSelectController = new CarSelectController(_placeForUi, _profilePlayer);
                break;
            default:
                DisposeAllControllers();
                break;
        }
    }

    private void DisposeAllControllers()
    {
        _mainMenuController?.Dispose();
        _gameInitController?.Dispose();
        _settingsMenuController?.Dispose();
        _carSelectController?.Dispose();
    }
    
    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameInitController?.Dispose();

        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }
}
