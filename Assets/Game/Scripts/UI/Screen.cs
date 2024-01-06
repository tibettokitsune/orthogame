using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private void OnValidate()
        {
            if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        }

        [Button]
        public virtual void Open()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        [Button]
        public virtual void Close()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}