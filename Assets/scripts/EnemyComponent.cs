using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public struct PositionComponent : IComponentData
{
    public float3 position;
}

public struct SpeedComponent : IComponentData
{
    public float speed;
}

public struct Translation : IComponentData
{
    public float3 Value;
}

public struct EnemyPrefab : IComponentData
{
    public Entity enemyPrefab;
}

//public struct MeshComponent : IComponentData
//{
 //   public Entity meshEntity;  // A reference to the mesh prefab
//}
