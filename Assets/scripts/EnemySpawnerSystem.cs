using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using System.Collections;

public class EnemySpawnerSystem : MonoBehaviour
{
    public int entitiesCount = 100;
    public int x;
    public int y;
    public float cooldownTime = 0.3f;
   // private bool isOnCooldown = false;


    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;
    //[SerializeField] private GameObject  enemyTextPrefab;
    [SerializeField] private GameObject  enemyShaderPrefab;
    [SerializeField] private GameObject  enemyCollisionPrefab;


    void Start()
    {
        StartCoroutine(MakeEntities());
   //     OnDrawGizmos();

    }

    IEnumerator MakeEntities()
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
            yield return new WaitForSeconds(0.1f);
            Entity entity = entityManager.CreateEntity(archetype);
            entityManager.SetComponentData(entity, new SpeedComponent { speed = 3.0f });

            float3 randomPosition = new float3(
                          UnityEngine.Random.Range(10, 11),
                           UnityEngine.Random.Range(5, -5),
                          0f // Random Z
                          );

            entityManager.SetComponentData(entity, new Translation { Value = randomPosition });

            //   GameObject entityshader = Instantiate(enemyShaderPrefab, randomPosition, Quaternion.identity);
            //   entityshader.GetComponent<ShaderRefrence>().entityshader = entity;

        ///    GameObject enemyObject = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
       //   GameObject textObject = Instantiate(enemyTextPrefab, randomPosition, Quaternion.identity);
       //    textObject.GetComponent<EntityTextReference>().entitytext = entity;
        //    textObject.GetComponent<TextMeshPro>().text = UnityEngine.Random.Range(0, 2) == 0 ? "0" : "1";

          GameObject entitycollision = Instantiate(enemyCollisionPrefab, randomPosition, Quaternion.identity);
           entitycollision.GetComponent<EnemyCollisionRefrence>().entitycollision = entity; 

            #region
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


            //GameObject enemyGO = enemyPrefab;
            //   Entity enemyEntity = entityManager1.GetComponentObject<GameObject>(enemyGO).Entity;
            #endregion
           
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
