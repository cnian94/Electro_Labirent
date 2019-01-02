using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelRunner : MonoBehaviour
{


    public Transform startWire;
    GameObject currentRunner;

    private Camera cam;

    public float lineDrawSpeed;

    private Transform origin;
    private Transform destination;

    private Vector3 stationA;
    private Vector3 stationB;
    private Vector3[] newStations;

    List<Transform> otherWires = new List<Transform>();

    Vector3 raycastOrigin;
    Vector3 raycastDirection;

    RaycastHit hit;



    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        currentRunner = GameObject.FindGameObjectWithTag("CurrentRunner");
        //lineDrawSpeed = 50;
        lineDrawSpeed = 12;
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

    void StartRunner(Transform startWire, int ID)
    {
        //Debug.Log("Starting Runner !!");
        if (ID == gameObject.GetInstanceID())
        {
            StartCoroutine(RunParallelCurrent(startWire));
        }

    }

    Vector3[] GetStations(Transform hitWire)
    {
        Vector3[] stations = { Vector3.zero, Vector3.zero };
        //Debug.Log("hitWire Eulers: " + hitWire.transform.rotation);
        BoxCollider collider = hitWire.gameObject.GetComponent<BoxCollider>();


        if (hitWire.parent.eulerAngles.z == 0)
        {
            //Debug.Log("Parent Euler 0");

            if (HasBattery(hitWire.parent.parent))
            {
                lineDrawSpeed = 9;

                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Top Parallel 1:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;

                }

                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Top Parallel 2:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Top Parallel 3:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                }
            }
            else
            {
                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Top Parallel 1:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                }

                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Top Parallel 2:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Top Parallel 3:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                }
            }


        }

        if (hitWire.parent.eulerAngles.z == 90)
        {
            //Debug.Log("Parent Euler 90");

            if (HasBattery(hitWire.parent.parent))
            {
                Debug.Log("Has Battery 2 !!!");
                lineDrawSpeed = 9;

                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Left Parallel 1:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;



                }

                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Left Parallel 2:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;

                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Left Parallel 3:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;

                }

            }

            else
            {
                Debug.Log("No Battery 2 !!!");
                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Left Parallel 1:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                }

                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Left Parallel 2:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Left Parallel 3:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                }
            }



        }

        if (hitWire.parent.eulerAngles.z == 180)
        {
            Debug.Log("Parent Euler 180");

            if (HasBattery(hitWire.parent.parent))
            {
                lineDrawSpeed = 9;
                Debug.Log("Has Battery !!");
                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Bottom Parallel 1:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;

                    /* Today
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                    */
                }

                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Bottom Parallel 3:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;

                    /* today
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                    */
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Bottom Parallel 2:");

                    /* today
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                    */
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                }
            }
            else
            {
                Debug.Log("No Battery !!");
                Debug.Log("parent: " + hitWire.parent.gameObject.name);

                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    stations[0] = collider.transform.position + collider.bounds.size / 2;
                    stations[1] = collider.transform.position - collider.bounds.size / 2;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2);
                    stations[1].y = stations[0].y;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    stations[0] = collider.transform.position - collider.bounds.size / 2;
                    stations[1] = collider.transform.position + collider.bounds.size / 2;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2);
                    stations[1].x = stations[0].x;
                }

            }

        }


        if (hitWire.parent.eulerAngles.z == 270)
        {
            //Debug.Log("Parent Euler 270");

            if (HasBattery(hitWire.parent.parent))
            {
                lineDrawSpeed = 9;

                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Right Parallel 1:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2f;
                    stations[1] = collider.transform.position + collider.bounds.size / 2f;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2f);
                    stations[1].y = stations[0].y;

                }

                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Right Parallel 2:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2f;
                    stations[1] = collider.transform.position - collider.bounds.size / 2f;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2f);
                    stations[1].x = stations[0].x;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Right Parallel 3:");

                    stations[0] = collider.transform.position + collider.bounds.size / 2f;
                    stations[1] = collider.transform.position - collider.bounds.size / 2f;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2f);
                    stations[1].y = stations[0].y;
                }
            }
            else
            {
                if (hitWire.gameObject == hitWire.parent.GetChild(0).gameObject)
                {
                    //Debug.Log("Right Parallel 1:");
                    stations[0] = collider.transform.position - collider.bounds.size / 2f;
                    stations[1] = collider.transform.position + collider.bounds.size / 2f;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2f);
                    stations[1].y = stations[0].y;
                }

                if (hitWire.gameObject == hitWire.parent.GetChild(1).gameObject)
                {
                    //Debug.Log("Right Parallel 2:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2f;
                    stations[1] = collider.transform.position - collider.bounds.size / 2f;
                    stations[0].x = stations[1].x - ((stations[1].x - stations[0].x) / 2f);
                    stations[1].x = stations[0].x;
                }


                if (hitWire.gameObject == hitWire.parent.GetChild(2).gameObject)
                {
                    //Debug.Log("Right Parallel 3:");
                    stations[0] = collider.transform.position + collider.bounds.size / 2f;
                    stations[1] = collider.transform.position - collider.bounds.size / 2f;
                    stations[0].y = stations[1].y - ((stations[1].y - stations[0].y) / 2f);
                    stations[1].y = stations[0].y;
                }
            }



        }

        stations[0].z = 0f;
        stations[1].z = 0f;

        return stations;
    }

    IEnumerator MergeRunners()
    {
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = currentRunner.transform.position;

        while (gameObject.transform.position != targetPosition)
        {
            gameObject.transform.position = Vector3.MoveTowards(startPosition, targetPosition, 7.0f * Time.deltaTime);
            yield return null;
        }

    }


    public IEnumerator RunParallelCurrent(Transform startWire)
    {
        this.startWire = startWire;
        Transform parallel = startWire.transform.parent;

        //Transform currentWire = startWire;

        newStations = GetStations(startWire);

        stationA = newStations[0];

        stationB = newStations[1];


        float dist = Vector3.Distance(stationA, stationB);
        float counter = 0;

        float x = Mathf.Lerp(0, dist, counter);

        Vector3 pointAlongParallelLine = x * Vector3.Normalize(stationB - stationA) + stationA;

        raycastOrigin = pointAlongParallelLine;
        raycastOrigin.z = -5;
        raycastDirection = Vector3.forward * 10.0f;

        int stationIndex = 0;
        bool movingAllowed = true;




        while (movingAllowed)
        {

            //x = Mathf.Lerp(0, dist, counter);
            //pointAlongParallelLine = x * Vector3.Normalize(stationB - stationA) + stationA;

            Color castColor = Color.red;

            if (stationIndex == 0)
            {
                castColor = Color.cyan;
                //Debug.Log("On Station 0 !!");

                Physics.Raycast(raycastOrigin, raycastDirection, out hit);
                //Debug.DrawRay(raycastOrigin, raycastDirection, castColor, 1);
            }

            if (stationIndex == 1)
            {
                castColor = Color.green;
                //Debug.Log("On Station 1 !!");

                Physics.Raycast(raycastOrigin, raycastDirection, out hit);
                //Debug.DrawRay(raycastOrigin, raycastDirection, castColor, 1);
            }

            if (stationIndex == 2)
            {
                castColor = Color.grey;
                //Debug.Log("On Station 2 !!");
                Physics.Raycast(raycastOrigin, raycastDirection, out hit);
                //Debug.DrawRay(raycastOrigin, raycastDirection, castColor, 1);
            }

            if (stationIndex == 3)
            {
                castColor = Color.red;
                //Debug.Log("On Station 3 !!");
                Physics.Raycast(raycastOrigin, raycastDirection, out hit);
                //Debug.DrawRay(raycastOrigin, raycastDirection, castColor, 1);
            }




            if (hit.collider != null)
            {

                if (hit.collider.gameObject.CompareTag("Line") || hit.collider.gameObject.CompareTag("Resistor") || hit.collider.gameObject.CompareTag("Bulb") || hit.collider.gameObject.CompareTag("Battery"))
                {
                    //(hit.collider.gameObject == currentWire.gameObject && gameObject.transform.position != destination.position) || hit.collider.gameObject.CompareTag("Battery") || hit.collider.gameObject.CompareTag("Bulb") || hit.collider.gameObject.CompareTag("Resistor")
                    if (gameObject.transform.position != stationB)
                    {
                        counter += .1f / lineDrawSpeed;
                        x = Mathf.Lerp(0, dist, counter);
                        pointAlongParallelLine = x * Vector3.Normalize(stationB - stationA) + stationA;
                        gameObject.transform.position = pointAlongParallelLine;
                        pointAlongParallelLine.z = raycastOrigin.z;
                        raycastOrigin = pointAlongParallelLine;
                        //Debug.Log("StationIndex: " + stationIndex);
                    }

                    else
                    {

                        movingAllowed = false;
                        if (stationIndex == 0)
                        {
                            //Debug.Log("End Of Station 0 !!");
                            newStations = GetStations(hit.collider.gameObject.transform);
                            stationA = newStations[0];
                            stationB = newStations[1];
                            gameObject.transform.position = stationA;
                            counter = 0;
                            dist = Vector3.Distance(stationA, stationB);
                            movingAllowed = true;
                        }


                        if (stationIndex == 1)
                        {
                            //Debug.Log("End Of Station 1 !!");

                            raycastOrigin.z = 5;
                            raycastDirection = Vector3.back * 10.0f;
                            Physics.Raycast(raycastOrigin, raycastDirection, out hit);
                            Debug.DrawRay(raycastOrigin, raycastDirection, castColor, 1);
                            newStations = GetStations(hit.collider.gameObject.transform);
                            stationA = newStations[0];
                            stationB = newStations[1];
                            Debug.Log("Station A: " + stationA);
                            Debug.Log("Station B: " + stationB);
                            gameObject.transform.position = stationA;
                            counter = 0;
                            dist = Vector3.Distance(stationA, stationB);
                            movingAllowed = true;
                        }

                        if (stationIndex == 2)
                        {
                            //Debug.Log("End Of Station 2 !!");
                            Debug.Log("No Battery !!");
                            Debug.Log("SİLİYORUMMMMM !!");
                            //Debug.Log("Hit: " + hit.collider.gameObject.name + " ---- " + hit.transform.eulerAngles);
                            StartCoroutine(MergeRunners());
                            currentRunner.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 1f;
                            //Debug.Log("Key Name: " + hit.collider.gameObject.transform.parent.transform.parent.gameObject.name);
                            //Debug.Log("Length 1: " + GameManager.Instance.parallelRunners.Count);
                            GameManager.Instance.parallelRunners.Remove(hit.collider.gameObject.transform.parent.transform.parent.gameObject);
                            //Debug.Log("Length 2: " + GameManager.Instance.parallelRunners.Count);
                            Destroy(gameObject);

                            /* Today
                            if (HasBattery(startWire.parent.transform.parent))
                            {
                                Debug.Log("Has Battery !!");
                                Debug.Log("Hit Euler angles: " + hit.transform.eulerAngles);
                                stationA = stationB;
                                if(hit.transform.eulerAngles.z == 180  || hit.transform.eulerAngles.z == 90)
                                {
                                    stationB = startWire.gameObject.GetComponent<BoxCollider>().bounds.center + startWire.gameObject.GetComponent<BoxCollider>().bounds.size / 2;
                                }

                                if(hit.transform.eulerAngles.z == 0 || hit.transform.eulerAngles.z == 270)
                                {
                                    stationB = startWire.gameObject.GetComponent<BoxCollider>().bounds.center - startWire.gameObject.GetComponent<BoxCollider>().bounds.size / 2;
                                }

                                gameObject.transform.position = stationA;
                                counter = 0;
                                dist = Vector3.Distance(stationA, stationB);
                                //movingAllowed = true;
                            }
                            else
                            {
                                Debug.Log("No Battery !!");
                                Debug.Log("SİLİYORUMMMMM !!");
                                //Debug.Log("Hit: " + hit.collider.gameObject.name + " ---- " + hit.transform.eulerAngles);
                                StartCoroutine(MergeRunners());
                                currentRunner.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 1f;
                                //Debug.Log("Key Name: " + hit.collider.gameObject.transform.parent.transform.parent.gameObject.name);
                                //Debug.Log("Length 1: " + GameManager.Instance.parallelRunners.Count);
                                GameManager.Instance.parallelRunners.Remove(hit.collider.gameObject.transform.parent.transform.parent.gameObject);
                                //Debug.Log("Length 2: " + GameManager.Instance.parallelRunners.Count);
                                Destroy(gameObject);
                            }*/


                        }

                        if (stationIndex == 3)
                        {
                            //Debug.Log("End Of Station 3 !!");
                            raycastOrigin.z = -5;
                            raycastDirection = Vector3.forward * 10.0f;
                            newStations = GetStations(startWire);
                            Physics.Raycast(raycastOrigin, raycastDirection, out hit);
                            Debug.DrawRay(raycastOrigin, raycastDirection, castColor, 1);
                            stationA = newStations[0];
                            stationB = newStations[1];
                            gameObject.transform.position = stationA;
                            counter = 0;
                            dist = Vector3.Distance(stationA, stationB);
                            stationIndex = -1;
                            movingAllowed = true;
                        }

                        stationIndex++;
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
