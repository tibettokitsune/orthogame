using Infrastructure;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_OSX
            Container.Bind<IPlayerInput>().To<PCPlayerInput>().AsSingle();
#endif

            Container.Bind<Antigravity>().FromComponentInHierarchy().AsSingle();
        }
    }
}