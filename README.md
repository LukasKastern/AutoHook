# AutoHooker

AutoHooker is an Unity Editor extension for automatically resolving object references on MonoBehaviour scripts.  

![ShowCase](https://github.com/LukasKastern/AutoHooker/raw/master/showcase.gif)


### Usage

All you have to do is to add the attribute **AutoHook** to the property. </br>
The attribute will try to find a component of the given type on the object and assign it to the property.

``` csharp
[AutoHook]
public Collider PlayerCollider; 
```
