using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZandomLevelGenerator.GeneratorObjects
{
    public class RectSectorPlan : SectorPlan
    {
        public RectSectorPlan(LevelPlan levelPlan, int id, SectorPlan parent = null) : base(levelPlan, id, parent)
        {
        }
    }
}
