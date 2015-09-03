using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XRpgLibrary {
    public class InputHandler : GameComponent {
        private static KeyboardState _keyboardState;
        private static KeyboardState _lastKeyboardState;

        private static GamePadState[] _gamePadStates;
        private static GamePadState[] _lastGamePadStates;


        public static KeyboardState KeyboardState => _keyboardState;
        public static KeyboardState LastKeyboardState => _lastKeyboardState;
        public static GamePadState[] GamePadStates => _gamePadStates;
        public static GamePadState[] LastGamePadStates => _lastGamePadStates;


        public InputHandler(Game game) : base(game) {
            _keyboardState = Keyboard.GetState();
            _gamePadStates = new GamePadState[Enum.GetValues(typeof (PlayerIndex)).Length];
            GetGamePadStates();
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _lastGamePadStates = (GamePadState[]) _gamePadStates.Clone();
            GetGamePadStates();
            base.Update(gameTime);
        }

        private static void GetGamePadStates() {
            foreach (PlayerIndex index in Enum.GetValues(typeof (PlayerIndex))) {
                _gamePadStates[(int) index] = GamePad.GetState(index);
            }
        }

        public static void Flush() => _lastKeyboardState = _keyboardState;

        public static bool KeyReleased(Keys key) => _keyboardState.IsKeyUp(key) && _lastKeyboardState.IsKeyDown(key);

        public static bool KeyPressed(Keys key) => _keyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyUp(key);

        public static bool KeyDown(Keys key) => _keyboardState.IsKeyDown(key);

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
            => _gamePadStates[(int) index].IsButtonUp(button) &&
               _lastGamePadStates[(int) index].IsButtonDown(button);

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
            => _gamePadStates[(int) index].IsButtonDown(button) &&
               _lastGamePadStates[(int) index].IsButtonUp(button);

        public static bool ButtonDown(Buttons button, PlayerIndex index)
            => _gamePadStates[(int) index].IsButtonDown(button);
    }
}