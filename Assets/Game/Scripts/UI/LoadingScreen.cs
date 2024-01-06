using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class LoadingScreen : Screen
    {
        [SerializeField] private Image loadingBar;


        public void Loading(Queue<AsyncOperation> loadingStack, Action onComplete)
        {
            StartCoroutine(LoadingProgress(loadingStack, onComplete));
        }

        private IEnumerator LoadingProgress(Queue<AsyncOperation> loadingStack, Action onComplete)
        {
            Open();

            
            
            while (loadingStack.Count > 0)
            {
                var op = loadingStack.Dequeue();
                while (!op.isDone)
                {
                    loadingBar.fillAmount = op.progress;
                    yield return null;
                }
            }

            float fakeTimer = 0f;
            while (fakeTimer < 3f)
            {
                fakeTimer += Time.deltaTime;
                loadingBar.fillAmount = fakeTimer / 3f;
                yield return null;
            }
            
            onComplete?.Invoke();
            Close();
        }
    }
}