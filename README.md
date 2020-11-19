## InsideStars

This project is intended for astrophysicists that want to explain what is happening inside a Star. With the help of the kinect sensor, the user of the application can draw nuclear elements (like hydrogen) and make the react so that the nuclear rections of a star can be described to the audience. 

This project has been presented in Genova. A demonstration can be found [here](https://www.youtube.com/watch?v=tvEsc-n5Dkc)

### Requirements
This project was made with [Unity](https://unity3d.com/get-unity/download/archive) version `2018.2.9f1` using [Kinect v2](https://www.microsoft.com/en-us/download/details.aspx?id=44561).

### Project scenes

There are 4 unity scenes: `Intro`, `Simulation`, `HBurning`, `HeBurning`:
* The `Intro` scene is the introduction of the application that is a view of the solar system with a camera that explore it until inside the star. 
* The `Simulation` is a simulated internal star environment with semi random simulated nuclear reaction.
* The `HBurning` scene is the gamification with the help of the kinect sensor of the hydrogen burning process;
* The `HeBurning` is the gamification with the help of the kinect sensor of the helium burning process

### Project structure

```
├───Assets
│   ├───AvatarsDemo (kinect v2 sdk)
│   ├───DepthColliderDemo (kinect v2 sdk)
│   ├───DL_Fantasy_RPG_Effects (https://assetstore.unity.com/packages/vfx/particles/dl-fantasy-rpg-effects-68246)
│   ├───GesturesDemo (kinect v2 sdk)
│   ├───GUI (not used)
│   ├───KinectScripts (kinect v2 sdk)
│   ├───Material (basic materials for hydrogen)
│   ├───Materials (solar system materials)
│   ├───MilkyWay  (https://assetstore.unity.com/packages/2d/textures-materials/milky-way-skybox-94001)
│   ├───OutlineEffect (https://www.assetstore.unity3d.com/#!/content/86481)
│   ├───OverlayDemo (kinect v2 sdk)
│   ├───Pianeti (Saturn and Uran rings)
│   ├───PlaymakerKinectActions (not used)
│   ├───Plugins (kinect v2 sdk)
│   ├───Pre-made (planets maps)
│   ├───Prefab (basic prefab for nuclear elements)
│   ├───Prefabs (sun and solar system prefabs)
│   ├───ProceduralGrid (not used)
│   ├───RockVR (assets: https://assetstore.unity.com/publishers/24830)
│   ├───Scenes (unity scenes)
│   ├───Script (c# script for nuclear reaction simulations)
│   ├───Shaders (shaders used for the prefab)
│   ├───SplashScreeniOs (image for splash screen)
│   ├───spline (spline interpolation)
│   ├───Standard Assets
│   ├───Standard Assets (Mobile)
│   ├───StreamingAssets (assets: https://assetstore.unity.com/publishers/24830)
│   ├───Texture (images for planets of the solar system)
│   ├───VolumetricLines (https://assetstore.unity.com/packages/tools/particles-effects/volumetric-lines-29160)
├───Library (unity private) 
├───Packages (unity private)
├───ProjectSettings (unity private)
```
