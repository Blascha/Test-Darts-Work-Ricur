using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenManager
{
    //When I tested the game for the first time, each time I loaded a new scene, everything lasted almost a full second to load, including the AR part that I wanted to be seamless
    //So, I used this programming pattern that helps everytinh load faster.

    //I use enums for increasing readability
    public enum Screen
    {
        Menu,
        Play,
        Store
    }

    //I save the actual "Scene" (wich in the pattern are called screens)
    static Screen actualScreen = Screen.Menu;
    
    //I use a Disctionary to save everything in a list of gameObjects with a reference to qhere they are.
    static Dictionary<Screen, List<IScreenObject>> objectsToBeTurnedOn = new Dictionary<Screen, List<IScreenObject>>();

    public static void ChangeScene(Screen newScreen)
    {
        Debug.Log($"Going from <color=red>{actualScreen}</color> to <color=red>{newScreen}</color>");
        //I turn off the old scene
        foreach (var i in objectsToBeTurnedOn[actualScreen])
        {
            i.OnScreenEnd();
        }

        //I turn on the new scene
        foreach (var i in objectsToBeTurnedOn[newScreen])
        {
            i.OnScreenStart();
        }

        //I save the new scene as the actual scene
        actualScreen = newScreen;
    }

    public static void AddObjectToScreen(IScreenObject objectToAdd, Screen screen)
    {
        //If there isnt a scene saved where there should be, I make a new one
        if(!objectsToBeTurnedOn.ContainsKey(screen))
        {
            objectsToBeTurnedOn[screen] = new List<IScreenObject>();
        }

        //I add the objects I want where I want them
        objectsToBeTurnedOn[screen].Add(objectToAdd);
    }

    public static void TurnOffAllExceptFor(Screen screenToBeOn)
    {
        //I willl cycle through the whole dictionary checking some things
        foreach (var i in objectsToBeTurnedOn.Keys)
        {
            //If the Screen doesn´t exist or is the screen I want on, I wont turn it off. Else I will.
            if(objectsToBeTurnedOn.ContainsKey(i) && i != screenToBeOn)
            {
                foreach (var g in objectsToBeTurnedOn[i])
                {
                    g.OnScreenEnd();
                }
            }
        }
        
    }
}
