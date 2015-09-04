using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XRpgLibrary.Controls {
    public class LeftRightSelector : Control {
        private readonly Texture2D _leftTexture;
        private int _maxItemWidth;
        private readonly Texture2D _rightTexture;
        private int _selectedItem;
        private readonly Texture2D _stopTexture;

        public Color SelectedColor { get; set; } = Color.Red;

        public int SelectedIndex {
            get { return _selectedItem; }
            set { _selectedItem = (int) MathHelper.Clamp(value, 0f, Items.Count); }
        }

        private List<string> Items { get; } = new List<string>();

        public string SelectedItem => Items[_selectedItem];

        public event EventHandler SelectionChanged;

        public LeftRightSelector(Texture2D leftTexture, Texture2D rightTexture, Texture2D stopTexture) {
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _stopTexture = stopTexture;
            TabStop = true;
            Color = Color.White;
        }

        public override void Update(GameTime gameTime) {}

        public override void Draw(SpriteBatch spriteBatch) {
            var drawTo = Position;
            spriteBatch.Draw(_selectedItem != 0 ? _leftTexture : _stopTexture, drawTo, Color.White);
            drawTo.X += _leftTexture.Width + 5f;

            var itemWidth = SpriteFont.MeasureString(Items[_selectedItem]).X;
            var offset = (_maxItemWidth - itemWidth) / 2;
            drawTo.X += offset;
            spriteBatch.DrawString(SpriteFont, Items[_selectedItem], drawTo, HasFocus ? SelectedColor : Color);

            drawTo.X += -1 * offset + _maxItemWidth + 5f;
            spriteBatch.Draw(_selectedItem != Items.Count - 1 ? _rightTexture : _stopTexture, drawTo, Color.White);
        }

        public override void HandleInput(PlayerIndex playerIndex) {
            if (Items.Count == 0) {
                return;
                ;
            }

            if (InputHandler.KeyReleased(Keys.Left)) {
                _selectedItem--;
                if (_selectedItem < 0) {
                    _selectedItem = 0;
                }
                OnSelectionChanged();
            }
            if (InputHandler.KeyReleased(Keys.Right)) {
                _selectedItem++;
                if (_selectedItem >= Items.Count) {
                    _selectedItem = Items.Count - 1;
                }
                OnSelectionChanged();
            }
        }

        public void SetItems(IEnumerable<string> items, int maxWidth) {
            Items.Clear();
            foreach (var s in items) {
                Items.Add(s);
            }
            _maxItemWidth = maxWidth;
        }

        protected virtual void OnSelectionChanged() {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}