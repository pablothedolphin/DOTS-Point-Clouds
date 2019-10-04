using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.PointClouds
{
    public class PointCloudPositionRenderSystem : PointCloudFloat3RenderSystem<PointPosition>
    {
        protected override void OnCreate ()
        {
            base.OnCreate ();

            renderTexture = Resources.Load<RenderTexture> ("Position Map");
        }
    }
}