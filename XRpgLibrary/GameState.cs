using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace XRpgLibrary {
    public abstract partial class GameState : DrawableGameComponent {
        private List<GameComponent> _childComponents;
        private GameState _tag;
        public List<GameComponent> ChildComponents => _childComponents;
        public GameState Tag => _tag;
        protected GameStateManager StateManager;

        public GameState(Game game, GameStateManager manager) : base(game) {
            StateManager = manager;
            _childComponents = new List<GameComponent>();
            _tag = this;
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            foreach (var component in _childComponents.Where(component => component.Enabled)) {
                component.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime) {
            foreach (
                var component in _childComponents.OfType<DrawableGameComponent>().Where(component => component.Visible)) {
                component.Draw(gameTime);
            }
        }

        protected internal virtual void StateChange(object sender, EventArgs e) {
            if (StateManager.CurrentState == Tag) {
                Show();
            }
            else {
                Hide();
            }
        }

        protected virtual void Show() {
            Visible = true;
            Enabled = true;

            foreach (var component in _childComponents) {
                component.Enabled = true;
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null) {
                    gameComponent.Visible = true;
                }
            }
        }

        protected virtual void Hide() {
            Visible = false;
            Enabled = false;

            foreach (var component in _childComponents) {
                component.Enabled = false;
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null) {
                    gameComponent.Visible = false;
                }
            }
        }
    }
}