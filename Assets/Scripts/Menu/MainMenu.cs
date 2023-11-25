using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent (typeof(CanvasGroup))]
public class MainMenu : MonoBehaviour , IScreenObject
{
    [SerializeField] Button ButtonToTurnOn;
    [SerializeField] bool isMainMenu;
    [SerializeField] TMP_Text HighScore;

    private void Start()
    {
        if (isMainMenu)
        {
            ScreenManager.AddObjectToScreen(this, ScreenManager.Screen.Menu);
        }

        if (ButtonToTurnOn != null)
        {
            ButtonToTurnOn.enabled = false;
        }

        if (HighScore != null)
        {
            HighScore.text = $"High Score: <color=yellow>{Points.HighScore}</color>";
        }
    }

    //Most of these functions are mostly stuff like pressing play, wich only makes the screen manager shift to the game screen
    #region Used For Buttons
    public void Play()
    {
        ScreenManager.ChangeScene(ScreenManager.Screen.Play);
    }

    public void ReturnToMenu()
    {
        ScreenManager.ChangeScene(ScreenManager.Screen.Menu);
    }

    public void Store()
    {
        ScreenManager.ChangeScene(ScreenManager.Screen.Store);
    }
    #endregion

    #region From ScreenObject

    public void OnScreenStart()
    {
        gameObject.SetActive(true);

        //I will set the HighScore Text as it should be. 
        if (HighScore != null)
        {
            HighScore.text = $"High Score: <color=yellow>{Points.HighScore}</color>";
            SaveManager.Save();
        }
    }

    public void OnScreenEnd()
    {
        gameObject.SetActive(false);
    }
    #endregion

    public IEnumerator EndPlaytime()
    {
        ButtonToTurnOn.enabled = true;

        //I will Fade in the Menu
        CanvasGroup _postGameScreen = GetComponent<CanvasGroup>();

        float i = 0;
        WaitForSeconds wait = new WaitForSeconds(0.01f);


        while (i < 1)
        {
            _postGameScreen.alpha = i;
            i += 0.1f;
            yield return wait;
        }
    }
}
