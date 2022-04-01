using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
   internal sealed class InputKeyboardView : BaseInputView
   {
        protected override void Move(float deltaTime)
        {
            float axisOffset = Input.GetAxis("Horizontal");
            float moveValue = _speed * deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }
   }
}