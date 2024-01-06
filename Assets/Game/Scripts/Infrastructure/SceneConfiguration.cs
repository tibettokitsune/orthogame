#if UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64
using UnityEditor;
#endif
using UnityEngine;
using Sirenix.OdinInspector;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/Scenes")]
    public class SceneConfiguration : ScriptableObject
    {
#if UNITY_EDITOR || UNITY_EDITOR_OSX || UNITY_EDITOR_64

        [OnValueChanged("OnChange")]
        public SceneAsset menuScene;
        public SceneAsset gameScene;

#endif

        [ReadOnly] public string menuSceneName;
        [ReadOnly] public string gameSceneName;


        private void OnChange()
        {
            menuSceneName = menuScene.name;
            gameSceneName = gameScene.name;
        }
    }
}