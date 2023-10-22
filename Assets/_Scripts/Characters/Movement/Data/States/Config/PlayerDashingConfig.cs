using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerDashingConfig
    {
        [Range(1f, 5f)] public float speedModifier = 3f;
        [Range(0f, 1f)] public float dashTime = 0.3f;
        [Range(3f, 10f)] public float countdown = 5f;
    }
}