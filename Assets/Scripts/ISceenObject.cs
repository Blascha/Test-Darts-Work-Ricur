using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenObject
{
    public abstract void OnScreenStart();

    public abstract void OnScreenEnd();
}
