using System.Linq;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn;
using Tallaks.ArcheroTest.Runtime.Gameplay.Battle.Spawn.Factories;
using UnityEngine;
using Zenject;

namespace Tallaks.ArcheroTest.Runtime.Infrastructure.Installers
{
  public class LevelInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private Transform _charactersParent;
    [SerializeField] private HeroSpawnPoint _heroSpawnPoint;

#if UNITY_EDITOR
    private void Awake()
    {
      Debug.Assert(GetComponent<SceneContext>().Installers.Contains(this),
        $"Forgot to add installer {name} to SceneContext");
    }
#endif

    public void Initialize()
    {
      Debug.Log("Level initialized");
      Container.Resolve<HeroFactory>().Create(_heroSpawnPoint, _charactersParent);
      EnemySpawnPoint[] enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
      foreach (EnemySpawnPoint enemySpawnPoint in enemySpawnPoints)
        Container.Resolve<EnemyFactory>().Create(enemySpawnPoint, _charactersParent);
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<LevelInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind<HeroFactory>()
        .AsSingle();

      Container
        .Bind<EnemyFactory>()
        .AsSingle();
    }
  }
}