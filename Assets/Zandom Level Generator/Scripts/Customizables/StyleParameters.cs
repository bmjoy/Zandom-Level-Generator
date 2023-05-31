using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZandomLevelGenerator.Customizables
{
    [CreateAssetMenu(menuName = "Zandom2/StyleParameters")]
    public class StyleParameters : ScriptableObject
    {
        [Header("Level Size")]
        [SerializeField] private readonly bool avoidSafetyBounds = true;
        [SerializeField][Range(0F, 1F)] private readonly float levelSizeTarget = 0.5F;

        public bool AvoidSafetyBounds => avoidSafetyBounds;
        public float LevelSizeTarget => levelSizeTarget;
    }
}
