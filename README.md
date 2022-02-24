# Shadow_Casting
Sous projet de [13th Haunted Street](https://github.com/AlecInfo/13th_Haunted_Street), qui a pour but d'expérimenter [Penumbra](https://github.com/discosultan/penumbra). Penumbra 
est un Package C# conçu pour [Monogame](https://www.monogame.net/), qui a pour but de créer des ombres et des lumières.


## Installation

1. [DirectX End-User Runtimes (June 2010)](https://www.microsoft.com/en-us/download/details.aspx?id=8109)
> DirectX est utile pour compiler les effects sheder

2. Ajouter un NuGet
> Aller dans Vidual Studio 2019 > Outils > Gestionnaire de package NuGet
> <br>
<p align="center">
  <img src="https://github.com/AlecInfo/Shadow_Casting/blob/master/Documentation/InstallNuGet.png" alt="NuGet" Height="360">
</p>

3. Mettre la commande

```shell
Install-Package MonoGame.Penumbra.WindowsDX
```
<p align="center">
  <img src="https://github.com/AlecInfo/Shadow_Casting/blob/master/Documentation/InstallPackage.png" alt="Install Package" Height="340">
</p>

4. Créer le projet Monogame

<br>
<p align="center">
  <img src="https://github.com/AlecInfo/Shadow_Casting/blob/master/Documentation/CreateProjectMonogame.png" alt="Create monogame project" Height="360">
</p>

6. Begin to code

Ajoutez le using pour pouvoir avoir acces a toutes les fonctionnalitées de penumbra
```c#
using Penumbra;
``` 


