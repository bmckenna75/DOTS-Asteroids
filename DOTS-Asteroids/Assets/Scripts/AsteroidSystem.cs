using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class AsteroidSystem : JobComponentSystem
{


    [BurstCompile]
    struct AsteroidSystemJob : IJobForEach<Translation, Rotation, AsteroidData>
    {
        public float time;
        //uses jobs to push asteroids across the screen and loop them around
        public void Execute(ref Translation translation, ref Rotation rotation, ref AsteroidData asteroidData)
        {
            if (translation.Value.x <= -11)
            {
                translation.Value.x = 10.5f;
                //asteroidData.velocity.x = asteroidData.velocity.x * -1;
            }
            else if (translation.Value.x >= 11)
            {
                translation.Value.x = -10.5f;
                //asteroidData.velocity.x = asteroidData.velocity.x * -1;
            }

            if (translation.Value.y <= -6)
            {
                translation.Value.y = 5.9f;
                //asteroidData.velocity.y = asteroidData.velocity.y * -1;
            }
            else if (translation.Value.y >= 6)
            {
                translation.Value.y = -5.9f;
                //asteroidData.velocity.y = asteroidData.velocity.y * -1;
            }

            translation.Value.x = translation.Value.x + asteroidData.velocity.x * time;
            translation.Value.y = translation.Value.y + asteroidData.velocity.y * time;



        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new AsteroidSystemJob();

        job.time = UnityEngine.Time.deltaTime;

        return job.Schedule(this, inputDependencies);
    }
}