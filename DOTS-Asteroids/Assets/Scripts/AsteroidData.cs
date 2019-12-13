using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct AsteroidData : IComponentData
{
    public Vector3 position;
    public Quaternion rotation;
}
