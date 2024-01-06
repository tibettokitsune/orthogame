using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class UnitBar : MonoBehaviour
    {
        private Transform _target;
        [SerializeField] private Image[] sliders;

        public void UpdateTarget(Transform target)
        {
            _target = target;
        }
        public void UpdateData(float[] args)
        {
            var i = 0;
            foreach (var slider in sliders)
            {
                slider.fillAmount = args[i++];
                slider.color = Color.Lerp(Color.red, Color.green, slider.fillAmount);
            }
        }

        private void Update()
        {
            transform.position = Camera.main.WorldToScreenPoint(_target.position);
            
            var rt = (RectTransform) transform;
            rt.anchoredPosition += Vector2.up * UnityEngine.Screen.height * 0.04f;
        }
    }
}