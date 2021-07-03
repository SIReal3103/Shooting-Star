using UnityEngine;

namespace ANTs
{
    public static class ANTsUtility
    {
        public static float ConvertVectorToAngle(Vector3 vector)
        {
            float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
            return angle;
        }
    }
}
