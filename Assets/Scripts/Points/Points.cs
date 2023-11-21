using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{
    static int points;
    public static ScoreBoard Score;

    public static void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        DisplayPoints();
    }

    static void DisplayPoints()
    {
        Score.ShowPoints(points);
        Debug.Log(points);
    }
}
