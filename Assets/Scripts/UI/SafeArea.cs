using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ZooWorld.UI
{
    [ExecuteAlways]
    public sealed class SafeArea : MonoBehaviour
    {
        private RectTransform rect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            Apply();
        }

        private void OnEnable()
        {
            Apply();
        }

#if UNITY_EDITOR
        private void Update()
        {
            Apply();
        }
#endif

        private void Apply()
        {
            if (rect == null) rect = GetComponent<RectTransform>();

            var safe = Screen.safeArea;

            var anchorMin = safe.position;
            var anchorMax = safe.position + safe.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }
}

