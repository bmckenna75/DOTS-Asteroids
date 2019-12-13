using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class BulletAuthor : MonoBehaviour, IConvertGameObjectToEntity
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Velocity;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        BulletData data = new BulletData
        {
            position = Position,
            rotation = Rotation,
            velocity = Velocity
        };
        dstManager.AddComponentData(entity, data);

    }
}
