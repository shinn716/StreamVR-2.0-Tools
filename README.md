# StreamVR-2.0-Tutorial  
**STEAM VR - The Ultimate VR developer guide PART 1**   
https://www.youtube.com/watch?v=5C6zr4Q5AlA&t=398s  
  
- 1:11 - Steam VR Setup 
- 4:00 - Customizable Hands 
- 7:19 - Teleportation  
- 11:04 - Input System  
- 14:27 - Joystick Movement 
  
**STEAM VR - The Ultimate VR developer guide PART 2**   
https://www.youtube.com/watch?v=MKOc8J877tI  
- 0:37 - Interactable Object  
- 6:05 - Throwable Objects  
- 9:41 - Snap Object with Handgun 
- 11:45 - Using Grabbed Object / Shooting with the handgun  
- 14:43 - Custom Hand Pose  
  
  

***

**SteamVR Action - Sample Code**  
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VIVEInput : MonoBehaviour
{
    public SteamVR_Behaviour_Pose trackedObject;
    public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    public SteamVR_Action_Boolean grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");
    public SteamVR_Action_Boolean uiInteractAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Vector2 touchpadAction = SteamVR_Actions.default_Touchpadpos;
    public SteamVR_Action_Boolean menu = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Menu");

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (menu.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            print("Menu clicked");
        }
        print("touchPad: " + touchpadAction.GetLastAxis(SteamVR_Input_Sources.RightHand));
    }
}
```


**Source:**  
Valem  
https://www.youtube.com/channel/UCPJlesN59MzHPPCp0Lg8sLw  
