using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZandomLevelGenerator.Customizables;
using ZandomLevelGenerator.GeneratorObjects;
using ZandomLevelGenerator.Tools.Checkers;
using ZandomLevelGenerator.Tools.Factories;
using ZandomLevelGenerator.Tools.Helpers;

namespace ZandomLevelGenerator.Tasks.Common
{
    public class CreateBuddingRooms : GeneratorTask
    {
        public CreateBuddingRooms(ZandomLevelGenerator zandomLevelGenerator, CreateBuddingRoomsParameters parameters) : base(zandomLevelGenerator)
        {
            Parameters = parameters;
            NewRooms = new();
        }

        public CreateBuddingRoomsParameters Parameters { get; }
        public List<RoomPlan> NewRooms { get; }

        public List<RoomPlan> RootSectors { get; private set; }
        public Queue<RoomPlan> LoopRooms { get; private set; }

        public override void RunContents()
        {
            RootSectors = Parameters.RootRoomsFunction(ZandomLevelGenerator);
            LoopRooms = new(RootSectors);
            RoomPlan current = null;
            while (!Parameters.TaskStopFunction(ZandomLevelGenerator, current) && LoopRooms.Count > 0)
            {
                current = LoopRooms.Dequeue();
                if (!IsCurrentValid(current)) continue;
                List<RoomPlan> currentChilds = BudRoom(current);
                currentChilds = currentChilds.OrderBy(x => ZandomLevelGenerator.GeneratorCoroutine.SeededRandom.Next()).ToList();
                foreach (var item in currentChilds)
                {
                    if (item == null) continue;
                    LoopRooms.Enqueue(item);
                    NewRooms.Add(item);
                }
                //TODO: wait type item, wait per room created here
            }
        }

        private bool IsCurrentValid(RoomPlan current)
        {
            if (current == null) return false;
            bool tooFar = new SafetyBoundsChecker().IsWithinSafetyBounds(current);
            if (tooFar) return false;
            return true;
        }

        private List<RoomPlan> BudRoom(RoomPlan current)
        {
            Vector3Int size = Parameters.RoomSizeFunction(ZandomLevelGenerator, current);
            bool vertical = Parameters.RoomVerticalFunction(ZandomLevelGenerator, current);
            bool canRetry = Parameters.RetryFunction(ZandomLevelGenerator, current);
            List<RoomPlan> result = BudRoomAlongAxis(current, size, vertical);
            if (result.Count <= 0 && canRetry) result = BudRoomAlongAxis(current, size, !vertical);
            return result;
        }

        private List<RoomPlan> BudRoomAlongAxis(RoomPlan parent, Vector3Int size, bool vertical)
        {
            CreateBuddingRoomsPositionPicker positionPicker = new(ZandomLevelGenerator);
            Vector3Int position1 = vertical ? positionPicker.BackRandom(parent, size) : positionPicker.LeftRandom(parent, size);
            Vector3Int position2 = vertical ? positionPicker.FrontRandom(parent, size) : positionPicker.RightRandom(parent, size);
            List<RoomPlan> result = new()
            {
                Run(position1, size, vertical, parent),
                Run(position2, size, vertical, parent),
            };
            return result;
        }

        private RoomPlan Run(Vector3Int position, Vector3Int size, bool vertical, SectorPlan parent)
        {
            RoomPlan result = null;
            LevelPlan levelPlan = ZandomLevelGenerator.GeneratorCoroutine.Level;
            HashSet<Vector3Int> coordinates = new CoordinatesGetter().Get(position, size);
            bool canBuild = new AreaAvailabilityChecker(levelPlan).IsAvailableForTiles(coordinates);
            if (canBuild)
            {
                RoomPlanFactory factory = new(levelPlan);
                int roomId = factory.NextId();
                result = factory.Create(roomId, position, size, vertical, parent);
            }
            return result;

        }
    }
}
