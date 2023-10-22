using System;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerRunningConfig
    {
        [Range(1f,2f)] public float speedModifier = 1f; 
    }
}