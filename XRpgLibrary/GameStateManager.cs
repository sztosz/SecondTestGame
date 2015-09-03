using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XRpgLibrary {
    public class GameStateManager :GameComponent {
        public event EventHandler OnStateChange; // TODO: FIND OUT WHAT IT IS AND REPAIR
        private readonly Stack<GameState> _gameStates = new Stack<GameState>();

        private const int StartDrawOrder = 5000;
        private const int DrawOrderInc = 100;
        private int _drawOrder;

        public GameState CurrentState => _gameStates.Peek();

        public GameStateManager(Game game) : base(game) {
            _drawOrder = StartDrawOrder;
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public void PopState() {
            if (!(_gameStates?.Count > 0)) {
                return;
            }
            RemoveState();
            _drawOrder -= DrawOrderInc;
            OnStateChange?.Invoke(this, null);
        }

        private void RemoveState() {
            GameState state = _gameStates.Peek();
            OnStateChange -= state.StateChange;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void PushState(GameState newState) {
            _drawOrder += DrawOrderInc;
            newState.DrawOrder = _drawOrder;
            AddState(newState);
            OnStateChange?.Invoke(this, null);
        }

        private void AddState(GameState newState) {
            _gameStates.Push(newState);
            Game.Components.Add(newState);
            OnStateChange += newState.StateChange;
        }

        public void ChangeState(GameState newState) {
            while (_gameStates.Count > 0) {
                RemoveState();
            }

            newState.DrawOrder = StartDrawOrder;
            _drawOrder = StartDrawOrder;

            AddState(newState);

            OnStateChange?.Invoke(this, null);
        }
    }
}