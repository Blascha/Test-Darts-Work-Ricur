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
        _rig = GetComponent<Rigidbody>();
        yield return new WaitForSeconds(_timeToLive);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // It stops the object form destroying itself.
        StopAllCoroutines();

        //It makes it "Stick" to the place where it hit.
        _rig.constraints = RigidbodyConstraints.FreezeAll;

        //If it hit in a Board, it will get the amount of points it deserve
        Board board;
        if(collision.gameObject.TryGetComponent<Board>(out board))
        {
            board.GetPoints(transform.position);

            //It will make itself a child of the board, to stay in the same relative position forever
            transform.parent = board.transform;
        }

        //It will turn off it´s collider, so that it stops colliding with other things (ex: other Darts)
        GetComponent<BoxCollider>().enabled = false;
    }
    
    //With this code, the Dart will point towards where its moving
    private void Update()
    {
        transform.forward = Vector3.Lerp(transform.forward , _rig.velocity.normalized, 0.1f);
    }
}
