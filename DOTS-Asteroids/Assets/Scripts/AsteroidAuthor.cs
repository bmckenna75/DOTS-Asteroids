using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[DisallowMultipleComponent]
[RequiresEntityConversion]
public class AsteroidAuthor : MonoBehaviour, IConvertGameObjectToEntity
{
    public Vector3 Position;
    public Quaternion Rotation;   

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        AsteroidData data = new AsteroidData
        {
            position = Position,
            rotation = Rotation
        };
        dstManager.AddComponentData(entity, data);

    }
}
