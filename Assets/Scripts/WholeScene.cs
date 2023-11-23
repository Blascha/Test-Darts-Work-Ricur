using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeScene : MonoBehaviour , IScreenObject
{
    [SerializeField] ScreenManager.Screen Scene;
    void Awake()
    {
        ScreenManager.AddObjectToScreen(this, Scene);
    }

    public void OnScreenStart()
    {
        gameObject.SetActive(true);
    }

    public void OnScreenEnd()
    {
        gameObject.SetActive(false);
    }
}
