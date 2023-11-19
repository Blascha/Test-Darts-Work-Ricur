using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{
    static int points;

    public static void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        DisplayPoints();
    }

    static void DisplayPoints()
    {
        Debug.Log(points);
    }
}
