using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR

namespace tools
{
    [ExecuteInEditMode]
    
    public class TransformVisualiser : MonoBehaviour
    {
        [Tooltip("The number of individual lines drawn per parent-child.")]
        [SerializeField] [Range(3, 100)] int vertexCount = 30;
        [Tooltip("Can lines be seen through nearby rendered objects.")]
        [SerializeField] bool alwaysVisible = true;
        [Tooltip("The size of the visualisation cone.")]
        [SerializeField] [Range(0.001f, 0.1f)] float radius = 0.05f;
        [Tooltip("Should longer connections appear larger.")]
        [SerializeField] bool radiusFunctionOfLength = false;

        List<Color> transformColor = new List<Color>();


        void Start()
        {
            transformColor.Add(Color.blue);
            transformColor.Add(Color.cyan);
        }


        void OnEnable() 
        {
            Start();
        }


        void Update()
        {
            DrawChildren(transform, 0);
        }


        void DrawChildren(Transform parent, int colorID)
        {
            int nextColorID = (int) Mathf.Pow(0, colorID);
            foreach (Transform child in parent)
            {
                DrawLines(parent.position, child.position, transformColor[colorID]);
                DrawChildren(child, nextColorID);
            }
        }


        void DrawLines(Vector3 startPos, Vector3 endPos, Color lineColor)
        {
            Vector3 lookingAt = endPos - startPos;
            Vector3 k = lookingAt.normalized;
            Vector3 i = new Vector3(k.z, k.z, -k.x-k.y).normalized;
            if (i.magnitude == 0) i = new Vector3(-k.y-k.z, k.x, k.x).normalized;
            Vector3 j = Vector3.Cross(k, i).normalized;

            float angleIncrement = 2 * Mathf.PI / vertexCount;

            float localRadius;
            if (radiusFunctionOfLength) localRadius = lookingAt.magnitude * radius;
            else localRadius = radius;

            float angle;
            Vector3 offsetStart;
            for (int vertexID = 0; vertexID < vertexCount; vertexID++)
            {
                angle = vertexID * angleIncrement;
                offsetStart = startPos + localRadius * (Mathf.Cos(angle) * i + Mathf.Sin(angle) * j);
                Debug.DrawLine(offsetStart, endPos, lineColor, 0, alwaysVisible);
            }
        }

    }
}
#endif
