using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace SecondTestGame.GameScreens {
    public class CharacterGeneratorScreen : BaseGameState {
        private PictureBox _backgroundImage;
        private LeftRightSelector _classSelector;
        private LeftRightSelector _genderSelector;
        private readonly string[] _genderItems = {"Male", "Female"};
        private readonly string[] _classItems = {"Fighter", "Wizard", "Rouge", "Priest"};

        public CharacterGeneratorScreen(Game game, GameStateManager manager) : base(game, manager) {}

        protected override void LoadContent() {
            base.LoadContent();
            CreateControls();
        }

        public override void Update(GameTime gameTime) {
            ControlManager.Update(gameTime, PlayerIndex.One);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        private void CreateControls() {
            var leftTexture = Game.Content.Load<Texture2D>(@"GUI\leftarrowUp");
            var rightTexture = Game.Content.Load<Texture2D>(@"GUI\rightarrowUp");
            var stopTexture = Game.Content.Load<Texture2D>(@"GUI\StopBar");

            _backgroundImage = new PictureBox(
                Game.Content.Load<Texture2D>(@"Backgrounds\startScreen"),
                GameRef.ScreenRectangle);

            var label1 = new Label {
                Text = "Who shall Thy be?"
            };
            label1.Size = label1.SpriteFont.MeasureString(label1.Text);
            label1.Position = new Vector2((GameRef.Window.ClientBounds.Width - label1.Size.X) / 2, 150);
            ControlManager.Add(label1);

            _genderSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            _genderSelector.SetItems(_genderItems, 125);
            _genderSelector.Position = new Vector2(label1.Position.X, 200);
            ControlManager.Add(_genderSelector);

            _classSelector = new LeftRightSelector(leftTexture, rightTexture, stopTexture);
            _classSelector.SetItems(_classItems, 125);
            _classSelector.Position = new Vector2(label1.Position.X, 250);
            ControlManager.Add(_classSelector);

            var linklabel1 = new LinkLabel {Text = "Accept", Position = new Vector2(label1.Position.X, 300)};
            linklabel1.Selected += new EventHandler(linklabel1_Selected);
            ControlManager.Add(linklabel1);

            ControlManager.NextControl();
        }

        private void linklabel1_Selected(object sender, EventArgs e) {
            InputHandler.Flush();

            StateManager.PopState();
            StateManager.PushState(GameRef.GamePlayScreen);
        }
    }
}