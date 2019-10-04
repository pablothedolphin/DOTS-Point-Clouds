using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Unity.PointClouds
{
    public struct PointPositionEvent : IComponentData { }
    public struct PointVelocityEvent : IComponentData { }
    public struct PointRotationEvent : IComponentData { }
    public struct PointColorEvent : IComponentData { }
}
