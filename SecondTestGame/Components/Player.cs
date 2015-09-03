using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary.TileEngine;

namespace SecondTestGame.Components {
    public class Player {
        private Game1 _gameRef;
        public Camera Camera { get; set; }

        public Player(Game game) {
            _gameRef = (Game1) game;
            Camera = new Camera(_gameRef.ScreenRectangle);
        }

        public void Update(GameTime gameTime) {
            Camera.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
    }
}