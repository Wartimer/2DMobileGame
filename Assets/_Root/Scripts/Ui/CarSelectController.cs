using Game.Transport;
using Scripts.Enums;
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
        private TransportCharacteristicsFactory _transportCharFactory;


        public CarSelectController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.ReturnToMainMenuInit(MainMenu);
            _view.StartGameInit(StartGame);
            _view.SelectRedCar(SetRedCar);
            _view.SelectSchoolBus(SetSchoolBus);
            _view.OpenUpgradesInit(OpenShed);
            _transportCharFactory = new TransportCharacteristicsFactory();
        }


        private CarSelectView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_carSelectView);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<CarSelectView>();
        }
        private void MainMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
        
        private void OpenShed()
        {
            if (_profilePlayer.CurrentTransport.Type == TransportType.None)
            {
                PrintText("Select your car");
                return;
            }
            _profilePlayer.CurrentState.Value = GameState.Shed;
        }
        
        private void StartGame()
        {
            if (_profilePlayer.CurrentTransport.Type == TransportType.None)
            {
                PrintText("Select your car");
                return;
            }
            _profilePlayer.CurrentState.Value = GameState.Game;
        }

        private void SetSchoolBus()
        {
            SetTransportStats(TransportType.SchoolBus);
            PrintText(_profilePlayer.CurrentTransport.Type.ToString());
        }


        private void SetRedCar(){
            
            SetTransportStats(TransportType.RedJeep);
            PrintText(_profilePlayer.CurrentTransport.Type.ToString());
        }


        private void SetTransportStats(TransportType type)
        {
            _profilePlayer.CurrentTransport.SetTransportType(type);
            _profilePlayer.CurrentTransport.TransportModelInit(_transportCharFactory.GetTransportCharacteristics(type).Speed,
                    _transportCharFactory.GetTransportCharacteristics(type).JumpHeight);
        }

        private void PrintText(string text)
        {
            _view.Text.text = text;
        }
    }
}