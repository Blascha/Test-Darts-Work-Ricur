using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I make it abstract so that if I want to make new types of boards, its really easy
public abstract class Board : MonoBehaviour , IScreenObject
{
    [Header("Points")]
    [SerializeField] float[] _distancesForPoints;
    [SerializeField] int[] _pointsToGet;

    [Header("Distance Variation")]
    [SerializeField] AnimationCurve _dificultySpike;
    [SerializeField] int _maxDistance;
    [SerializeField] int _minDistance;

    int _amountOfShotsTaken = 0;

    [SerializeField] bool _switchingPlace;

    [Header ("Disappear and Dissapear")]
    [SerializeField] AnimationCurve _disappearCurve;
    [SerializeField] AnimationCurve _appearCurve;
    [SerializeField] float _disapearTime;

    public List<GameObject> Darts = new List<GameObject>();

    public void OnScreenStart()
    {
        Darts = new List<GameObject>();

        //I move the Board to where it should be and watch towards the player, then I turn it on
        transform.position = Vector3.forward * _minDistance;
        transform.up = transform.position - Shooter.Instance.transform.position;
        
        _amountOfShotsTaken = 0;


        gameObject.SetActive(true);
    }

    public void OnScreenEnd()
    {
        //This will make it so that the board isn´t slowly filling with darts from old playthroughs, then it will turn it on
        foreach(GameObject i in Darts)
        {
            Destroy(i);
        }

        gameObject.SetActive(false);
    }

    private void Start()
    {
        //I move the Board to where it should be and watch towards the player, then I turn it on
        transform.position = Vector3.forward * _minDistance;
        transform.up = transform.position - Shooter.Instance.transform.position;

        ScreenManager.AddObjectToScreen(this, ScreenManager.Screen.Play);
        DrawBorders();

        //All of the Following are warnings for Debuggin. I wanted to make them automized, but it made everything go slowly

        //This will make it run only when in the Editor. It wont run during build time.
        #if UNITY_EDITOR

        //I check so that at every distance selected, you will recieva a desired amount of points
        if (_distancesForPoints.Length != _pointsToGet.Length)
        {
            Debug.LogError("You should check the distances from the center and the points you will recieve in the dartboard");
            return;
        }

        //If Distances are ordered wrongly, I send a Warning
        for(int i = 0 ; i < _distancesForPoints.Length - 1; i++)
        {
            if(_distancesForPoints[i] > _distancesForPoints[i + 1])
            {
                Debug.LogError("Board Distances are ordered Poorly. They should go from LOWEST TO HIGHEST");
            }
        }

        //If Points are ordered weirdly, I send a Warning
        for (int i = 0; i < _pointsToGet.Length - 1; i++)
        {
            if (_pointsToGet[i] < _pointsToGet[i + 1])
            {
                Debug.Log("<color=red> Board Points are Weirdly Ordered. They should go from HIGHEST TO LOWEST</color>");
            }
        }

        //If Distances are diferent to Points, I send a Warning
        if (_pointsToGet.Length != _distancesForPoints.Length)
        {
            Debug.LogError("Board Distances and Points should be equally ordered");
        }
        #endif
    }

    private void OnEnable()
    {
        Debug.Log("Enabled");
    }

    public void GetPoints(Vector3 dartPos)
    {
        int pointsToAdd = 0;

        //I check how many points I should add, basing myself on the distance to the center
        for (int i = 0; i < _distancesForPoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, dartPos) <= _distancesForPoints[i])
            {
                pointsToAdd = _pointsToGet[i];
                break;
            }
        }

        //I actually add the points
        AddPoints(pointsToAdd);

        //Finally, I move the board around
        if (Shooter.Instance.ShotsMade != Shooter.MaxShots && _switchingPlace)
        {
            StartCoroutine("Disapear");
        }

    }

    public void AddPoints(int pointsToAdd)
    {
        Points.AddPoints(pointsToAdd);
    }

    //This function will mke it so that the board displays the correct borders
    void DrawBorders()
    {
        //I get the Mesh renderer and the material
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Material mat = mesh.material;

        //I set the new distances
        mat.SetFloat("_Distance2", _distancesForPoints[0]);
        mat.SetFloat("_Distance3", _distancesForPoints[1]);
    }

    //This will help visualize the diferent zones where you will recieve points
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.up * (-1), _distancesForPoints[0]);

        Gizmos.color = Color.blue;
        for (int i = 1; i < _distancesForPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.position + transform.up * (-1), _distancesForPoints[i]);
        }
    }

    private void Move()
    {
        //I will get a random direction
        Vector3 randomDir = GetRandomDir();

        //Then, I will get the distance from the player. This distance will increase, making the game harder every shot.
        float distance = _dificultySpike.Evaluate((float)(Shooter.Instance.ShotsMade) / Shooter.MaxShots) * (_maxDistance - _minDistance) + _minDistance;
        transform.position = Shooter.Instance.transform.position + randomDir.normalized * distance;

        //Finally, I will make it look towards the Player. If not, scoring would be a hassle
        transform.up = transform.position - Shooter.Instance.transform.position;

        Debug.Log($"<color=black>Distance to center {transform.position.magnitude}</color>");
    }

    //I will Generate a Random Position, and I try to make it so that its not really vertical. That would be quite anoying.
    Vector3 GetRandomDir()
    {
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);

        float yRange = Mathf.Min(Mathf.Repeat(x , 5), Mathf.Repeat(z , 5));

        float y = Random.Range(-yRange , yRange);

        return new Vector3(x , y , z);
    }

    IEnumerator Disapear()
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        Vector3 normalScale = transform.localScale;

        for (float i = 0; i <= _disapearTime; i += 0.01f)
        {
            transform.localScale = normalScale * _disappearCurve.Evaluate(i);
            yield return wait;
        }

        Move();

        StartCoroutine(Appear(normalScale));
    }

    IEnumerator Appear(Vector3 normalScale)
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        for (float i = 0; i <= _disapearTime; i += 0.01f)
        {
            transform.localScale = normalScale * _appearCurve.Evaluate(i);
            yield return wait;
        }
    }
}
