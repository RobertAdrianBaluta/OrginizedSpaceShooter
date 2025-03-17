using UnityEngine;
using Unity.Entities;

public class EnemyCollisionRefrence : MonoBehaviour
{
    public Entity entitycollision; // The ECS entity this collider belongs to
    private EntityManager entityManager;
    public bool hit = false;
    [SerializeField] private GameObject enemyTextPrefab;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void Update()
    {

        if (entityManager.Exists(entitycollision) && entityManager.HasComponent<Translation>(entitycollision))
        {
            // ✅ Get the entity's current position and apply it to the GameObject
            Translation translation = entityManager.GetComponentData<Translation>(entitycollision);
            transform.position = translation.Value;
        }


    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        int sroe = 0;
        if (collision.CompareTag("bullet")) // 🎯 Ensure bullets have the "Bullet" tag
        {

            sroe++;
            Debug.Log("score");

            DestroyEnemy();
        }

        if (collision.CompareTag("Killzone"))
        {
            DestroyEnemy();
        }

    }

    private void DestroyEnemy()
    {
        Debug.Log($"Enemy hit by bullet: {entitycollision}");

        // ❌ Destroy the ECS entity
        if (entityManager.Exists(entitycollision))
        {
            entityManager.DestroyEntity(entitycollision);
        }

        // ❌ Destroy the GameObject (collision object)
        Destroy(gameObject);

        if (enemyTextPrefab != null)
        {
            Destroy(enemyTextPrefab);  // Make sure this isn't a prefab but an instance
        }
    }
}
