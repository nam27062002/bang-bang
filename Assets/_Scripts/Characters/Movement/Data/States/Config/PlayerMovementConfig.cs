using System;
using UnityEngine;
namespace MovementSystem
{
    [Serializable]
    public class PlayerMovementConfig
    {
        [Range(0f,30f)] public float baseSpeed = 20f;
        public PlayerRunningConfig runningConfig;
        public PlayerDashingConfig dashingConfig;
    }
}