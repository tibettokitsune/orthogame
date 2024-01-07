using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI
{
    public class MenuScreen : Screen
    {
        [Inject] private ISceneController _sceneController;
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button exitBtn;
        
        public override void Start()
        {
            base.Start();
            startGameBtn.onClick.AddListener(StartGame);
            settingsBtn.onClick.AddListener(OpenSettings);
            exitBtn.onClick.AddListener(ExitGame);
        }

        private void OpenSettings()
        {
            throw new NotImplementedException();
        }

        private void ExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            return;
            #endif
            Application.Quit();
        }

        private void StartGame()
        {
            _sceneController.LoadGameScene();
        }
    }
}