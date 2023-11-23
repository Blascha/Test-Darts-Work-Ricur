using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour , IScreenObject
{
    [SerializeField] GameObject _dart;
    [Header ("Throw Stats")]
    [SerializeField] float _throwStrength;
    [SerializeField] float _throwAngle;

    public static Shooter Instance;

    public static int MaxShots = 5;
    public int ShotsMade;

    [SerializeField] CanvasGroup _postGameScreen;

    private void Awake()
    {
        ScreenManager.AddObjectToScreen(this, ScreenManager.Screen.Play);
        
        Instance = this;
    }

    #region When Screen Starts and End
    public void OnScreenStart()
    {
        //I turn on the Player
        gameObject.SetActive(true);

        //I turn off the menu that appears when you finished a playthrough
        _postGameScreen.alpha = 0;
        _postGameScreen.interactable = false;

        //I reset the shot and points count
        ShotsMade = 0;

        //I reset the UI Elements
        Points.ResetPoints();
        ShotsMadeUI.instance.ResetShots();
    }

    public void OnScreenEnd()
    {
        gameObject.SetActive(false);
        ShotsMade = 0;
    }
    #endregion

    //When I press once the Screen, I Throw a Dart
    private void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                ThrowDart();
            }
        }
    }

    void ThrowDart()
    {
        if (ShotsMade < MaxShots)
        {
            //I Instantiate the dart prefab and save a reference to it.
            GameObject dart = Instantiate(_dart);

            //I make the new instance be in the place where it should be, looking towards where it should look.
            dart.transform.position = transform.position;
            Quaternion rotation = Quaternion.Euler(_throwAngle + transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            dart.transform.rotation = rotation;

            //I throw the Dart.
            dart.GetComponent<Rigidbody>().AddForce(dart.transform.forward * _throwStrength, ForceMode.Impulse);

            //I add one to the amount of shots made
            ShotsMade++;

            //I make the UI Display the new amount
            ShotsMadeUI.instance.DisplayShots(ShotsMade);

            //If I made all the shots that I can, I will end the game
            if (ShotsMade >= MaxShots)
            {
                _postGameScreen.interactable = true;
                StartCoroutine(_postGameScreen.GetComponent<MainMenu>().EndPlaytime());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position , Quaternion.AngleAxis(_throwAngle , transform.right) * transform.forward * 5);
    }
}
