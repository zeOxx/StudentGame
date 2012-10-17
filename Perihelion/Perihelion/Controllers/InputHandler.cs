using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework;

namespace Perihelion
{
    class InputHandler
    {
        // Sets vibration to 0. CURRENTLY NOT IN USE!
        private float vibrationAmount = 0.0f;

        // Sets up different states for the gamepad
        static GamePadState currentGamePadState;
        static GamePadState lastGamePadState;
        // Sets up different states for the keyboard
        static KeyboardState currentKeyboardState;
        static KeyboardState lastKeyboardState;

        // NOT 100% SURE ON WETHER THESE ARE NEEDED OR IF IT IS JUST AN EXTRA THING
        //  BEGIN UNSUREDNESS
        public static KeyboardState KeyboardState
        {
            get { return currentKeyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        public static GamePadState GamePadState
        {
            get { return currentGamePadState; }
        }

        public static GamePadState LastGamePadState
        {
            get { return lastGamePadState; }
        }
        // END UNSUREDNESS

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public InputHandler()
        {
            currentKeyboardState = Keyboard.GetState();

            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void updateInput()
        {
            // Updates the current and last keyboardstates
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            // Updates the current and last gamepadstates
            lastGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        /************************************************************************/
        /* KEYBOARD METHODS                                                     */
        /************************************************************************/
        public void Flush()
        {
            lastKeyboardState = currentKeyboardState;
        }

        public bool KeyReleased(Keys key)
        {
            // Returns if the released key was the same key that was pressed
            return currentKeyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        public bool KeyDown(Keys key)
        {
            // Returns the key that is currently pressed down
            return currentKeyboardState.IsKeyDown(key);
        }



        /************************************************************************/
        /* GAMEPAD METHODS                                                      */
        /************************************************************************/
        public bool ButtonReleased(Buttons button)
        {
            // Returns if the released button was the same button that was pressed
            return currentGamePadState.IsButtonUp(button) && lastGamePadState.IsButtonDown(button);
        }

        public bool ButtonPressed(Buttons button)
        {
            // Returns if the pressed button was not already being pressed
            return currentGamePadState.IsButtonDown(button) && lastGamePadState.IsButtonUp(button);
        }

        public bool ButtonDown(Buttons button)
        {
            // Returns the button that is being pressed down.
            return currentGamePadState.IsButtonDown(button);
        }

        // TEMP STICK HANDLING
        public Vector2 getMovementInputFromPlayer()
        {
            Vector2 movementInput = new Vector2(currentGamePadState.ThumbSticks.Left.X,
                                                currentGamePadState.ThumbSticks.Left.Y);
            return movementInput;
        }

        public Vector2 getShootingInputFromPlayer()
        {
            Vector2 movementInput = new Vector2(currentGamePadState.ThumbSticks.Right.X,
                                                currentGamePadState.ThumbSticks.Right.Y);
            return movementInput;
        }

        public float updateLeftStickX()
        {
            return currentGamePadState.ThumbSticks.Left.X;
        }

        public float updateLeftStickY()
        {
            return currentGamePadState.ThumbSticks.Left.Y;
        }

        public float updateRightStickX()
        {
            return currentGamePadState.ThumbSticks.Right.X;
        }

        public float updateRightStickY()
        {
            return currentGamePadState.ThumbSticks.Right.Y;
        }
    }
}
