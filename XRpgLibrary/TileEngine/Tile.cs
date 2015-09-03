using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRpgLibrary.TileEngine {
    public class Tile {
        public int TileIndex { get; private set; }
        public int Tileset { get; private set; }

        public Tile(int tileIndex, int tileset) {
            TileIndex = tileIndex;
            Tileset = tileset;
        }
    }
}