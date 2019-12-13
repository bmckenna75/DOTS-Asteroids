using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct BulletData : IComponentData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
}