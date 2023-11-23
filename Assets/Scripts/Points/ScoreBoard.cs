using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    TMP_Text _text;
    [SerializeField] AnimationCurve _growthAndShrinkage;
    [SerializeField] float _waitBetweenGrowSteps;


    void Awake()
    {
        Points.Score = this;
        _text = GetComponent<TMP_Text>();

        _text.text = "0 Points";
    }

    public void ShowPoints(int pointsAmount)
    {
        StartCoroutine(GrowAndShrink());
        _text.text = $"{ pointsAmount} Points";
    }

    public void ShowPoints(int pointsAmount, bool growAndShrink)
    {
        if (growAndShrink)
        {
            StartCoroutine(GrowAndShrink());
        }

        _text.text = $"{ pointsAmount} Points";
    }

    IEnumerator GrowAndShrink()
    {
        float i = 0;
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        _text.fontStyle = FontStyles.Bold;

        while(i < .5f)
        {
            transform.localScale = Vector3.one * _growthAndShrinkage.Evaluate(i);
            i += 0.01f;
            yield return wait;
        }

        _text.fontStyle = FontStyles.SmallCaps;
    }
}
