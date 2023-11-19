using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I make it abstract so that if I want to make new types of boards, its really easy
public abstract class Board : MonoBehaviour
{
    public void GetPoints()
    {
        int pointsToAdd = 5;

        AddPoints(pointsToAdd);
    }

    public void AddPoints(int pointsToAdd)
    {
        Points.AddPoints(pointsToAdd);
    }
}
