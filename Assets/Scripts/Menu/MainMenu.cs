using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent (typeof(CanvasGroup))]
public class MainMenu : MonoBehaviour
{
    enum Scenes
    {
        MainMenu,
        Play,
        Store
    }
    [SerializeField] Button ButtonToTurnOn;

    private void Start()
    {
        if (ButtonToTurnOn != null)
        {
            ButtonToTurnOn.enabled = false;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene((int)Scenes.Play);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }

    public void Store()
    {
        SceneManager.LoadScene((int)Scenes.Store);
    }

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
