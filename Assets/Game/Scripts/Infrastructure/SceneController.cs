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
    }

    public class SceneController : IInitializable, ISceneController
    {
        [Inject] private SceneConfiguration _sceneConfiguration;
        [Inject] private LoadingScreen _loadingScreen;
        private Queue<AsyncOperation> _loadingQueue = new Queue<AsyncOperation>();
        private List<string> _loadedScenes = new List<string>();
        public void LoadMenu(){}

        public void LoadGameScene()
        {
            var gameSceneLoading =
                SceneManager.LoadSceneAsync(_sceneConfiguration.gameSceneName, LoadSceneMode.Additive);
            _loadingQueue.Enqueue(gameSceneLoading);
            
            _loadingScreen.Loading(_loadingQueue, () =>
            {
                Debug.Log("Scenes loading complete");
                _loadedScenes.Add(_sceneConfiguration.gameSceneName);
            });
        }
        
        private void LoadLevel(string levelName)
        {
            
        }

        public void Initialize()
        {
            LoadGameScene();
        }
    }
}