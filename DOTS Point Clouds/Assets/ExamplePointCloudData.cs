using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.PointClouds;

public class ExamplePointCloudData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EntityManager manager = World.Active.EntityManager;

        for (int x = 0; x < 1000; x++)
        {
            for (int z = 0; z < 1000; z++)
            {
                Entity entity = manager.CreateEntity (typeof (PointPosition), typeof (PointNormal));
                manager.SetComponentData (entity, new PointPosition (new float3 (x, 0, z) / 10));
                manager.SetComponentData (entity, new PointNormal (new float3 (90, 0, 0)));
            }
        }       

    }
}
