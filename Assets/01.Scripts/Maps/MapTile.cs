using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIVIX.Map
{
    public enum MapTileType
    {
        Top,
        Bottom,
        Obstacle
    }

    public struct MapTile
    {
        public MapTileType TileType { get; set; }
        public Transform TileTransform { get; set; }
        public bool IsStretching { get; set; }
    }
}

