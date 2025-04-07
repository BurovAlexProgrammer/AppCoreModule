using System;
using System.Globalization;
using UnityEngine;

namespace AppCoreModule.Scripts.Extensions
{
    public static class UnityStructExtension
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static Color SetNewAlpha(this Color color, float alpha)
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
        
        public static Color FromHex(this Color color, string hex)
        {
            if (hex.Length<6)
            {
                throw new System.FormatException("Needs a string with a length of at least 6");
            }

            var r = hex.Substring(0, 2);
            var g = hex.Substring(2, 2);
            var b = hex.Substring(4, 2);
            var alpha = hex.Length >= 8 ? hex.Substring(6, 2) : "FF";

            return new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
                (int.Parse(g, NumberStyles.HexNumber) / 255f),
                (int.Parse(b, NumberStyles.HexNumber) / 255f),
                (int.Parse(alpha, NumberStyles.HexNumber) / 255f));
        }
    }
}