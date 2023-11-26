using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour, IScreenObject
{
    public static List<Dart> Darts;
    public static Vector3 windStrength;
    [SerializeField] float windMagnitude;
    [SerializeField] Transform windArrow;

    void Awake()
    {
        ScreenManager.AddObjectToScreen(this, ScreenManager.Screen.Play);
    }

    public void OnScreenStart()
    {
        gameObject.SetActive(true);
        CalculateNewWind();
    }

    public void OnScreenEnd()
    {
        Darts = new List<Dart>();
        gameObject.SetActive(false);
    }

    //I will add the force from the wind constantly
    void FixedUpdate()
    {
        windArrow.up = windStrength;

        foreach (Dart i in Darts)
        {
            i.Rig.AddForce(windStrength);
        }
    }

    void CalculateNewWind()
    {
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        float y = 0;
        
        windStrength = new Vector3(x, y, z).normalized * windMagnitude;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, windStrength);
    }
}
