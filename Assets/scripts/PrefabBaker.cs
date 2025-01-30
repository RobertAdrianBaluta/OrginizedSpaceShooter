using Unity.Entities;
using UnityEngine;

public class PrefabBakerAuthoring : MonoBehaviour
{
    public GameObject prefab; // Assign your prefab in the Inspector
}

public class PrefabBaker : Baker<PrefabBakerAuthoring>
{
    public override void Bake(PrefabBakerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new PrefabEntity { Value = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic) });
    }
}

public struct PrefabEntity : IComponentData
{
    public Entity Value;
}