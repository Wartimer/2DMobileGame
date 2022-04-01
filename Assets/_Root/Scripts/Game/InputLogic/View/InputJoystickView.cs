using JoostenProductions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal sealed class InputJoystickView : BaseInputView, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private Joystick _joystick;
        
        [SerializeField] private float _inputMultiplier = 10;
        
        private bool _usingJoystick;

        internal void SetJoystick(Joystick joystick)
        {
            _joystick = joystick;
        }
        
        protected override void Move(float deltaTime)
        {
            if (!_usingJoystick) return;
            
            float axisOffset = CrossPlatformInputManager.GetAxis("Horizontal");
            float moveValue = _inputMultiplier * deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);
            
            Debug.Log($"{nameof(gameObject)}: {abs}");
            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _joystick.transform.position = (eventData.position);
            _joystick.SetStartPosition(eventData.position);
            _joystick.OnPointerDown(eventData);
            StartUsing();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.OnPointerUp(eventData);
            FinishUsing();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _joystick.OnDrag(eventData);
        }
        
        private void StartUsing()
        {
            _usingJoystick = true;
            SetActive(true);
        }

        private void FinishUsing()
        {
            _usingJoystick = false;
            SetActive(false);
        }

        private void SetActive(bool active) =>
            _joystick.gameObject.SetActive(active);
    }
}
