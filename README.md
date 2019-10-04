# DOTS Point Clouds

![image](image.png)

The image above shows a grid of 10 million quads rendered on a mid range laptop at 45 fps. Since the input data is in `Entity` `ComponentData`, detailed simulations can be done with the full power of both the CPU and GPU.

Updates to the point cloud are handled by a `ComponentSystem` per `ComponentData` type. Each `ComponentSystem` is triggered via an event `Entity`.

The component types are all available under the `Unity.PointClouds` namespace.