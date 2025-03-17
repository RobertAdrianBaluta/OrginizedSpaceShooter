using UnityEngine;
using Unity.Entities;

public class EntityTextReference : MonoBehaviour
{
    public Entity entitytext; // The ECS entity this text belongs to
    private EntityManager entityManager;
 
    
    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void Update()
    {
        if (entityManager.Exists(entitytext))
        {
            // Get ECS position
            
            Translation translation = entityManager.GetComponentData<Translation>(entitytext);
            transform.position = translation.Value;
        }
    }
}