using Game.Scripts.Metagameplay.CombatSystem;
using Game.Scripts.Metagameplay.Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GroundChecker>().FromComponentInChildren().AsSingle();
            Container.Bind<ObjectOfGravity>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Rigidbody>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Animator>().FromComponentInChildren().AsSingle();
            Container.Bind<CharacterAnimationController>().FromComponentInChildren().AsSingle();
            Container.Bind<MoveAndRotation>().FromComponentOn(gameObject).AsSingle();

            Container.Bind<DamagePoint>().FromComponentsInChildren().AsSingle();
        }
    }
}