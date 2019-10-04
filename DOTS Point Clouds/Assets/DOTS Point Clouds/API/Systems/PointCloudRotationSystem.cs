using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace Unity.PointClouds
{
	public class PointCloudRotationSystem : PointCloudSystemBase<PointRotation, PointRotationEvent>
	{
		protected override void OnCreate ()
		{
			base.OnCreate ();

			renderTextureName = "Rotation Map";
			computeShaderName = "float3ComputeShader";
			propertyCount = 3;
		}
	}
}