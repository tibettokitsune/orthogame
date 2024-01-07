using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure
{
    public interface ISceneController
    {
        void LoadGameScene();
    }

    public class SceneController : IInitializable, ISceneController
    {
        [Inject] private SceneConfiguration _sceneConfiguration;
        [Inject] private LoadingScreen _loadingScreen;
        private Queue<AsyncOperation> _loadingQueue = new Queue<AsyncOperation>();
        private List<string> _loadedScenes = new List<string>();
        private void UnloadScenes(Action onUnloading)
        {
            if (_loadedScenes.Count <= 0)
            {
                Debug.Log("Nothing to unload");
                onUnloading?.Invoke();
                return;
            }
            
            while (_loadedScenes.Count > 0)
            {
                var unloadingOperation = SceneManager.UnloadSceneAsync(_loadedScenes[^1]);
                _loadedScenes.RemoveAt(_loadedScenes.Count - 1);
                _loadingQueue.Enqueue(unloadingOperation);
            }
            
            _loadingScreen.Loading(_loadingQueue, () =>
            {
                Debug.Log("Scenes unloading complete");
                onUnloading?.Invoke();
            });
        }
        public void LoadGameScene()
        {
            UnloadScenes(() =>
            {
                var gameSceneLoading =
                    SceneManager.LoadSceneAsync(_sceneConfiguration.gameSceneName, LoadSceneMode.Additive);
                _loadingQueue.Enqueue(gameSceneLoading);
            
                _loadingScreen.Loading(_loadingQueue, () =>
                {
                    Debug.Log("Scenes loading complete");
                    _loadedScenes.Add(_sceneConfiguration.gameSceneName);
                });
            });
        }

        public void LoadMenu()
        {
            UnloadScenes(() =>
            {
                var gameSceneLoading =
                    SceneManager.LoadSceneAsync(_sceneConfiguration.menuSceneName, LoadSceneMode.Additive);
                _loadingQueue.Enqueue(gameSceneLoading);
            
                _loadingScreen.Loading(_loadingQueue, () =>
                {
                    Debug.Log("Scenes loading complete");
                    _loadedScenes.Add(_sceneConfiguration.menuSceneName);
                });
            });
        }

        private void LoadLevel(string levelName)
        {
            
        }

        public void Initialize()
        {
            LoadMenu();
        }
    }
}