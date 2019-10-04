using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.PointClouds
{
    public class PointCloudNormalRenderSystem : PointCloudFloat3RenderSystem<PointNormal>
    {
        protected override void OnCreate ()
        {
            base.OnCreate ();

            renderTexture = Resources.Load<RenderTexture> ("Normal Map");
        }
    }
}