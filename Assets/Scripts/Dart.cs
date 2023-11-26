using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class Dart : MonoBehaviour
{
    public Rigidbody Rig;
    [SerializeField] float _timeToLive;
    [SerializeField] TrailRenderer _trail;
    [SerializeField] ParticleSystem _particles;

    //I Destroy the Object after certain time. If it didn´t crash with anything, it probably is too far to see
    IEnumerator Start()
    {
        //I will first get a reference to its Rigidbody
        Rig = GetComponent<Rigidbody>();
        
        //I will make it be afected by the wind
        Wind.Darts.Add(this);

        //I will wait for some time. Eventually, when I said so, it will Destroy itself. This way, Darts that are quite far away will be clansed from the memory. Maybe I could be using Pools and whatnot to make it more efficient, but this will do for now.
        yield return new WaitForSeconds(_timeToLive);

        //I will make the wind stop trying to add a force to me
        Wind.Darts.Remove(this);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // It stops the object form destroying itself.
        StopAllCoroutines();

        //If it hit in a Board, it will get the amount of points it deserve and stick into it
        Board board;
        if(collision.gameObject.TryGetComponent<Board>(out board))
        {
            transform.forward = Rig.velocity.normalized;

            //It makes it "Stick" to the place where it hit.
            Rig.constraints = RigidbodyConstraints.FreezeAll;

            //It will make itself a child of the board, to stay in the same relative position forever
            transform.parent = board.transform;
            _trail.enabled = false;

            //I will instantiate the Particles and make them point towards the Player
            ParticleSystem particle = Instantiate(_particles);
            particle.transform.position = transform.position;
            particle.transform.forward = -transform.forward;

            //This will lead to it´s eventual destruction, but in order to save memory, everything is fair
            board.Darts.Add(gameObject);

            //I will finally recieve the points that I rigthfully deserve
            board.GetPoints(transform.position);
        }

        //It will turn off it´s collider, so that it stops colliding with other things (ex: other Darts)
        GetComponent<BoxCollider>().enabled = false;
    }
    
    
    private void Update()
    {
        //The Dart will point towards where its moving
        transform.forward = Vector3.Lerp(transform.forward , Rig.velocity.normalized, 0.1f);
    }
}
