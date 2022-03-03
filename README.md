# Shadow_Casting
Proof of concept of [13th Haunted Street](https://github.com/AlecInfo/13th_Haunted_Street), which aims to experience [Penumbra](https://github.com/discosultan/penumbra). Penumbra is a C# package designed for [Monogame](https://www.monogame.net/), which aims to create shadows and lights.

## Installation

1. [DirectX End-User Runtimes (June 2010)](https://www.microsoft.com/en-us/download/details.aspx?id=8109)
> DirectX is usefull for compiling effects shaders

2. Add a NuGet
> Go to Visual Studio 2019 > Tools > NuGet Package Manager
> <br>
<p>
  <img src="/Documentation/InstallNuGet.png" alt="NuGet" Height="300">
</p>

3. Run the order

```shell
Install-Package MonoGame.Penumbra.WindowsDX
```
<p>
  <img src="/Documentation/InstallPackage.png" alt="Install Package" Height="280">
</p>

4. Create a new Monogame project

<p>
  <img src="/Documentation/CreateProjectMonogame.png" alt="Create monogame project" Height="300">
</p>

## Use

Add the using to be able to access all the features of penumbra
```cs
using Penumbra;
``` 

Create penumbra component
```cs
PenumbraComponent penumbra;

public Game1()
{
  penumbra = new PenumbraComponent(this);
  Components.Add(penumbra);
}
```

Add the BeginDraw for penumbra
```cs
protected override void Draw(GameTime gameTime)
{
  penumbra.BeginDraw();
  GraphicsDevice.Clear(Color.CornflowerBlue);
}
```
<br>

Penumbra supports three types of lights: `PointLight`, `Spotlight`, `TexturedLight`

![PointLight](https://github.com/discosultan/penumbra/raw/master/Documentation/PointLight.png)
![Spotlight](https://github.com/discosultan/penumbra/raw/master/Documentation/Spotlight.png)
![TexturedLight](https://github.com/discosultan/penumbra/blob/master/Documentation/TexturedLight.png)

Lights provide three types of shadowing schemes: `ShadowType.Solid`, `ShadowType.Occluded`, `ShadowType.Illuminated`

![Solid](https://github.com/discosultan/penumbra/blob/master/Documentation/Solid.png)
![Occluded](https://github.com/discosultan/penumbra/raw/master/Documentation/Occluded.png)
![Illuminated](https://github.com/discosultan/penumbra/raw/master/Documentation/Illuminated.png)
