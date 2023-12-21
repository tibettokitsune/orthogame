using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GroundCheker>().FromComponentInChildren().AsSingle();
            Container.Bind<ObjectOfGravity>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Rigidbody>().FromComponentOn(gameObject).AsSingle();

            Container.Bind<MoveAndRotation>().FromComponentOn(gameObject).AsSingle();
        }
    }
}