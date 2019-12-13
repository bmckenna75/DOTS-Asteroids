using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class AsteroidAuthor : MonoBehaviour, IConvertGameObjectToEntity
{
    public Vector3 Position;
    public Vector3 Velocity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        AsteroidData data = new AsteroidData
        {
            position = Position,
            velocity = Velocity
        };
        dstManager.AddComponentData(entity, data);

    }
}
