using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using TMPro;

public class EntityTextReference : MonoBehaviour
{
    public Entity entitytext; // The ECS entity this text belongs to
    private EntityManager entityManager;
<<<<<<< HEAD
    private TextMeshPro textMesh;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        textMesh = GetComponent<TextMeshPro>();
=======
 
    
    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
>>>>>>> c9207b23c7e9b49918ab10c0e8f9b3e4e2163d95
    }

    void Update()
    {
        if (entityManager.Exists(entitytext))
        {
            // Get ECS position
<<<<<<< HEAD
=======
            
>>>>>>> c9207b23c7e9b49918ab10c0e8f9b3e4e2163d95
            Translation translation = entityManager.GetComponentData<Translation>(entitytext);
            transform.position = translation.Value;
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> c9207b23c7e9b49918ab10c0e8f9b3e4e2163d95
