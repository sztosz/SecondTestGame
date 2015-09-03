using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SecondTestGame.Components;
using XRpgLibrary;
using XRpgLibrary.TileEngine;

namespace SecondTestGame.GameScreens {
    public class GamePlayScreen : BaseGameState {
        private Engine _engine = new Engine(32, 32);
        private TileMap _map;
        private Player _player;

        public GamePlayScreen(Game game, GameStateManager manager) : base(game, manager) {
            _player = new Player(game);
        }

        protected override void LoadContent() {
            base.LoadContent();

            var tilesets = new List<Tileset>();

            var tileset1 = Game.Content.Load<Texture2D>(@"Tilesets\tileset1");
            var tileset2 = Game.Content.Load<Texture2D>(@"Tilesets\tileset2");
            tilesets.Add(new Tileset(tileset1, 8, 8, 32, 32));
            tilesets.Add(new Tileset(tileset2, 8, 8, 32, 32));

            var layer = new MapLayer(40, 40);
            for (var y = 0; y < layer.Height; y++) {
                for (var x = 0; x < layer.Width; x++) {
                    var tile = new Tile(0, 0);
                    layer.SetTile(x, y, tile);
                }
            }

            var splatter = new MapLayer(40, 40);
            var random = new Random();

            for (var i = 0; i < 80; i++) {
                var x = random.Next(0, 40);
                var y = random.Next(0, 40);
                var index = random.Next(2, 14);

                splatter.SetTile(x, y, new Tile(index, 0));
            }

            splatter.SetTile(1, 0, new Tile(0, 1));
            splatter.SetTile(2, 0, new Tile(2, 1));
            splatter.SetTile(3, 0, new Tile(0, 1));

            var mapLayers = new List<MapLayer> {layer, splatter};
            _map = new TileMap(tilesets, mapLayers);
        }

        public override void Update(GameTime gameTime) {
            _player.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            GameRef.SpriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Matrix.Identity);
            _map.Draw(GameRef.SpriteBatch, _player.Camera);
            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }
    }
}