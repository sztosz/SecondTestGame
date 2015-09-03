using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XRpgLibrary.TileEngine {
    public class Tileset {
        private Rectangle[] _sourceRectangles;

        public Texture2D Texture { get; set; }

        public int TileWidthInPixels { get; set; }

        public int TileHeigthInPixels { get; set; }

        public int TilesWide { get; set; }

        public int TilesHigh { get; set; }

        public Rectangle[] SourceRectangles => (Rectangle[]) _sourceRectangles.Clone();

        public Tileset(Texture2D texture, int tilesWide, int tilesHigh, int tileWidthInPixels, int tileHeigthInPixels) {
            Texture = texture;
            TileWidthInPixels = tileWidthInPixels;
            TileHeigthInPixels = tileHeigthInPixels;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;

            _sourceRectangles = new Rectangle[tilesWide * tilesHigh];

            var tiles = tilesWide * tilesHigh;
            var tile = 0;
            for (var y = 0; y < tilesHigh; y++) {
                for (var x = 0; x < tilesWide; x++) {
                    _sourceRectangles[tile] = new Rectangle(
                        x * tileWidthInPixels,
                        y * tileHeigthInPixels,
                        tileWidthInPixels,
                        tileHeigthInPixels);
                    tile++;
                }
            }
        }
    }
}