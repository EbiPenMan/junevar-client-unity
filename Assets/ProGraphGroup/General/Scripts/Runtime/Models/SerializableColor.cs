using Newtonsoft.Json;
using UnityEngine;

namespace ProGraphGroup.General.Models
{
    [System.Serializable]
    public class SerializableColor
    {
        public float r, g, b, a;

        public Color GetColor() => new Color(r, g, b, a);

        public void SetColor(Color color)
        {
            r = color.r;
            g = color.g;
            b = color.b;
            a = color.a;
        }

        public SerializableColor()
        {
            r = g = b = a = 1f;
        }

        public SerializableColor(Color color) : this(color.r, color.g, color.b, color.a)
        {
        }

        [JsonConstructor]
        public SerializableColor(float r, float g, float b, float a = 1f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public SerializableColor(string hexColor ,float a = 1f)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                color.a = a;
                SetColor(color);
            }
        }
    }
}