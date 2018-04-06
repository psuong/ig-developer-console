# In-Game Developer Console

The In-Game Developer Console provides an interactable in-game console similar to ones found in:

* Counter Strike series
* Quake 4
* Elder Scrolls/Fallout series

It is a miminalistic Unity tool and comes packaged with:

* Global Event Handler
* Relative Event Handler
* Customizable UI

Allowing *any* developer or user to interact and modify/execute controllable properties or functions 
when the game is **built** or in **play mode**.

## Submodules

### Global Event Handler

The Global Event Handler allows any class/structure to register a `Delegate/Action` to a `globalEventTable` within 
`GlobalEventHandler.cs`. The Developer Console uses this to allow any method to create a custom output for the console 
to display, however this can be extended for any class to implement.

Example:
```
// To register a global event

using UnityEngine;

public class Health : MonoBehaviour {
    
}
```

### Relative Event Handler (.NET 3.5 Version)

The Relative Event Handler takes a particular instance of a gameObject 
