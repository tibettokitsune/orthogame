using Game.Scripts.UI;
using Infrastructure;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingScreen>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<SceneController>().AsSingle();
        }
    }
}