using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject _dart;
    [Header ("Throw Stats")]
    [SerializeField] float _throwStrength;
    [SerializeField] float _throwAngle;

    //When I press once the Screen, I Throw a Dart
    private void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                ThrowDart();
            }
        }
    }

    void ThrowDart()
    {
        //I Instantiate the dart prefab and save a reference to it.
        GameObject dart = Instantiate(_dart);

        //I make the new instance be in the place where it should be, looking towards where it should look.
        dart.transform.position = transform.position;
        Quaternion rotation = Quaternion.Euler(_throwAngle, 0, 0);
        dart.transform.rotation = rotation;

        //I throw the Dart.
        dart.GetComponent<Rigidbody>().AddForce(dart.transform.forward * _throwStrength, ForceMode.Impulse);
    }
}
