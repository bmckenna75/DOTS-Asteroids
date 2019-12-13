using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class BulletDataJob : JobComponentSystem
{
    // This declares a new kind of job, which is a unit of work to do.
    // The job is declared as an IJobForEach<Translation, Rotation>,
    // meaning it will process all entities in the world that have both
    // Translation and Rotation components. Change it to process the component
    // types you want.
    //
    // The job is also tagged with the BurstCompile attribute, which means
    // that the Burst compiler will optimize it for the best performance.
    [BurstCompile]
    struct BulletDataJobJob : IJobForEach<Translation, Rotation, BulletData>
    {
        // Add fields here that your job needs to do its work.
        // For example,
        public float deltaTime;
        //pushes bullets forwrard from their original position
        public void Execute(ref Translation trans, ref Rotation rot, ref BulletData bulletdata)
        {
            // Implement the work to perform for each entity here.
            // You should only access data that is local or that is a
            // field on this job.
            //     translation.Value += mul(rotation.Value, new float3(0, 0, 1)) * deltaTime;
            trans.Value.x = trans.Value.x + bulletdata.velocity.x * deltaTime;
            trans.Value.y = trans.Value.y + bulletdata.velocity.y * deltaTime;
            rot.Value = bulletdata.rotation;
            

        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new BulletDataJobJob();

        // Assign values to the fields on your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        job.deltaTime = UnityEngine.Time.deltaTime;
        
        
        // Now that the job is set up, schedule it to be run. 
        return job.Schedule(this, inputDependencies);
    }
}