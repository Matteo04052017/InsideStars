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
│   ├───AvatarsDemo
│   │   ├───Cubeman
│   │   │   ├───ColorCube.fbm
│   │   │   └───Materials
│   │   ├───Skyboxes
│   │   │   └───Textures
│   │   │       └───Sunny1
│   │   └───TestModels
│   │       ├───Grid
│   │       │   ├───Materials
│   │       │   └───Textures
│   │       └───U_Character
│   │           └───Materials
│   ├───DepthColliderDemo
│   │   ├───Materials
│   │   └───Scripts
│   ├───DL_Fantasy_RPG_Effects
│   │   ├───material
│   │   ├───prefab
│   │   └───texture
│   │       └───Materials
│   ├───GesturesDemo
│   │   ├───Materials
│   │   ├───Scripts
│   │   └───SlideTextures
│   ├───GUI
│   ├───KinectScripts
│   │   ├───Cubeman
│   │   ├───Filters
│   │   └───Samples
│   ├───Material
│   ├───Materials
│   ├───MilkyWay
│   │   ├───Material
│   │   ├───Scenes
│   │   └───Texture
│   ├───OutlineEffect
│   │   └───Demo
│   ├───OverlayDemo
│   │   ├───Materials
│   │   └───Scripts
│   ├───Pianeti
│   │   └───Materials
│   ├───PlaymakerKinectActions
│   ├───Plugins
│   │   ├───Metro
│   │   ├───x86
│   │   └───x86_64
│   ├───Pre-made
│   │   └───compressed
│   ├───Prefab
│   ├───Prefabs
│   ├───ProceduralGrid
│   ├───RockVR
│   │   ├───Common
│   │   │   ├───Scripts
│   │   │   └───Textures
│   │   └───Video
│   │       ├───Demo
│   │       │   ├───Audio
│   │       │   └───Scripts
│   │       ├───Editor
│   │       ├───Plugins
│   │       │   ├───OSX
│   │       │   │   └───VideoCaptureLib.bundle
│   │       │   │       └───Contents
│   │       │   │           ├───MacOS
│   │       │   │           └───_CodeSignature
│   │       │   └───Windows
│   │       │       ├───x64
│   │       │       └───x86
│   │       ├───Resources
│   │       │   ├───Materials
│   │       │   └───Prefabs
│   │       ├───Scripts
│   │       │   ├───Base
│   │       │   └───Utils
│   │       └───Shaders
│   ├───Scenes
│   ├───Script
│   ├───Shaders
│   ├───SplashScreeniOs
│   ├───spline
│   ├───Standard Assets
│   │   ├───Editor
│   │   ├───Particles
│   │   │   ├───Legacy Particles
│   │   │   └───Sources
│   │   │       ├───Materials
│   │   │       └───Textures
│   │   └───Windows
│   │       ├───Data
│   │       └───Kinect
│   ├───Standard Assets (Mobile)
│   │   └───Shaders
│   │       └───Miscellaneous Shaders
│   ├───StreamingAssets
│   │   └───RockVR
│   │       ├───FFmpeg
│   │       │   ├───OSX
│   │       │   └───Windows
│   │       └───Spatial Media Metadata Injector
│   │           ├───OSX
│   │           └───Windows
│   ├───Texture
│   ├───VolumetricLines
│   │   ├───ExampleScenes
│   │   │   ├───ExampleMaterials
│   │   │   ├───ExamplePrefabs
│   │   │   ├───ExampleScripts
│   │   │   ├───ExampleTerrain
│   │   │   └───ExampleTextures
│   │   ├───Materials
│   │   ├───Prefabs
│   │   ├───Scripts
│   │   │   └───Utils
│   │   └───Shaders
│   └───_MK
│       └───MKGlassFree
│           ├───Demo
│           │   ├───Materials
│           │   └───Textures
│           ├───Editor
│           └───Shader
│               └───Inc
│                   ├───Common
│                   ├───Forward
│                   ├───Meta
│                   ├───Surface
│                   └───VertexLit
├───Library
├───Packages
├───ProjectSettings
```
