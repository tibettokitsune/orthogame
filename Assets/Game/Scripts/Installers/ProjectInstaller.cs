using Game.Scripts.Metagameplay.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfiguration playerConfiguration;
        public override void InstallBindings()
        {
            Container.BindInstance(playerConfiguration);
        }
    }
}