using Microsoft.Xna.Framework;

namespace XRpgLibrary.TileEngine {
    public class Engine {
        private static int _tileWidth;
        private static int _tileHeight;

        public static int TileWidth => _tileWidth;

        public static int TileHeight => _tileHeight;

        public Engine(int tileWidth, int tileHeight) {
            Engine._tileWidth = tileWidth;
            Engine._tileHeight = tileHeight;
        }

        public static Point VectorToCell(Vector2 position)
            => new Point((int) position.X / _tileWidth, (int) position.Y / _tileHeight);
    }
}