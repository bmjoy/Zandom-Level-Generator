using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZandomLevelGenerator.BaseObjects;
using ZandomLevelGenerator.Enums;

namespace ZandomLevelGenerator.Task
{
    public class GenerateFinalLevel : LevelGeneratorTask
    {
        public GenerateFinalLevel(LevelGenerator levelGenerator) : base(levelGenerator)
        {
        }

        public override IEnumerator Run()
        {
            Level level = LevelGenerator.Level;
            if (LevelGenerator.taskWaitSetting == TaskWaitSettings.PER_ITERATION)
            {
                //Debug.LogWarning("Generator tasks can produce duplicated results when taskWaitingTier > 0.");
                Debug.LogWarning("Skipping GenerateFinalLevel since by task waiting you should have everything already done.");
                yield break;
            }
            foreach (Room room in level.Rooms.Values)
            {
                yield return new GenerateFinalRoom(LevelGenerator, room).Run();
            }
            yield return new GenerateFinalTiles(LevelGenerator).Run();
            yield return new GenerateFinalObstacles(LevelGenerator).Run();
        }
    }
}
