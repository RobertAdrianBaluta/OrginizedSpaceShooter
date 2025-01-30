using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class EnemySpawnerSystem : MonoBehaviour
{
    public int entitiesCount = 10;

    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private GameObject enemyPrefab;

    void Start()
    {

        MakeEntities();
        OnDrawGizmos();
    }

    private void MakeEntities()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype archetype = entityManager.CreateArchetype(
        #region

            typeof(RenderMeshUnmanaged),
            typeof(RenderBounds),
        #endregion
            typeof(LocalToWorld),
            typeof(Translation),
            typeof(SpeedComponent)
        );


        for (int i = 0; i < entitiesCount; i++)
        {
            Entity entity = entityManager.CreateEntity(archetype);
            World world = World.DefaultGameObjectInjectionWorld;
            EntityManager entityManager1 = world.EntityManager;

            #region

            entityManager.SetComponentData(entity, new SpeedComponent { speed = 20.0f });

            float3 randomPosition = new float3(
                          UnityEngine.Random.Range(-10f, 10f),
                           UnityEngine.Random.Range(-10f, 10f),
                          0f // Random Z
                          );

            entityManager.SetComponentData(entity, new Translation { Value = randomPosition });

            RenderMeshUnmanaged renderMeshUnmanaged = new RenderMeshUnmanaged
            {
                mesh = enemyMesh,
                materialForSubMesh = enemyMaterial,
            };

            entityManager.SetComponentData(entity, renderMeshUnmanaged);

            entityManager.SetComponentData(entity, new LocalToWorld
            {
                Value = float4x4.TRS(randomPosition, quaternion.identity, new float3(1, 1, 1))
            });
            #endregion

            GameObject enemyGO = enemyPrefab;
            //   Entity enemyEntity = entityManager1.GetComponentObject<GameObject>(enemyGO).Entity;

        }
    }

    void OnDrawGizmos()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            //   Debug.LogWarning("World is not initialized yet!");
            return; // Exit if world is null, preventing the null reference error.
        }

        // Get the EntityManager from the current World
        var entityManager = world.EntityManager;

        // Ensure we have valid EntityManager before proceeding
        if (entityManager == null)
        {
            // Debug.LogWarning("EntityManager is not available!");
            return;
        }

        // Get all entities from the EntityManager
        var entities = entityManager.GetAllEntities(Unity.Collections.Allocator.TempJob);
        foreach (var entity in entities)
        {
            if (entityManager.HasComponent<Translation>(entity))
            {
                var translation = entityManager.GetComponentData<Translation>(entity);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(new Vector3(translation.Value.x, translation.Value.y, translation.Value.z), 0.5f);
            }
        }

        // Dispose of the entities list after use to avoid memory leaks
        entities.Dispose();
    }
}
