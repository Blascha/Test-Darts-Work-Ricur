using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Points
{
    static int _points;
    public static ScoreBoard Score;
    public static int HighScore;

    public static void AddPoints(int pointsToAdd)
    {
        //I will add the amount requested by the board to my points
        _points += pointsToAdd;

        //If it surpasses the Current HighScore, it will save it
        if(_points > HighScore)
        {
            HighScore = _points;
            Debug.Log(HighScore);
        }

        //Finally, it will display them
        DisplayPoints();
    }

    public static void ResetPoints()
    {
        //It will set it´s current points amount to 0 and display that
        _points = 0;
        Score.ShowPoints(0 , false);
    }

    static void DisplayPoints()
    {
        //It will Display the current point amount
        Score.ShowPoints(_points);
    }
}
