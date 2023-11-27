using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsAnimator : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ShowPoints(int points)
    {
        TMP_Text text = GetComponent<TMP_Text>();
        text.text = $"{points} Points";

        Debug.Log("Animating " + points);
        animator.SetTrigger("_AppearText");
        animator.SetFloat("_Points", points);
        animator.Play("Appear 1 Point");
    }

    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
