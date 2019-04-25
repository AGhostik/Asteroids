using Resources.Components;
using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings() {
        Container.Bind<IController>().To<Controller>().AsSingle();
    }
}