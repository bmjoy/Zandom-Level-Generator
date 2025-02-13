using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZandomLevelGenerator.Customizables;
using ZandomLevelGenerator.GeneratorObjects;
using ZandomLevelGenerator.Tools.Factories;

namespace ZandomLevelGenerator.Tasks.Common
{
    public class PlaceStartLocation : GeneratorTask
    {
        public PlaceStartLocation(ZandomLevelGenerator zandomLevelGenerator, Func<Vector3> positionFunction, Func<Obstacle> obstacleFunction) : base(zandomLevelGenerator)
        {
            PositionFunction = positionFunction;
            ObstacleFunction = obstacleFunction;
        }

        public Func<Vector3> PositionFunction { get; }
        public Func<Obstacle> ObstacleFunction { get; }

        public override void RunContents()
        {
            Vector3 position = PositionFunction();
            Obstacle obstacle = ObstacleFunction();
            new StartLocationFactory(ZandomLevelGenerator.GeneratorCoroutine.Level).Create(position, obstacle);
        }
    }
}
