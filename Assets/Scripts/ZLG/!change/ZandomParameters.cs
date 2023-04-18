using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZandomParameters : ScriptableObject
{
    [Header("Level")]
    public bool avoidSizeBoundaries = true;

    [Header("Room")]
    [Min(0)] public int roomAgeMax = 8;

    [Header("Wall")]
    [Min(0)] public int neighborAgeMin = 4;
    [Min(0)] public int smallDoorLengthMin = 4;
    [Min(0)] public int largeDoorLengthMax = 8;

    [Header("Requirements")]
    [Range(0F, 1F)] public float levelSizeMin = 0.4F;
    [Range(0F, 1F)] public float levelSizeMax = 0.6F;

    [Header("Targets")]
    [Range(0F, 1F)] public float levelSizeTarget = 0.5F;
    [Min(0)] public int specialRoomTarget = 8;
}
