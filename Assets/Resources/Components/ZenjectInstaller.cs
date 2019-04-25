using Zenject;

namespace Resources.Components {
    public class ZenjectInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.Bind<IController>().To<Controller>().AsSingle();
        }
    }
}