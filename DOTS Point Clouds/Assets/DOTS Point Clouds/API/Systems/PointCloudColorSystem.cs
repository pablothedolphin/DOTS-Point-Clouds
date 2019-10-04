using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace Unity.PointClouds
{
	public class PointCloudColorSystem : PointCloudSystemBase<PointColor, PointColorEvent>
	{
		protected override void OnCreate ()
		{
			base.OnCreate ();

			renderTextureName = "Color Map";
			computeShaderName = "float4ComputeShader";
			propertyCount = 4;
		}
	}
}