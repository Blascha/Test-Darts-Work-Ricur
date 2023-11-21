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
        DrawBorders();

        //I check so that at every distance selected, you will recieva a desired amount of points
        if (_distancesForPoints.Length != _pointsToGet.Length)
        {
            Debug.LogError("You should check the distances from the center and the points you will recieve in the dartboard");
            return;
        }

        //bool recheck = false;

        //do
        //{
        //    //I check so that the distance board is ordered
        //    for (int i = 0; i < _distancesForPoints.Length - 1; i++)
        //    {
        //        if (_distancesForPoints[i] > _distancesForPoints[i + 1])
        //        {
        //            //i save the large distance
        //            float biggerdistance = _distancesForPoints[i];
        //            int biggerpoints = _pointsToGet[i];

        //            //in it´s place, i place the ones that where smaller
        //            _distancesForPoints[i] = _distancesForPoints[i + 1];
        //            _pointsToGet[i] = _pointsToGet[i + 1];

        //            //where there was the smaller, i place the big ones
        //            _distancesForPoints[i + 1] = biggerdistance;
        //            _pointsToGet[i + 1] = biggerpoints;

        //            //im going to recheck it afterwards, just in case
        //            recheck = true;
        //        }
        //    }
        //}
        //while (!recheck);
    }

    public void GetPoints(Vector3 dartPos)
    {
        int pointsToAdd = 0;

        //Basing myself int the Distance to the dart, I add diferent amounts of points
        for (int i = 0; i < _distancesForPoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, dartPos) <= _distancesForPoints[i])
            {
                Debug.Log(Vector3.Distance(transform.position, dartPos) + " : " + _distancesForPoints[i]);
                pointsToAdd = _pointsToGet[i];
                break;
            }
        }

        AddPoints(pointsToAdd);
    }

    public void AddPoints(int pointsToAdd)
    {
        Points.AddPoints(pointsToAdd);
    }

    //This function will mke it so that the board displays the correct borders
    void DrawBorders()
    {
        //I get the Mesh renderer and the material
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Material mat = mesh.material;

        //I set the new distances
        mat.SetFloat("_Distance2", _distancesForPoints[0]);
        mat.SetFloat("_Distance3", _distancesForPoints[1]);
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
