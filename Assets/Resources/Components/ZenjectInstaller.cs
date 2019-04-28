using Resources.Core;
using Zenject;

namespace Resources.Components {
    public class ZenjectInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IController>().To<Controller>().AsSingle();
            Container.Bind<IGameScore>().To<GameScore>().AsSingle();
            Container.Bind<ISpawner>().To<Spawner>().AsSingle();
        }
    }
}