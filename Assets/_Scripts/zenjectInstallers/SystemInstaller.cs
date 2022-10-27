using Project.Systems.Inventory;
using Zenject;

namespace Project.Zenject
{
    public class SystemInstaller : MonoInstaller<SystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerActionMaps>().AsCached();
            Container.Bind<Inventory>().AsCached();
        }
    }
}
