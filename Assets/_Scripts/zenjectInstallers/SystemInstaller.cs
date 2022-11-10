using Project.Systems.Equipment;
using Project.Systems.SoundSystem;
using UnityEngine;
using Zenject;

namespace Project.Zenject
{
    public class SystemInstaller : MonoInstaller<SystemInstaller>
    {
        [SerializeField]private SoundManager soundManager;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerActionMaps>().AsCached();
            Container.Bind<Inventory>().AsCached();
            Container.Bind<SoundManager>().FromInstance(soundManager);
        }
    }
}
