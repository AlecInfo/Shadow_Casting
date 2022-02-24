# Shadow_Casting
Sous projet de [13th Haunted Street](https://github.com/AlecInfo/13th_Haunted_Street), qui a pour but d'expérimenter [Penumbra](https://github.com/discosultan/penumbra). Penumbra 
est un Package C# conçu pour [Monogame](https://www.monogame.net/), qui a pour but de créer des ombres et des lumières.


## Installation

1. [DirectX End-User Runtimes (June 2010)](https://www.microsoft.com/en-us/download/details.aspx?id=8109)
> DirectX est utile pour compiler les effects sheder

2. Ajouter un NuGet
> Aller dans Vidual Studio 2019 > Outils > Gestionnaire de package NuGet
> <br>
<p>
  <img src="https://github.com/AlecInfo/Shadow_Casting/blob/master/Documentation/InstallNuGet.png" alt="NuGet" Height="300">
</p>

3. Mettre la commande

```shell
Install-Package MonoGame.Penumbra.WindowsDX
```
<p>
  <img src="https://github.com/AlecInfo/Shadow_Casting/blob/master/Documentation/InstallPackage.png" alt="Install Package" Height="280">
</p>

4. Créer le projet Monogame

<p>
  <img src="https://github.com/AlecInfo/Shadow_Casting/blob/master/Documentation/CreateProjectMonogame.png" alt="Create monogame project" Height="300">
</p>

## Utilisation

Ajoutez le using pour pouvoir avoir acces a toutes les fonctionnalitées de penumbra
```cs
using Penumbra;
``` 

Créer le composant de penumbra
```cs
PenumbraComponent penumbra;

public Game1()
{
  // ...
  penumbra = new PenumbraComponent(this);
  Components.Add(penumbra);
}
```

Ajouter le BeginDraw pour penumbra
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
