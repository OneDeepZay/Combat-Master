using System;
using UnityEngine;

namespace Combat
{
    internal class Drawing
    {
        public static Camera GameCamera = Camera.main;
        public static GUIStyle StringStyle { get; set; } = new GUIStyle(GUI.skin.label);

        public static void DrawString(Vector2 position, string label, Color color, int fontSize, bool centered = true)
        {
            GUI.color = color;
            Drawing.StringStyle.fontSize = fontSize;
            Drawing.StringStyle.normal.textColor = color;
            GUIContent content = new GUIContent(label);
            Vector2 vector = Drawing.StringStyle.CalcSize(content);
            GUI.Label(new Rect(centered ? (position - vector / 2f) : position, vector), content, Drawing.StringStyle);
        }

        public static void TextWithDistance(Transform target, string text, Camera relativeTo = null)
        {
            var camera = relativeTo ?? GameCamera;

            Vector3 position = target.position;
            Vector3 vector = camera.WorldToScreenPoint(position);

            if ((vector.x < 0f || vector.x > (float)Screen.width || vector.y < 0f || vector.y > (float)Screen.height || vector.z > 0f))
            {
                float distanceToMonster = (float)Math.Round((double)Vector3.Distance(Main.IPlayerState.Transform.Position, target.position), 1);

                if (vector.z >= 0f && distanceToMonster < 125f)
                {
                    DrawString(new Vector2(vector.x, (float)Screen.height - vector.y), text + " [" + distanceToMonster.ToString() + "m]", Color.red, 12, true);
                }
            }
        }
    }
}