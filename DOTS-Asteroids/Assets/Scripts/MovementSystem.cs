using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class MovementSystem : JobComponentSystem
{
    public static float totalTime = 0;

    [BurstCompile]
    struct MovementSystemJob : IJobForEach<Translation, Rotation>
    {
        public float time;



        public void Execute(ref Translation translation, [ReadOnly] ref Rotation rotation)
        {
            float y = math.abs(2 * math.sin(PI * 3 * time)) + 3;
            translation.Value.y = y;

            /*Quaternion temp = rotation.Value;
            Vector3 tempVec = temp.eulerAngles;
            translation.Value.x = tempVec.x * 3.0f;
            translation.Value.z = tempVec.z * 3.0f;*/


        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new MovementSystemJob();

        // bouncing requires a summation of the delta time's from Unity
        totalTime += UnityEngine.Time.deltaTime;
        job.time = totalTime;


        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}