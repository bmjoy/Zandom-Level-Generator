using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZandomLevelGenerator.Enums;
using ZandomLevelGenerator.FinalObjects;

namespace ZandomLevelGenerator.BaseObjects
{
    public class Room
    {
        //public Room(int id, Vector2Int start, Vector2Int size, bool vertical, Room root, Room parent, Level level)
        public Room(int id, Level level, Vector2Int start, Vector2Int size, bool vertical, Room parent)
        {
            Id = id;
            Level = level;
            Start = start;
            Size = size;
            Vertical = vertical;
            Parent = parent;
            //TileMap = new();
            Tiles = new();
            Walls = new();
            Children = new();
            Type = RoomType.NORMAL;
            DefineRootAndAge();
        }

        public int Id { get; }
        public Level Level { get; }
        public Vector2Int Start { get; }
        public Vector2Int Size { get; }
        public bool Vertical { get; }
        //public Room Root { get; }
        public Room Parent { get; }
        //public TileMap TileMap { get; }
        public List<Tile> Tiles { get; }
        public List<Wall> Walls { get; }
        public List<Room> Children { get; }

        public RoomType Type { get; set; }
        public Room Root { get; private set; }
        public int Age { get; private set; }
        public FinalRoom GeneratedRoom { get; set; }
        public bool FromSetPiece { get; set; }

        public int Area
        {
            get
            {
                int x = Size.x;// - Start.x + 1;
                int y = Size.y;// - Start.y + 1;
                return x * y;
            }
        }

        public Vector3 Center
        {
            get
            {
                float x = (Size.x - 1) / 2F + Start.x;
                float z = (Size.y - 1) / 2F + Start.y;
                return new(x, 0, z);
            }
        }

        public override string ToString()
        {
            return $"Room #{Id} \'{Type}\'";
        }

        public bool IsEnclosed()
        {
            bool hasParent = Parent != null;
            bool noChildren = Children.Count <= 0;
            return hasParent && noChildren;
        }

        private void DefineRootAndAge()
        {
            Root = null;
            Age = 0;
            if (Parent != null)
            {
                Root = Parent.Root ?? this;
                Age = Parent.Age + 1;
            }
        }
    }
}
