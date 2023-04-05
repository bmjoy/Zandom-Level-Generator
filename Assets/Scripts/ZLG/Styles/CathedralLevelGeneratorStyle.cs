using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CathedralLevelGeneratorStyle : LevelGeneratorStyle
{
    protected override bool Step01_PreDungeon_Execution(out string message)
    {
        message = null;
        //new StartingRoom(LevelGenerator).Run();
        new CathedralSpine(LevelGenerator).Run();
        List<Room> startingBuddingRooms = new(LevelGenerator.Level.Rooms.Values.Take(2));
        new BuddingRooms(LevelGenerator, startingBuddingRooms).Run();
        new WallFinder(LevelGenerator).Run();
        AreaSumChecker dungeonAreaChecker = new(LevelGenerator);
        bool levelAreaMin = dungeonAreaChecker.CheckMin(out int current, out int minimum);
        message += $"\nLevel area at {current} / minimum required at {minimum}";
        return levelAreaMin;// &&;
    }

    protected override bool Step02_Dungeon_Execution(out string message)
    {
        message = null;
        new RoomTypeRandomizer(LevelGenerator).Run();
        new WallTypeRandomizer(LevelGenerator).Run();
        new DoorSpawner(LevelGenerator).Run();
        return true;
    }

    protected override bool Step03_PostDungeon_Execution(out string message)
    {
        message = null;
        new FinalRooms(LevelGenerator).Run();
        new FinalTiles(LevelGenerator).Run();
        return true;
    }
}
