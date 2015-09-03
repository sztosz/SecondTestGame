using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.TileEngine {
    public class TileMap {
        private List<Tileset> _tilesets;
        private List<MapLayer> _mapLayers;
        private static int _mapWidth;
        private static int _mapHeight;
        public static int WidthInPixels => _mapWidth * Engine.TileWidth;
        public static int HeightInPixels => _mapHeight * Engine.TileWidth;

        public TileMap(List<Tileset> tilesets, List<MapLayer> mapLayers) {
            _tilesets = tilesets;
            _mapLayers = mapLayers;

            _mapWidth = mapLayers[0].Width;
            _mapHeight = mapLayers[0].Height;

            if (mapLayers.Any(t => _mapWidth != t.Width || _mapHeight != t.Height)) {
                throw new Exception("Map layers size are not uniformed");
            }
        }

        public TileMap(Tileset tileset, MapLayer mapLayer) {
            _tilesets = new List<Tileset> {tileset};
            _mapLayers = new List<MapLayer> {mapLayer};

            _mapWidth = _mapLayers[0].Width;
            _mapHeight = _mapLayers[0].Height;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera) {
            var destination = new Rectangle(0, 0, Engine.TileWidth, Engine.TileHeight);

            foreach (var layer in _mapLayers) {
                for (var y = 0; y < layer.Height; y++) {
                    destination.Y = (int) (y * Engine.TileHeight - camera.Position.Y);
                    for (var x = 0; x < layer.Width; x++) {
                        var tile = layer.GetTile(x, y);
                        if (tile.TileIndex == -1 || tile.Tileset == -1) {
                            continue;
                        }
                        destination.X = (int) (x * Engine.TileWidth - camera.Position.X);

                        spriteBatch.Draw(
                            _tilesets[tile.Tileset].Texture,
                            destination,
                            _tilesets[tile.Tileset].SourceRectangles[tile.TileIndex],
                            Color.White);
                    }
                }
            }
        }

        public void AddLayer(MapLayer mapLayer) {
            if (mapLayer.Width != _mapWidth && mapLayer.Height != _mapHeight) {
                throw new Exception("Map layer size has incorect dimension");
            }
            _mapLayers.Add(mapLayer);
        }
    }
}