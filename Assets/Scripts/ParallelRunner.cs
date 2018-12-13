using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelRunner : MonoBehaviour
{


    public Transform startWire;

    private Camera cam;

    public float lineDrawSpeed;

    private Transform origin;
    private Transform destination;

    private Vector3 stationA;
    private Vector3 stationB;
    private Vector3[] newStations;

    List<Transform> otherWires = new List<Transform>();



    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        lineDrawSpeed = 20;
        origin = GameManager.Instance.batteries[0].transform.GetChild(0).transform;
        destination = GameManager.Instance.batteries[0].transform.GetChild(1).transform;
        GameManager.Instance.runParallelRunner.AddListener(StartRunner);
    }

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(RunParallelCurrent2(startWire));

    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartRunner(Transform startWire)
    {
        Debug.Log("Starting Runner !!");
        StartCoroutine(RunParallelCurrent(startWire));
    }

    Vector3[] GetStations(Transform hitWire)
    {
        Vector3[] stations = { Vector3.zero, Vector3.zero };
        Debug.Log("hitWire Parent Eulers: " + hitWire.parent.eulerAngles.z);
        Debug.Log("hitWire Local Eulers: " + hitWire.localEulerAngles.z);
        BoxCollider collider = hitWire.gameObject.GetComponent<BoxCollider>();


        if (hitWire.parent.eulerAngles.z == 0)
        {
            Debug.Log("Parent Euler 0");
        }

        if (hitWire.parent.eulerAngles.z == 90)
        {
            Debug.Log("Parent Euler 90");
        }

        if (hitWire.parent.eulerAngles.z == 180)
        {
            Debug.Log("Parent Euler 180");

            if (hitWire.localEulerAngles.z == 270)
            {
                stations[0] = collider.transform.position + collider.bounds.size / 2;
                stations[1] = collider.transform.position - collider.bounds.size / 2;
                stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                stations[1].x = stations[0].x;
            }

        }


        if (hitWire.parent.eulerAngles.z == 270)
        {
            Debug.Log("Parent Euler 270");
        }



        /*
        if (hitWire.localEulerAngles.z == 90)
        {
            Debug.Log("Local: " + 90);
            if (hitWire.parent.eulerAngles.z == 180)
            {
                stations[0] = collider.transform.position - collider.bounds.size / 2;
                stations[1] = collider.transform.position + collider.bounds.size / 2;
                stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                stations[1].x = stations[0].x;
            }

        }

        if (hitWire.localEulerAngles.z == 180)
        {
            Debug.Log("Local: " + 180);
            stations[0] = collider.transform.position - collider.bounds.size / 2;
            stations[1] = collider.transform.position + collider.bounds.size / 2;
            stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
            stations[1].y = stations[0].y;
        }

        if (hitWire.localEulerAngles.z == 0)
        {
            Debug.Log("Local: " + 0);
            stations[0] = collider.transform.position - collider.bounds.size / 2;
            stations[1] = collider.transform.position + collider.bounds.size / 2;
            stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
            stations[1].y = stations[0].y;
        }

        if (hitWire.localEulerAngles.z == 270)
        {
            Debug.Log("Local: " + 270);
            if (hitWire.parent.eulerAngles.z == 180)
            {
                Debug.Log("ONE !!");
                stations[0] = collider.transform.position + collider.bounds.size / 2;
                stations[1] = collider.transform.position - collider.bounds.size / 2;
                stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                stations[1].x = stations[0].x;
            }
            if (hitWire.parent.eulerAngles.z == 90)
            {
                Debug.Log("TWO !!");
                stations[0] = collider.transform.position + collider.bounds.size / 2;
                stations[1] = collider.transform.position - collider.bounds.size / 2;
                stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                stations[1].y = stations[0].y;
            }

        }*/
        stations[0].z = 0;
        stations[1].z = 0;
        Debug.Log("Station A: " + stations[0]);
        Debug.Log("Station B: " + stations[1]);
        return stations;
    }

    public IEnumerator RunParallelCurrent(Transform startWire)
    {
        Debug.Log("Starting Parallel Runner !! ");

        //Debug.Log("First Parallel Eulers: " + wire.eulerAngles);
        //Debug.Log("First Parallel Local Eulers: " + wire.localEulerAngles);
        this.startWire = startWire;
        Transform parallel = startWire.transform.parent;

        /*foreach (Transform child in parallel)
        {
            if (child != wire)
            {
                otherWires.Add(child);
            }
        }*/
        Transform currentWire = startWire;

        newStations = GetStations(startWire);

        stationA = newStations[0];

        stationB = newStations[1];

        stationA.z = 0;
        stationB.z = 0;

        //Debug.Log("Station A-2: " + stationA);
        //Debug.Log("Station B-3: " + stationB);


        float dist = Vector3.Distance(stationA, stationB);
        float counter = 0;

        Vector3 pointAlongParallelLine = Vector3.zero;

        int stationIndex = 0;
        bool movingAllowed = true;

        //gameObject.transform.position = stationA;


        while (movingAllowed)
        {
            float x = Mathf.Lerp(0, dist, counter);

            pointAlongParallelLine = x * Vector3.Normalize(stationB - stationA) + stationA;

            //Debug.Log("point along line: " + pointAlongParallelLine);

            //Debug.Log("farClipPane: " + cam.farClipPlane);
            //Debug.Log("nearClipPane: " + cam.nearClipPlane);

            Vector3 mousePosFar = new Vector3(pointAlongParallelLine.x, pointAlongParallelLine.y, cam.farClipPlane);
            Vector3 mousePosNear = new Vector3(pointAlongParallelLine.x, pointAlongParallelLine.y, cam.nearClipPlane);



            //Debug.Log("mousePosFar: " + mousePosFar);
            //Debug.Log("mousePosNear: " + mousePosNear);

            RaycastHit hit;
            Physics.Raycast(mousePosNear, mousePosFar - mousePosNear, out hit, Mathf.Infinity);
            //Debug.DrawRay(mousePosNear, mousePosFar - mousePosNear, Color.red, 30);

            if (hit.collider != null)
            {

                if (hit.collider.gameObject.CompareTag("Line") || hit.collider.gameObject.CompareTag("Resistor") || hit.collider.gameObject.CompareTag("Bulb") || hit.collider.gameObject.CompareTag("Battery"))
                {
                    // gameObject.transform.position != stationB
                    //Debug.Log("Hittt: " +hit.collider.gameObject.name);

                    if (hit.collider.gameObject == currentWire.gameObject && gameObject.transform.position != stationB)
                    {
                        counter += .1f / lineDrawSpeed;
                        x = Mathf.Lerp(0, dist, counter);
                        pointAlongParallelLine = x * Vector3.Normalize(stationB - stationA) + stationA;
                        gameObject.transform.position = pointAlongParallelLine;
                        Debug.Log("runner pos: " + gameObject.transform.position);
                        //Debug.Log("pointAlongLine: " + pointAlongParallelLine.y);
                    }

                    else
                    {
                        movingAllowed = false;

                        if (stationIndex < parallel.childCount - 1)
                        {
                            Debug.Log("Setting new station !! " + stationIndex);
                            counter = 0;
                            newStations = GetStations(hit.collider.gameObject.transform);
                            stationA = newStations[0];
                            stationB = newStations[1];
                            //Debug.Log("Station BB: " + stationB);
                            dist = Vector3.Distance(stationA, stationB);
                            gameObject.transform.position = stationA;
                            currentWire = hit.collider.gameObject.transform;
                            stationIndex++;
                        }

                        if (stationIndex == parallel.childCount - 1 && hit.collider.gameObject == parallel.parent.gameObject)
                        {

                            Debug.Log("To Destination: " + stationIndex);

                            if (HasBattery(parallel.parent))
                            {
                                Debug.Log("Has Battery !!");
                                counter = 0;
                                stationA = stationB;
                                stationB = destination.position;
                                //stationB.x = stationA.x;
                                stationA.z = 0;
                                stationB.z = 0;
                                stationA.y = stationB.y - ((stationB.y - stationA.y) / 2);
                                stationB.y = stationA.y;
                                dist = Vector3.Distance(stationA, stationB);
                                gameObject.transform.position = stationA;
                                currentWire = parallel.parent.transform;
                                stationIndex++;
                                Debug.Log("StationIndex: " + stationIndex);
                                Debug.Log("Station B: " + stationB);
                                Debug.Log("Destination:" + destination.position);
                            }

                        }


                        Debug.Log(gameObject.transform.position == stationB && stationIndex == parallel.childCount);


                        if (gameObject.transform.position == stationB && stationIndex == parallel.childCount)
                        {
                            Debug.Log("To First Wire");
                            Vector3 newSize = parallel.parent.gameObject.GetComponent<BoxCollider>().size;
                            newSize.z = 0.0f;
                            parallel.parent.gameObject.GetComponent<BoxCollider>().size = newSize;
                            counter = 0;
                            stationIndex = -1;
                            stationA = origin.position;

                            stationB = startWire.gameObject.GetComponent<BoxCollider>().transform.position + startWire.gameObject.GetComponent<BoxCollider>().bounds.size / 2;
                            stationA.y = stationB.y;
                            stationA.z = 0;
                            stationB.z = 0;
                            gameObject.transform.position = stationA;
                            currentWire = parallel.parent.transform;

                        }

                        if (stationIndex == parallel.childCount - 1)
                        {
                            Vector3 newSize = parallel.parent.gameObject.GetComponent<BoxCollider>().size;
                            newSize.z = 0.014f;
                            parallel.parent.gameObject.GetComponent<BoxCollider>().size = newSize;


                        }
                        movingAllowed = true;


                    }

                }
                else
                {
                    Debug.Log("Enteresan: " + hit.collider.gameObject.name);

                }

            }

            else
            {
                Debug.Log("Collider Null: " + hit.collider.gameObject.name);
            }
            yield return null;
        }
    }

    bool HasBattery(Transform ParallelParent)
    {
        bool hasBattery = false;
        foreach (Transform child in ParallelParent)
        {
            if (child.CompareTag("Battery"))
            {
                hasBattery = true;
            }
        }
        return hasBattery;
    }

    GameObject GetClosestWire(Transform currentWire)
    {
        Transform closestWire = null;
        float minDist = Mathf.Infinity;
        foreach (Transform wire in otherWires)
        {
            float dist = Vector3.Distance(wire.position, currentWire.transform.position);
            if (dist < minDist)
            {
                closestWire = wire;
            }
        }
        if (otherWires.IndexOf(closestWire) != -1)
        {
            otherWires.Remove(closestWire);
        }
        return closestWire.gameObject;
    }

}
