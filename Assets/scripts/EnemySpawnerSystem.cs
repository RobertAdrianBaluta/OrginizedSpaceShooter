using UnityEngine;
using Unity.Entities;

public class EnemySpawnerSystem : MonoBehaviour
{
     // Adjust the number of entities to spawn
     public int entitiesCount = 200;

    void Start()
    {
        MakeEntities();
    }

    private void MakeEntities()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;


      
        for (int i = 0; i < entitiesCount; i++)
        {
            Entity entity = entityManager.CreateEntity(
                typeof(PositionComponent),
                typeof(SpeedComponent),
                typeof(MeshComponent));

            // Set the LevelComponent's level field to a specific value
            entityManager.SetComponentData(entity, new PositionComponent { position = Random.Range(1, 200) });
            entityManager.SetComponentData(entity, new SpeedComponent { speed = 20.0f });
         


        }

    }
}

