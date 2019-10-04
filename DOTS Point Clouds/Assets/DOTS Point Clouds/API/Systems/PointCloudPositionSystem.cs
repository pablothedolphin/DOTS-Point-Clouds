using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace Unity.PointClouds
{
	public class PointCloudPositionSystem : PointCloudSystemBase<PointPosition, PointPositionEvent>
	{
		protected override void OnCreate ()
		{
			base.OnCreate ();

			renderTextureName = "Position Map";
			computeShaderName = "float3ComputeShader";
			propertyCount = 3;
		}
	}
}