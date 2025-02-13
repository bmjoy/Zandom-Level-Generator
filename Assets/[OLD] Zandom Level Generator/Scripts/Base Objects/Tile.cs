using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZandomLevelGenerator.Enums;

namespace ZandomLevelGenerator.BaseObjects
{
    public class Tile
    {
        public Tile(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            MentionedRooms = new();
            Obstacles = new();
        }

        public Vector2Int Coordinates { get; }

        public bool FromSetPiece { get; set; }
        public TileType Type { get; set; }
        public List<Room> MentionedRooms { get; set; }
        public bool Vertical { get; set; }
        public List<Obstacle> Obstacles { get; set; }
        public GameObject GeneratedTile { get; set; }

        public override string ToString()
        {
            return $"Tile {Coordinates} \'{Type}\'";
        }

        public bool HasObstacle()
        {
            return Obstacles.Count > 0;
        }
    }
}
