using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace SecondTestGame.GameScreens {
    public class StartMenuScreen : BaseGameState {
        private PictureBox _backgroundImage;
        private PictureBox _arrowImage;
        private LinkLabel _startGame;
        private LinkLabel _loadGame;
        private LinkLabel _exitGame;
        private float _maxItemWidth;


        public StartMenuScreen(Game game, GameStateManager manager) : base(game, manager) {}

        protected override void LoadContent() {
            base.LoadContent();

            var content = Game.Content;

            _backgroundImage = new PictureBox(
                content.Load<Texture2D>(@"Backgrounds\startScreen"),
                GameRef.ScreenRectangle);
            ControlManager.Add(_backgroundImage);

            var arrowTexture = content.Load<Texture2D>(@"GUI\leftarrowUp");
            _arrowImage = new PictureBox(
                arrowTexture,
                new Rectangle(0, 0, arrowTexture.Width, arrowTexture.Height));
            ControlManager.Add(_arrowImage);

            _startGame = new LinkLabel {Text = "The story begins"};
            _startGame.Size = _startGame.SpriteFont.MeasureString(_startGame.Text);
            _startGame.Selected += menuItem_Selected;
            ControlManager.Add(_startGame);

            _loadGame = new LinkLabel {Text = "The story continues"};
            _loadGame.Size = _loadGame.SpriteFont.MeasureString(_loadGame.Text);
            _loadGame.Selected += menuItem_Selected;
            ControlManager.Add(_loadGame);

            _exitGame = new LinkLabel {Text = "The story ends"};
            _exitGame.Size = _exitGame.SpriteFont.MeasureString(_exitGame.Text);
            _exitGame.Selected += menuItem_Selected;
            ControlManager.Add(_exitGame);

            ControlManager.NextControl();

            ControlManager.FocusChanged += ControlManager_FocusChanged;

            var position = new Vector2(350, 500);
            foreach (var c in ControlManager.OfType<LinkLabel>()) {
                if (c.Size.X > _maxItemWidth) {
                    _maxItemWidth = c.Size.X;
                }
                c.Position = position;
                position.Y += c.Size.Y + 5f;
            }

            ControlManager_FocusChanged(_startGame, EventArgs.Empty);
        }

        private void ControlManager_FocusChanged(object sender, EventArgs e) {
            var control = sender as Control;
            if (control != null) {
                _arrowImage.SetPosition(new Vector2(control.Position.X + _maxItemWidth + 10f, control.Position.Y));
            }
        }

        private void menuItem_Selected(object sender, EventArgs e) {
            if (sender == _startGame) {
                StateManager.PushState(GameRef.CharacterGeneratorScreen);
            }
            if (sender == _loadGame) {
                StateManager.PushState(GameRef.GamePlayScreen);
            }
            if (sender == _exitGame) {
                GameRef.Exit();
            }
        }

        public override void Update(GameTime gameTime) {
            ControlManager.Update(gameTime, PlayerIndexInControl);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }
    }
}