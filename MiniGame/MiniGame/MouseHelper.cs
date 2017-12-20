using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class MouseHelper : GameInvisibleEntity
    {
        MouseState previousState, currentState;
        private static float zoomValue = 1f;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (previousState == null)
                previousState = currentState = Mouse.GetState();
            else
            {
                previousState = currentState;
                currentState = Mouse.GetState();
            }
        }

        public Vector2 getCurrentMousePosition()
        {
            return new Vector2(currentState.X, currentState.Y);
        }

        public float getZoomValue()
        {
            float d = (currentState.ScrollWheelValue - previousState.ScrollWheelValue) / 12000f;

            if ((d < 0 && zoomValue > 0.1) || (d > 0 && zoomValue < 2))
                //{
                zoomValue += d;
            return zoomValue;
            //}
            //else
            //    return -1;
        }
        internal bool isZooming()
        {
            return currentState.ScrollWheelValue != previousState.ScrollWheelValue;
        }

        public Vector2 getDifference()
        {
            float dx = currentState.X - previousState.X;
            float dy = currentState.Y - previousState.Y;

            return new Vector2(dx, dy);
        }
        #region mouse even
        public Boolean isLButtonDown()
        {
            return currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released;
        }

        public Boolean isLButtonHold()
        {
            return currentState.LeftButton == ButtonState.Pressed;
        }

        public Boolean isLButtonUp()
        {
            return currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed;
        }

        public Boolean isRButtonDown()
        {
            return currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released;
        }

        public Boolean isRButtonHold()
        {
            return currentState.RightButton == ButtonState.Pressed;
        }

        public Boolean isRButtonUp()
        {
            return currentState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed;
        }
        #endregion
    }
}