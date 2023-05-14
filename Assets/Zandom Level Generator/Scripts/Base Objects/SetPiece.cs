using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZandomLevelGenerator.Helpers;

namespace ZandomLevelGenerator.BaseObjects
{
    public abstract class SetPiece
    {
        public char[,] Layout { get; protected set; }
        public Vector2Int Size { get => new Vector2Int(Layout.GetLength(0), Layout.GetLength(1)); }

        public SetPiece()
        {
            string layoutString = GetString();
            CreateLayout();
            FillLayout(layoutString);
        }

        public char Get(int x, int y) => Layout[x, y];

        public void Rotate()
        {
            SetPieceRotator setPieceRotator = new(this);
            Layout = setPieceRotator.Rotate90Negative();
        }

        private void FillLayout(string layoutString)
        {
            Queue<char> chars = new(layoutString.ToCharArray());
            for (int row = 0; row < Size.y; row++)
            {
                for (int col = 0; col < Size.x; col++)
                {
                    char next = chars.Dequeue();
                    Layout[col, row] = next;
                }
            }
        }

        protected abstract string GetString();
        protected abstract void CreateLayout();
        protected abstract bool AddBorders();
    }
}
