using Zenject;

namespace Project.Zenject
{
    public class SystemInstaller : MonoInstaller<SystemInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInputActions>().AsCached();
        }
    }
}
