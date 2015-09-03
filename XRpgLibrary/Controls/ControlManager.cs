using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XRpgLibrary.Controls {
    public class ControlManager : List<Control> {
        private int _selectedControl = 0;

        public static SpriteFont SpriteFont { get; private set; }
        public event EventHandler FocusChanged;

        public ControlManager(SpriteFont spriteFont) {
            SpriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, int capacity) : base(capacity) {
            SpriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection) : base(collection) {
            SpriteFont = spriteFont;
        }

        public void Update(GameTime gameTime, PlayerIndex playerIndex) {
            if (Count == 0) {
                return;
            }
            foreach (var c in this) {
                if (c.Enabled) {
                    c.Update(gameTime);
                }
                if (c.HasFocus) {
                    c.HandleInput(playerIndex);
                }
            }

            if (InputHandler.ButtonPressed(Buttons.LeftThumbstickUp, playerIndex) ||
                InputHandler.ButtonPressed(Buttons.DPadUp, playerIndex) ||
                InputHandler.KeyPressed(Keys.Up)) {
                PreviousControl();
            }
            if (InputHandler.ButtonPressed(Buttons.LeftThumbstickDown, playerIndex) ||
                InputHandler.ButtonPressed(Buttons.DPadDown, playerIndex) ||
                InputHandler.KeyPressed(Keys.Down)) {
                NextControl();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var c in this.Where(c => c.Visible)) {
                c.Draw(spriteBatch);
            }
        }

        public void NextControl() {
            if (Count == 0) {
                return;
            }

            var currentControl = _selectedControl;
            this[_selectedControl].HasFocus = false;

            do {
                _selectedControl++;

                if (_selectedControl == Count) {
                    _selectedControl = 0;
                }

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled) {
                    FocusChanged?.Invoke(this[_selectedControl], null);
                    break;
                }
            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }

        public void PreviousControl() {
            if (Count == 0) {
                return;
            }

            var currentControl = _selectedControl;
            this[_selectedControl].HasFocus = false;

            do {
                _selectedControl--;

                if (_selectedControl < 0) {
                    _selectedControl = Count - 1;
                }

                if (this[_selectedControl].TabStop && this[_selectedControl].Enabled) {
                    FocusChanged?.Invoke(this[_selectedControl], null);
                    break;
                }
            } while (currentControl != _selectedControl);

            this[_selectedControl].HasFocus = true;
        }
    }
}