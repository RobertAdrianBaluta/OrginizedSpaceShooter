
using Unity.Entities;

public partial struct MoverSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        // The system runs every frame
        state.RequireForUpdate<SpeedComponent>();
    }
<<<<<<< HEAD
    
   public void OnUpdate(ref SystemState state)
=======

    public void OnUpdate(ref SystemState state)
>>>>>>> c9207b23c7e9b49918ab10c0e8f9b3e4e2163d95
    {
       float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (translation, speed) in
                 SystemAPI.Query<RefRW<Translation>, RefRO<SpeedComponent>>())
        {
            // Move the enemy to the right
            translation.ValueRW.Value.x -= speed.ValueRO.speed * deltaTime;
        }
   
    }
  
}
