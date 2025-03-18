using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using TMPro;

public class EntityTextReference : MonoBehaviour
{
    public Entity entitytext; // The ECS entity this text belongs to
    private EntityManager entityManager;
    private TextMeshPro textMesh;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        textMesh = GetComponent<TextMeshPro>();
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
