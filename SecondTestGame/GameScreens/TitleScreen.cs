using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace SecondTestGame.GameScreens {
    public class TitleScreen : BaseGameState {
        private Texture2D _backgroundImage;
        private LinkLabel _startLabel;
        public TitleScreen(Game game, GameStateManager manager) : base(game, manager) {}

        protected override void LoadContent() {
            var content = GameRef.Content;
            _backgroundImage = content.Load<Texture2D>(@"Backgrounds\titlescreen");
            base.LoadContent();

            _startLabel = new LinkLabel {
                Position = new Vector2(350, 600),
                Text = "Press ENTER to start",
                Color = Color.White,
                TabStop = true,
                HasFocus = true
            };
            _startLabel.Selected += _startLabel_Selected;

            ControlManager.Add(_startLabel);
        }

        public override void Update(GameTime gameTime) {
            ControlManager.Update(gameTime, PlayerIndex.One);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);
            GameRef.SpriteBatch.Draw(_backgroundImage, GameRef.ScreenRectangle, Color.White);
            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        private void _startLabel_Selected(object sender, EventArgs e) {
            StateManager.PushState(GameRef.StartMenuScreen);
        }
    }
}