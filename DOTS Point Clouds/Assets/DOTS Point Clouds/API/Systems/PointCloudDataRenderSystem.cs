using System;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

namespace Unity.PointClouds
{
	public abstract class PointCloudDataRenderSystem<T> : ComponentSystem where T: struct, IComponentData
	{
		protected RenderTexture renderTexture;
		protected ComputeShader computeShader;

		protected NativeArray<T> renderData;

		protected ComputeBuffer dataBuffer;
		protected RenderTexture tempRenderTexture;

        protected int propertyCount;

		protected override void OnUpdate ()
		{
            if (!ValidateRenderTexture ()) return;

            EntityQuery entityQuery = EntityManager.CreateEntityQuery (typeof (T));
            renderData = entityQuery.ToComponentDataArray<T> (Allocator.TempJob);

            int mapWidth = renderTexture.width;
			int mapHeight = renderTexture.height;

			int totalData = renderData.Length;
			int totalProperties = totalData * propertyCount;

			// Release the temporary objects when the size of them don't match the input.

			if (dataBuffer != null && dataBuffer.count != totalProperties)
			{
				dataBuffer.Dispose ();
				dataBuffer = null;
			}

			if (tempRenderTexture != null && (tempRenderTexture.width != mapWidth || tempRenderTexture.height != mapHeight))
			{
				GameObject.Destroy (tempRenderTexture);
				tempRenderTexture = null;
			}

			// Lazy initialization of temporary objects

			if (dataBuffer == null)
			{
				dataBuffer = new ComputeBuffer (totalProperties, sizeof (float));
			}

			if (tempRenderTexture == null)
			{
				tempRenderTexture = CreateRenderTexture (renderTexture);
			}

			// Set data and execute the bake task.

			computeShader.SetInt ("DataCount", totalData);
			dataBuffer.SetData (renderData);

            int kernel = 0;

			computeShader.SetBuffer (kernel, "DataBuffer", dataBuffer);
			computeShader.SetTexture (kernel, "DataMap", tempRenderTexture);

			computeShader.Dispatch (kernel, mapWidth / 32, mapHeight / 32, 1);

			// once complete, write the results back on to the real data map file

			Graphics.CopyTexture (tempRenderTexture, renderTexture);

            renderData.Dispose ();

            if (dataBuffer != null) dataBuffer.Dispose ();
            dataBuffer = null;

            if (tempRenderTexture != null) GameObject.Destroy (tempRenderTexture);
            tempRenderTexture = null;
        }

		private bool ValidateRenderTexture ()
		{
			if (renderTexture.width % 8 != 0 || renderTexture.height % 8 != 0)
			{
				Debug.LogError ("Render Texture dimensions should be a multiple of 8.");
                return false;
			}

			if (renderTexture.format != RenderTextureFormat.ARGBHalf && renderTexture.format != RenderTextureFormat.ARGBFloat)
			{
				Debug.LogError ("Render Texture format should be ARGBHalf or ARGBFloat.");
                return false;
            }

            return true;
        }

        private RenderTexture CreateRenderTexture (RenderTexture source)
		{
			var rt = new RenderTexture (source.width, source.height, 0, source.format);
			rt.enableRandomWrite = true;
			rt.Create ();
			return rt;
		}
	}
}
