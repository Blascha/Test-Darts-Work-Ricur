using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{
    static int _points;
    public static ScoreBoard Score;

    public static void AddPoints(int pointsToAdd)
    {
        _points += pointsToAdd;
        DisplayPoints();
    }

    static void DisplayPoints()
    {
        Score.ShowPoints(_points);
        Debug.Log(_points);
    }
}
