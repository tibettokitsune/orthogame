using Game.Scripts.Metagameplay.Player;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfiguration playerConfiguration;
        [SerializeField] private SceneConfiguration sceneConfiguration;
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfiguration);
            Container.BindInstance(sceneConfiguration);
        }
    }
}