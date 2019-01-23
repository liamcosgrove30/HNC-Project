using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace LiamCOOPAssessment
{
    public class InputManager
    {
        KeyboardState prevKeyState, currKeyState;

        public KeyboardState PrevKeyState
        {
            get { return prevKeyState; }
            set { prevKeyState = value; }
        }

        public KeyboardState CurrKeyState
        {
            get { return currKeyState; }
            set { currKeyState = value; }
        }

        public void Update()
        {
            prevKeyState = currKeyState;
            currKeyState = Keyboard.GetState();
        }

        public bool KeyPressed(Keys key)
        {
            if (currKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            return false;
        }

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if(currKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                        return true;                    
            }
            return false;
        }

        public bool KeyReleased(Keys key)
        {
            if (currKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(Keys key)
        {
            if (currKeyState.IsKeyDown(key))
                return true;
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

    }
}
