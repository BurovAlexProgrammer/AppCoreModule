using UnityEngine;

namespace AppCoreModule.Scripts.Extensions
{
    public static class UnityStructExtension
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static Color SetAlpha(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }

        public static string ToCapital(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = char.ToUpper(str[0]) + str[1..];
            }

            return str;
        }

        public static Vector3 SetNew(this Vector3 vector3, float x = float.NaN, float y = float.NaN, float z = float.NaN)
        {
            if (float.IsNaN(x) == false) vector3.x = x;
            if (float.IsNaN(y) == false) vector3.y = y;
            if (float.IsNaN(z) == false) vector3.z = z;

            return vector3;
        }

        public static Vector2 SetNew(this Vector2 vector2, float x = float.NaN, float y = float.NaN)
        {
            if (float.IsNaN(x) == false) vector2.x = x;
            if (float.IsNaN(y) == false) vector2.y = y;

            return vector2;
        }
    }
}