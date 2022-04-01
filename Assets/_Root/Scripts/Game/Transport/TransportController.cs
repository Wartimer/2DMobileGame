using Features.AbilitySystem;
using Scripts.Enums;
using UnityEngine;

namespace Game.Transport
{
    internal abstract class TransportController : BaseController, IAbilityActivator
    {
        private readonly TransportFactory _transportFactory;
        private TransportModel _transportModel;
        protected readonly TransportView _view;

        internal TransportModel TransportModel => _transportModel;
        public TransportView Transport => _view;
        public float JumpHeight => _transportModel.JumpHeight;
        public abstract GameObject GameObjectView { get; } 

        public TransportController(TransportType type, TransportFactory transportFactory, TransportModel transportModel)
        {
            _transportFactory = transportFactory;
            _transportModel = transportModel;
            _view = LoadView(type);
        }

        private TransportView LoadView(TransportType type)
        {
            var transportConfig = _transportFactory.GetTransport(type);
            var objectView = Object.Instantiate(transportConfig.Prefab);
            AddGameObject(objectView);
            return objectView.GetComponent<TransportView>();
        }

    }
}