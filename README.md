# Scriptable Object Variables with UniRx
This is an implementation of variables in Unity using scriptable objects as explained in [this](https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=3289s) talk. We have integration with [UniRx](https://github.com/neuecc/UniRx) but there is an option to use this library without UniRx and use C# Actions instead. By default you will get version without UniRx integration. See Integrations section for a guide how to enable UniRx integration.

# Usage
## Creating Variable
Simply right click into one of you folders in your project, select: Create -> Variable -> Select Type (there are some primitive types already contained in this library)
## Using Variable
Anywhere in you script simply define reference to a variable like:
```C#
public BoolReference myVariableName;
```
or the variable itself
```C#
public BoolVariable myVariableName;
```
And use it anyway you with. Preferably you want to use references for reading the values and variables for writing into them.

# Persistance
There is a simple, generic, build in solution for persisting variables. What you need to do is
   - Create a persistor (Create -> Variables -> Persistance -> Persistor)
   - Create serializer (Create -> Variables -> Persistance -> Serializer) - You can choose from serializers that persist into player prefs or persistentDataPath
   - Assign the serializer into the persistor, give it a filename
   - Assign variables you want to persist into the persistor
   - Use Save/Load to save and load the data
   - (optional) - Put SaveDataWatcher on a game object, assign it a persistor(s), it will save / load data automatically and periodically

# Creating new custom variables
To simplify the process of creation of new variables and editor scripts for them we have implemented tool that help you with that
   - Use Tools -> Reactive Variables -> Create New (it uses current assembly, if you want to create variable for another assembly like Unitys Game Object or you are using new Assembly Definition Files, use All Assembly option)
   - Pick folders where to store generated files
        - Please note that the editor scripts needs to be stored in Editor folder or assembly
   - Start typing the class name
   - Select class you want to create variables for and click confirm
        - In case of any errors
        
# Integrations
  - This project is integrated with UniRx - https://github.com/neuecc/UniRx
     - To enable this integration, use Tools -> Reactive Variables -> Enable UniRx Integration
