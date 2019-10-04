using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.PointClouds;

public class ExamplePointCloudData : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        EntityManager manager = World.Active.EntityManager;

        for (int x = 0; x < 3162; x++)
        {
            for (int z = 0; z < 3162; z++)
            {
                Entity entity = manager.CreateEntity ();
                manager.AddComponentData (entity, new PointPosition (new float3 (x, 0, z) / 10));
                manager.AddComponentData (entity, new PointRotation (new float3 (90, 0, 0)));
				Color c = Color.HSVToRGB (Mathf.PerlinNoise (x * 0.01f, z * 0.01f) - 0.1f, 0.7f, 1);
				float4 color = new float4 (c.r, c.g, c.b, 1);

				manager.AddComponentData (entity, new PointColor (color)); 
			}
        }

		manager.CreateEntity (typeof (PointPositionEvent));
		manager.CreateEntity (typeof (PointRotationEvent));
		manager.CreateEntity (typeof (PointColorEvent));
	}

	// convert a Vector3 hue, saturation, brightness to red, green blue values 	
	private Color HSVToRGB (Vector3 HSV)
	{
		Vector3 H = Hue (HSV.x);
		H = new Vector3 (H.x - 1, H.y - 1, H.z - 1) * HSV.y;
		H = new Vector3 (H.x + 1, H.y + 1, H.z + 1) * HSV.z;
		return new Color (H.x, H.y, H.z, 1);
	}

	// algorithm to convert hue value for RGB [used in HSVtoRGB function]
	private Vector3 Hue (float H)
	{
		float R = Mathf.Abs (H * 6 - 3) - 1;
		float G = 2 - Mathf.Abs (H * 6 - 2);
		float B = 2 - Mathf.Abs (H * 6 - 4);
		return new Vector3 (Mathf.Clamp01 (R), Mathf.Clamp01 (G), Mathf.Clamp01 (B));
	}

}
