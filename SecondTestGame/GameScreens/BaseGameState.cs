using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using XRpgLibrary;
using XRpgLibrary.Controls;

namespace SecondTestGame.GameScreens {
    public abstract partial class BaseGameState : GameState {
        protected readonly Game1 GameRef;
        protected ControlManager ControlManager;
        protected PlayerIndex PlayerIndexInControl;

        protected BaseGameState(Game game, GameStateManager manager) : base(game, manager) {
            GameRef = (Game1) game;
            PlayerIndexInControl = PlayerIndex.One;
        }

        protected override void LoadContent() {
            var content = Game.Content;
            var menuFont = content.Load<SpriteFont>(@"Fonts\ControlFont");
            ControlManager = new ControlManager(menuFont);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }
    }
}