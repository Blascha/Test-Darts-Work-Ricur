using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I make it abstract so that if I want to make new types of boards, its really easy
public abstract class Board : MonoBehaviour
{
    [SerializeField] float[] _distancesForPoints;
    [SerializeField] int[] _pointsToGet;
    private void Start()
    {
        //I check so that at every distance selected, you will recieva a desired amount of points
        if(_distancesForPoints.Length != _pointsToGet.Length)
        {
            Debug.LogError("You should check the distances from the center and the points you will recieve in the dartboard");
            return;
        }

        bool reCheck = false;

        do
        {
            //I check so that the distance board is ordered
            for (int i = 0; i < _distancesForPoints.Length; i++)
            {
                if (_distancesForPoints[i] > _distancesForPoints[i + 1])
                {
                    //I save the large distance
                    float biggerDistance = _distancesForPoints[i];
                    int biggerPoints = _pointsToGet[i];

                    //In it´s place, I place the ones that where smaller
                    _distancesForPoints[i] = _distancesForPoints[i + 1];
                    _pointsToGet[i] = _pointsToGet[i + 1];

                    //Where there was the smaller, I place the big ones
                    _distancesForPoints[i + 1] = biggerDistance;
                    _pointsToGet[i + 1] = biggerPoints;

                    //Im going to reCheck it afterwards, just in case
                    reCheck = true;
                }
            }
        }
        while (!reCheck);
    }

    public void GetPoints(Vector3 dartPos)
    {
        int pointsToAdd = 0;

        //Basing myself int the Distance to the dart, I add diferent amounts of points
        for (int i = 0; i < _distancesForPoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, dartPos) <= _distancesForPoints[i])
            {
                pointsToAdd = _pointsToGet[i];
            }
        }


        AddPoints(pointsToAdd);
    }

    public void AddPoints(int pointsToAdd)
    {
        Points.AddPoints(pointsToAdd);
    }

    //This will help visualize the diferent zones where you will recieve points
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.up * (-1), _distancesForPoints[0]);

        Gizmos.color = Color.blue;
        for (int i = 1; i < _distancesForPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.position + transform.up * (-1), _distancesForPoints[i]);
        }
    }
}
