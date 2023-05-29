using UnityEngine;

namespace Utils
{
    public static class Algorithms
    {
        /// <summary>
        /// Finds and returns the nearest angle from an array of possible angles to a target angle.
        /// </summary>
        /// <param name="possibleAngles">Array of angles to choose from</param>
        /// <param name="target">Angle to use when finding nearest possible angle</param>
        /// <returns>Nearest angle from the array of possible angles</returns>
        public static float FindNearestAngle(float[] possibleAngles, float target)
        { 
            // Define the nearest angle as the first angle in the array
            var nearestAngle = possibleAngles[0];
            var minDifference = Mathf.Abs(target - possibleAngles[0]);

            // Loop through the array of angles
            for (var i = 1; i < possibleAngles.Length; i++)
            {
                // Calculate the difference between the target angle and the current angle
                var newDifference = Mathf.Abs(target - possibleAngles[i]);

                // If the new difference is not smaller than the current minimum difference, continue
                if (!(newDifference < minDifference)) continue;
                
                // If the new difference is smaller than the current minimum difference, update the minimum difference
                minDifference = newDifference;
                nearestAngle = possibleAngles[i];
            }
            
            return nearestAngle;
        }

        /// <summary>
        /// Checks if a point is inside a circle.
        /// </summary>
        /// <param name="centre">Centre of the circle</param>
        /// <param name="point">The point used for checking</param>
        /// <param name="radius">The radius of the circle</param>
        /// <returns>True if the point is inside the circle, otherwise False</returns>
        public static bool IsInsideCircle(Vector2 centre, Vector2 point, float radius)
        {
            return Vector2.Distance(centre, point) <= radius;
        }

    }
}

