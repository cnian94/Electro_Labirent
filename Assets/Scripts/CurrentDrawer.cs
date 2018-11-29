using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentDrawer : MonoBehaviour
{

    public GameObject circuitButton;
    public GameObject againButton;
    public GameObject RunCurrentButton;
    public Text currentText;
    public Camera cam;

    private float counter;
    private int currentLapCount;
    private float dist;

    public Transform origin;
    public Vector3 stationA;
    public Vector3 stationB;
    public Transform destination;

    Vector3 pointAlongLine;

    public float lineDrawSpeed;
    private int stationIndex;

    private bool currentAllowed;
    Transform startWire;
    Transform currentWire;
    bool isAllWiresViseted = false;



    // Use this for initialization
    void Start()
    {
        lineDrawSpeed = 30;
    }

    private void OnEnable()
    {
        currentLapCount = 0;
        ResetRunnerPos();
        StartCoroutine(DrawCurrent());
    }

    public void ResetRunnerPos()
    {
        float toEndDist = 0f;
        startWire = GameManager.Instance.batteries[0].transform.parent.transform;
        currentWire = GameManager.Instance.batteries[0].transform.parent.transform;
        stationIndex = 0;
        origin = GameManager.Instance.batteries[0].transform.GetChild(0).transform;
        destination = GameManager.Instance.batteries[0].transform.GetChild(1).transform;
        stationA = origin.position;
        Vector3 newStationPosB = origin.position;

        // left wire
        if (startWire.eulerAngles.z == 90)
        {
            //Debug.Log("Start Wire: " + startWire.gameObject.name);
            toEndDist = startWire.position.y + (startWire.gameObject.GetComponent<Renderer>().bounds.size.y / 2);
            newStationPosB.y = toEndDist;
        }

        // wire top
        if (startWire.eulerAngles.z == 0)
        {
            toEndDist = startWire.position.x + (startWire.gameObject.GetComponent<Renderer>().bounds.size.x / 2);
            newStationPosB.x = toEndDist;
        }

        //right wire
        if (startWire.eulerAngles.z == 270)
        {
            toEndDist = startWire.position.y - (startWire.gameObject.GetComponent<Renderer>().bounds.size.y / 2);
            newStationPosB.y = toEndDist;
        }

        //bottom wire
        if (startWire.eulerAngles.z == 180)
        {
            toEndDist = startWire.position.x - (startWire.gameObject.GetComponent<Renderer>().bounds.size.x / 2);
            newStationPosB.x = toEndDist;
        }

        stationB = newStationPosB;
        //Debug.Log("first start pos : " + stationA);
        //Debug.Log("first end pos : " + stationB);

        gameObject.transform.position = origin.position;
        dist = Vector3.Distance(stationA, stationB);
        counter = 0.0f;
        currentAllowed = true;
    }

    Vector3 SetStationB(Transform newWire)
    {

        Vector3 newEndPos = Vector3.zero;
        float toEndDist = 0f;

        if (!newWire)
        {

            //Debug.Log("HERE CURRENT WIRE: " + currentWire.name);
            newEndPos = destination.position;

            /*if (!isAllWiresViseted)
            {
                Debug.Log("Not all wires viseted !!");
                stationIndex = 0;
            }*/

        }

        else
        {
            newEndPos = newWire.position;
            // left wire
            if (newWire.eulerAngles.z == 90)
            {
                toEndDist = newWire.position.y + (newWire.gameObject.GetComponent<Renderer>().bounds.size.y / 2);
                newEndPos.y = toEndDist;
            }

            // wire top
            if (newWire.eulerAngles.z == 0)
            {
                toEndDist = newWire.position.x + (newWire.gameObject.GetComponent<Renderer>().bounds.size.x / 2);
                newEndPos.x = toEndDist;
            }

            //right wire
            if (newWire.eulerAngles.z == 270)
            {
                toEndDist = newWire.position.y - (newWire.gameObject.GetComponent<Renderer>().bounds.size.y / 2);
                newEndPos.y = toEndDist;
            }

            //bottom wire
            if (newWire.eulerAngles.z == 180)
            {
                toEndDist = newWire.position.x - (newWire.gameObject.GetComponent<Renderer>().bounds.size.x / 2);
                newEndPos.x = toEndDist;
            }
        }
        return newEndPos;
    }

    Transform GetClosestWire()
    {
        //Debug.Log("COND 1: " + (currentWire != startWire));
        //Debug.Log("COND 2: " + (stationIndex == 0));
        //Debug.Log("COND 3: " + (stationIndex <= GameManager.Instance.wires.Count - 1));
        Debug.Log("Count: " + GameManager.Instance.wires.Count);
        Debug.Log("Station Index: " + stationIndex);
        //(currentWire != startWire || stationIndex == 1) && (stationIndex <= GameManager.Instance.wires.Count - 1)
        //stationIndex < GameManager.Instance.wires.Count - 1
        if (stationIndex < GameManager.Instance.wires.Count - 1)
        {
            /*if(stationIndex == GameManager.Instance.wires.Count - 1)
            {
                Debug.Log("No More Wire !!");
                return null;
            }*/

            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            Debug.Log("Current Wire:" + currentWire.name);
            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
            {
                float dist = Vector3.Distance(GameManager.Instance.wires[i].position, currentPos);
                Debug.Log("Comparing: " + GameManager.Instance.wires[i].name);
                Debug.Log(GameManager.Instance.wires[i].name != currentWire.name);
                if (dist < minDist && GameManager.Instance.wires[i].name != currentWire.name)
                {
                    //Debug.Log("GİRDİİİİİİİ !!!");
                    tMin = GameManager.Instance.wires[i];
                    Debug.Log("Current TMin: " + tMin.gameObject.name);
                    minDist = dist;
                }
            }
            //Debug.Log("Wires Length: " + GameManager.Instance.wires.Count);
            currentWire = tMin;
            return tMin;
        }
        else
        {
            Debug.Log("No More Wire !!");
            return null;
        }

    }


    IEnumerator DrawCurrent()
    {
        while (currentAllowed)
        {
            float x = Mathf.Lerp(0, dist, counter);

            pointAlongLine = x * Vector3.Normalize(stationB - stationA) + stationA;

            Vector3 mousePosFar = new Vector3(pointAlongLine.x, pointAlongLine.y, cam.farClipPlane);
            Vector3 mousePosNear = new Vector3(pointAlongLine.x, pointAlongLine.y, cam.nearClipPlane);

            RaycastHit2D hit = Physics2D.Raycast(mousePosNear, mousePosFar - mousePosNear);
            //Debug.Log("HİTTTT !!!");
            if (hit.collider != null)
            {

                if (hit.collider.gameObject.CompareTag("Line") || hit.collider.gameObject.CompareTag("Resistor") || hit.collider.gameObject.CompareTag("Bulb") || hit.collider.gameObject.CompareTag("Battery"))
                {
                    //Debug.Log("LİNE NAME: " + hit.collider.gameObject.name);
                    //Debug.Log("Hit: " + hit.collider.name);

                    //counter < dist
                    if (gameObject.transform.position != stationB)
                    {
                        if (hit.collider.gameObject.CompareTag("Line") && GameManager.Instance.wires.IndexOf(hit.collider.gameObject.transform) == -1)
                        {
                            //currentAllowed = false;
                            Debug.Log("Adding Wire: " + hit.collider.name);
                            GameManager.Instance.wires.Add(hit.collider.gameObject.transform);
                            //currentAllowed = true;
                        }

                        //counter += .1f / (lineDrawSpeed * dist);
                        counter += .1f / lineDrawSpeed / dist;
                        x = Mathf.Lerp(0, dist, counter);
                        pointAlongLine = x * Vector3.Normalize(stationB - stationA) + stationA;
                        gameObject.transform.position = pointAlongLine;
                    }

                    else
                    {
                        currentAllowed = false;
                        if (gameObject.transform.position != destination.position)
                        {
                            stationA = stationB;
                            Debug.Log("new station !!" + stationA);
                            gameObject.transform.position = stationA;
                            stationB = SetStationB(GetClosestWire());
                            stationIndex++;
                            dist = Vector3.Distance(stationA, stationB);
                            counter = 0.0f;
                            currentAllowed = true;
                        }

                        else
                        {
                            Debug.Log("Starting new lap !!");
                            currentLapCount++;
                            ResetRunnerPos();

                            if (currentLapCount == 3)
                            {
                                Debug.Log("Circuit Approved !!");
                                circuitButton.GetComponent<Animator>().SetTrigger("Reveal");
                            }
                        }
                    }
                }

            }
            // Circuit crashed
            else
            {
                currentAllowed = false;
                Debug.Log("Circuit Crashed !!");
                currentText.text = "Something Wrong !!";
                currentText.GetComponent<Text>().color = Color.red;
                currentText.gameObject.SetActive(true);
                circuitButton.gameObject.SetActive(false);
                RunCurrentButton.gameObject.SetActive(false);
                againButton.gameObject.SetActive(true);
                gameObject.SetActive(false);

            }
            yield return null;
        }
    }

    public void TryAgain()
    {
        currentText.gameObject.SetActive(false);
        //length = GameManager.Instance.stations.ToArray().Length;
        //stationIndex = 0;
        //origin = GameManager.Instance.stations[stationIndex];
        //destination = GameManager.Instance.stations[stationIndex + 1];
        //gameObject.transform.position = origin.position;
        //dist = Vector3.Distance(origin.position, destination.position);
        //counter = 0.0f;
        againButton.gameObject.SetActive(false);
        RunCurrentButton.gameObject.SetActive(true);
        circuitButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
