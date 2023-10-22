using UnityEngine;

namespace Utilities
{
    public static class VectorUtilities
    {
        public static Vector2 GetUnitDirection(Vector2 startPosition, Vector2 endPosition)
        {
            Vector2 direction = endPosition - startPosition;
            Vector2 unitDirection = direction.normalized;
            return unitDirection;
        }
    }
}