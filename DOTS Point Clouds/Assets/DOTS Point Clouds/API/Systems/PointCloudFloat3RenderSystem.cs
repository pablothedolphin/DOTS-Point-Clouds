using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Unity.PointClouds
{
    public abstract class PointCloudFloat3RenderSystem<T> : PointCloudDataRenderSystem<T> where T : struct, IComponentData
    {
        protected override void OnCreate ()
        {
            base.OnCreate ();

            propertyCount = 3;
            computeShader = Resources.Load<ComputeShader> ("float3ComputeShader");
        }
    }
}