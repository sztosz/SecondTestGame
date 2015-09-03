namespace XRpgLibrary.TileEngine {
    public class MapLayer {
        private Tile[,] _map;
        public int Width => _map.GetLength(1);
        public int Height => _map.GetLength(0);

        public MapLayer(Tile[,] map) {
            _map = (Tile[,]) map.Clone();
        }

        public MapLayer(int width, int height) {
            _map = new Tile[height, width];
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    _map[y, x] = new Tile(0, 0);
                }
            }
        }

        public Tile GetTile(int x, int y) => _map[y, x];
        public void SetTile(int x, int y, Tile tile) => _map[y, x] = tile;
        public void SetTile(int x, int y, int tileIndex, int tileset) => _map[y, x] = new Tile(tileIndex, tileset);
    }
}