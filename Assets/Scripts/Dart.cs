using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class Dart : MonoBehaviour
{
    Rigidbody _rig;
    [SerializeField] float _timeToLive;

    //I Destroy the Object after certain time. If it didn´t crash with anything, it probably is too far to see
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeToLive);
        Destroy(gameObject);
    }

    //This will make two things happen:
    //  1) It stops the object form destroying itself.
    //  2) It makes it "Stick" to the place where it hit.
    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        _rig = GetComponent<Rigidbody>();
        _rig.constraints = RigidbodyConstraints.FreezeAll;
    }
}
