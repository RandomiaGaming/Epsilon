using System;
namespace Epsilon
{
    public sealed class InputManager
    {
        private readonly StagePlayer stagePlayer;
        public InputManager(StagePlayer stagePlayer)
        {
            if(stagePlayer is null)
            {
                throw new NullReferenceException();
            }
            this.stagePlayer = stagePlayer;
        }
        public bool jumpDown = false;
        public bool jumpHeld = false;
        public bool jumpUp = false;
        public bool leftDown = false;
        public bool leftHeld = false;
        public bool leftUp = false;
        public bool rightDown = false;
        public bool rightHeld = false;
        public bool rightUp = false;
        public int moveAxis = 0;

        private bool jumpHeldLastFrame = false;
        private bool leftHeldLastFrame = false;
        private bool rightHeldLastFrame = false;
        public void UpdateInput(InputPacket packet)
        {
            if (packet == null || packet.keyboardState == null)
            {
                jumpHeld = false;
                leftHeld = false;
                rightHeld = false;
            }
            else
            {
                jumpHeld = packet.keyboardState.GetKeyboardButtonState(KeyboardButton.Space);
                leftHeld = packet.keyboardState.GetKeyboardButtonState(KeyboardButton.A);
                rightHeld = packet.keyboardState.GetKeyboardButtonState(KeyboardButton.D);
            }

            if (jumpHeld && !jumpHeldLastFrame)
            {
                jumpDown = true;
            }
            else
            {
                jumpDown = false;
            }
            if (rightHeld && !rightHeldLastFrame)
            {
                rightDown = true;
            }
            else
            {
                rightDown = false;
            }
            if (leftHeld && !leftHeldLastFrame)
            {
                leftDown = true;
            }
            else
            {
                leftDown = false;
            }

            if (!jumpHeld && jumpHeldLastFrame)
            {
                jumpUp = true;
            }
            else
            {
                jumpUp = false;
            }
            if (!rightHeld && rightHeldLastFrame)
            {
                rightUp = true;
            }
            else
            {
                rightUp = false;
            }
            if (!leftHeld && leftHeldLastFrame)
            {
                leftUp = true;
            }
            else
            {
                leftUp = false;
            }

            if (rightHeld && !leftHeld)
            {
                moveAxis = 1;
            }
            else if (leftHeld && !rightHeld)
            {
                moveAxis = -1;
            }
            else
            {
                moveAxis = 0;
            }

            jumpHeldLastFrame = jumpHeld;
            leftHeldLastFrame = leftHeld;
            rightHeldLastFrame = rightHeld;
        }
    }
}
