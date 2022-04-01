using Ui;
using Tool;
using UnityEngine;
using Scripts.Enums;
using Game.Transport;
using Game.InputLogic;
using Services.Analytics;
using Game.TapeBackground;
using Features.AbilitySystem;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly ProfilePlayer _profilePlayer;
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;
        
        private readonly TapeBackgroundController _tapeBackgroundController;
        private readonly InputGameController _inputGameController;
        private readonly TransportController _transportController;
        private readonly IAbilitiesController _abilitiesController;
        
        public GameController(ProfilePlayer profilePlayer, AnalyticsManager analyticsManager, Transform placeForUi)
        {
            _profilePlayer = profilePlayer;
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();
            _tapeBackgroundController = CreateTapeBackground();
            _inputGameController = CreateInputGameController();
            _transportController = CreateTransportController(_profilePlayer.CurrentTransport);
            _abilitiesController = CreateAbilitiesController(placeForUi);

            analyticsManager.SendGameStarted();
        }


        private TapeBackgroundController CreateTapeBackground()
        {
            var tapeBackgroundController = new TapeBackgroundController(_leftMoveDiff, _rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }
        
        private InputGameController CreateInputGameController()
        {
            var inputGameController = new InputGameController(_leftMoveDiff, _rightMoveDiff, _profilePlayer.CurrentTransport);
            AddController(inputGameController);

            return inputGameController;
        }

        private TransportController CreateTransportController(TransportModel transportModel)
        {
            var carController = new CarController(_profilePlayer.CurrentTransport.Type, new TransportFactory(), transportModel,
                _leftMoveDiff, _rightMoveDiff, _inputGameController.PCIntputView);
            AddController(carController);

            return carController;
        }

        private IAbilitiesController CreateAbilitiesController(Transform placeForUi)
        {
            AbilityItemConfig[] abilityItemConfigs = LoadAbilityItemConfigs();
            var repository = CreateAbilitiesRepository(abilityItemConfigs);
            var view = LoadAbilitiesView(placeForUi);

            var abilitiesController =
                new AbilitiesController(view, repository, abilityItemConfigs, _transportController);
            return abilitiesController;
        }


        private AbilityItemConfig[] LoadAbilityItemConfigs()
        {
            var path = new ResourcePath("Configs/Abilities/" +
                                        "AbilityItemConfigDataSource");
            return ContentDataSourceLoader.LoadAbilityItemConfigs(path);
        }
        
        private AbilitiesRepository CreateAbilitiesRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            return repository;
        }
        
        private AbilitiesView LoadAbilitiesView(Transform placeForUi)
        {
            var path = new ResourcePath("Prefabs/Ability/AbilitiesView");

            GameObject prefab = ResourcesLoader.LoadPrefab(path);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
    
    
    
}
