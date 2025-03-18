using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

public partial struct MoverSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        // The system runs every frame
        state.RequireForUpdate<SpeedComponent>();
    }
    
   public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (translation, speed) in
                 SystemAPI.Query<RefRW<Translation>, RefRO<SpeedComponent>>())
        {
            // Move the enemy to the right
            translation.ValueRW.Value.y += speed.ValueRO.speed * deltaTime;
        }
    }
  
}
