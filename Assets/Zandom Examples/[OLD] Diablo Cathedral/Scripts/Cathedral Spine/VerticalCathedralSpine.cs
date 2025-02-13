using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZandomLevelGenerator.BaseObjects;
using ZandomLevelGenerator.Components;

namespace ZandomLevelGenerator.Examples.DiabloCathedral
{
    public class VerticalCathedralSpine : AxisCathedralSpine
    {
        public VerticalCathedralSpine(LevelGenerator levelGenerator, int firstRoomPosition, int corridorLength) : base(levelGenerator, firstRoomPosition, corridorLength)
        {
            rotate = true;
        }

        protected override Vector2Int GetPositionRoom1()
        {
            return new(3, firstRoomPosition);
        }

        protected override Vector2Int GetPositionRoom2()
        {
            return new(3, firstRoomPosition + 2 + corridorLength);
        }

        protected override Vector2Int GetPositionCorridor(int index)
        {
            return new(3, index);
        }

        protected override Room Build(Vector2Int position, SetPiecePattern setPiece)
        {
            return new CathedralSpineExecutor(levelGenerator).Run(position, setPiece, true);
        }
    }
}
