using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float CarSpeed = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private GameStateController _gameStateController;


    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(CarSpeed, InitialState);
        _gameStateController = new GameStateController(_placeForUi, profilePlayer);
    }

    private void OnDestroy()
    {
        _gameStateController.Dispose();
    }
}
