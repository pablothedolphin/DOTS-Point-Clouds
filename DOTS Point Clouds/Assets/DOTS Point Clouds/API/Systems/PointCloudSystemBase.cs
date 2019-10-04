using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace Unity.PointClouds
{
	public abstract class PointCloudSystemBase<T0, T1> : ComponentSystem
		where T0 : struct, IComponentData where T1 : struct, IComponentData
	{
		protected string renderTextureName;
		protected string computeShaderName;
		protected int propertyCount;

		private EntityQuery dataQuery;
		private EntityQuery eventQuery;

		protected override void OnCreate ()
		{
			base.OnCreate ();

			dataQuery = EntityManager.CreateEntityQuery (typeof (T0));
			eventQuery = EntityManager.CreateEntityQuery (typeof (T1));

			RequireForUpdate (eventQuery);
		}

		protected override void OnUpdate ()
		{
			// dispatch compute shader
			new PointCloudDataWriter<T0>
			{
				renderTexture = Resources.Load<RenderTexture> (renderTextureName),
				computeShader = Resources.Load<ComputeShader> (computeShaderName),
				renderData = dataQuery.ToComponentDataArray<T0> (Allocator.TempJob),
				propertyCount = propertyCount
			}.WriteToRenderTexture ();

			// destroy the event entities that were created this frame
			NativeArray<Entity> eventEntities = eventQuery.ToEntityArray (Allocator.TempJob);

			for (int i = 0; i < eventEntities.Length; i++)
			{
				EntityManager.DestroyEntity (eventEntities[i]);
			}

			eventEntities.Dispose ();
		}
	}
}