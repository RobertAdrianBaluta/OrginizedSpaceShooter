using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

public class EnemySpawnerSystem : MonoBehaviour
{
     // Adjust the number of entities to spawn
     public int entitiesCount = 10;

    [SerializeField] private Mesh enemyMesh;      // Assign a Cube/Sphere in the Inspector
    [SerializeField] private Material enemyMaterial; // Assign a basic material

    void Start()
    {
        MakeEntities();
        OnDrawGizmos();
    }

    private void MakeEntities()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype archetype = entityManager.CreateArchetype(
          // Position
            typeof(LocalToWorld),   // Required for rendering
            typeof(RenderMeshUnmanaged),     // Mesh & Material
            typeof(RenderBounds),   // Required for Hybrid Renderer
            typeof(Translation),
             typeof(SpeedComponent)
        );


        for (int i = 0; i < entitiesCount; i++)
        {
            Entity entity = entityManager.CreateEntity(archetype);

            // Set the LevelComponent's level field to a specific value
            entityManager.SetComponentData(entity, new SpeedComponent { speed = 20.0f });

            float3 randomPosition = new float3(
               UnityEngine.Random.Range(-100f, 100f), // Random X
               0f,                                  // Y (flat ground)
               UnityEngine.Random.Range(-100f, 100f) // Random Z
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

            Debug.Log($"Entity {i} created with mesh: {renderMeshUnmanaged.mesh} and material: {renderMeshUnmanaged.materialForSubMesh}");

        }

    }

    void OnDrawGizmos()
    {
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            Debug.LogWarning("World is not initialized yet!");
            return; // Exit if world is null, preventing the null reference error.
        }

        // Get the EntityManager from the current World
        var entityManager = world.EntityManager;

        // Ensure we have valid EntityManager before proceeding
        if (entityManager == null)
        {
            Debug.LogWarning("EntityManager is not available!");
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

