using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
   internal sealed class InputKeyboardView : BaseInputView
    {
        private void Start() =>
            UpdateManager.SubscribeToUpdate(GetAxis);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(GetAxis);


        private void GetAxis()
        {
            float axisOffset = Input.GetAxis("Horizontal");
            float moveValue = _speed * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }

    }
}