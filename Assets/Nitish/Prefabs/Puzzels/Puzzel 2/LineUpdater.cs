using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineUpdater : MonoBehaviour
{
    public List<GameObject> gameCheckPoints = new List<GameObject>();
    public List<Vector3> checkpoints = new List<Vector3>();

    //private EdgeCollider2D lineCollider;

    public Transform startPos;
    public Transform posHolder;

    public GameObject turnOffTrig;
    public GameObject originalPos;
    public GameObject turnOfIfEnd;

    public bool endReached;
    public bool isTriggerOn;

    //public float lineSepDist;

    public string endName;

    public int checkPointVal;
    public int latestLine;
    public int maxLines;
    public int saveNext;

    public LineRenderer line;

    public void Start()
    {
        endReached = false;
        isTriggerOn = false;

        latestLine = 1;
        checkPointVal = 0;
        line.SetPosition(0, startPos.position);
        line.SetPosition(1, posHolder.position);      
    }

    public void Update()
    {
        //lineCollider.edgeRadius = 0.1f;

        //Vector2[] colliderPoints;
        //colliderPoints = lineCollider.points;
        //colliderPoints[0] = posHolder.position;
        //lineCollider.points = colliderPoints;

        if (endReached == false)
        {
            if (Input.GetButton("Fire1"))
            {
                line.SetPosition(latestLine, posHolder.position);
            }

            if (Input.GetButtonUp("Fire1"))
            {
                line.positionCount = 2;
                line.SetPosition(1, originalPos.transform.position);
                posHolder.position = originalPos.transform.position;

                for(int i = 0; i < checkPointVal; i++)
                {
                    gameCheckPoints[i].GetComponent<Collider>().isTrigger = true;
                }
               
                latestLine = 1;
                checkPointVal = 0;
                gameCheckPoints.Clear();
                checkpoints.Clear();
            }
        }        
    }

    public void OnTriggerEnter(Collider other)
    {
        line.positionCount++;
        latestLine++;       
        checkPointVal++;

        gameCheckPoints.Add(other.gameObject);
        foreach(GameObject go in gameCheckPoints)
        {
            checkpoints.Add(go.transform.position);
            line.SetPosition(checkPointVal, go.transform.position);

            go.GetComponent<Collider>().isTrigger = false;
        }

        if(other.gameObject.name == endName)
        {
            endReached = true;
        }

        if(endReached == true)
        {
            line.positionCount--;
            print("Deze lijn is compleet!");
            turnOfIfEnd.SetActive(false);           
        }
    }
}
