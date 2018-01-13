using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGame
{
    public class KeyboardHelper : GameInvisibleEntity
    {
        private KeyboardState PreviousState, CurrentState;

        public override void Update(GameTime gameTime)
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
            base.Update(gameTime);
        }

        internal bool IsKeyPressed(Keys key)
        {
            if (PreviousState.IsKeyDown(key) && CurrentState.IsKeyUp(key))
                return CurrentState.IsKeyUp(key);
                return false;
        }

        public Keys getKeyUp()
        {
            Keys[] key = CurrentState.GetPressedKeys();
            //if (CurrentState.IsKeyUp(key[key.Length - 1]))
            //    return key[key.Length - 1];
            return key[0];
        }

        public bool isPressAnykey()
        {
            return CurrentState.GetPressedKeys().Length > 0;
        }
    }
}