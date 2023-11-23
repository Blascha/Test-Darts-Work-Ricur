using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class ShotsMadeUI : MonoBehaviour
{
    public static ShotsMadeUI instance;

    TMP_Text _text;

    private void Start()
    {
        //I will save the static reference to reach it easier and save a reference to the text
        instance = this;
        _text = GetComponent<TMP_Text>();
    }
    
    //I set the text so that at the beginig says, for example "5/5"
    public void ResetShots()
    {
        int maxShots = Shooter.MaxShots;
        _text.text = $"{maxShots}/{maxShots}";
    }

    //I set a new text, to update it, so now it would say 4/5, or 3/5, etc.
    public void DisplayShots(int shotsAmount)
    {
        int maxShots = Shooter.MaxShots;
        _text.text = $"{maxShots - shotsAmount}/{maxShots}";
    }
}
