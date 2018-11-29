using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// © 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net



public class Dragger : MonoBehaviour
{
    public Camera cam;
    private bool beingDragged;
    Vector3 startPos;

    public GameObject draggingObject;
    public GameObject target;

    public float rayLength = 300.0f;
    public LayerMask layermask;

    Vector3 tempScale;

    GameObject beforeParent;

    public GameObject CraftPanel;
    public GameObject inventoryContent;
    public GameObject inventoryDesk;

    //Transform[] tempWires = { };

void Start()
    {
        cam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        beingDragged = false;
    }

    /*
    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }
        }
    }*/

    private void FixedUpdate()
    {

    }

    void Update()
    {

        //If we've pressed down on the mouse (or touched on the iphone)
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane);

            Vector3 mousePosF = cam.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = cam.ScreenToWorldPoint(mousePosNear);
            Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.green);

            //RaycastHit hit = Physics.Raycast(mousePosN, mousePosF - mousePosN);
            RaycastHit hit;
            Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit, Mathf.Infinity);

            if (hit.collider != null)
            {
                // trying to drag another item
                if (!hit.collider.gameObject.CompareTag("Line"))
                {
                    Debug.Log("Press Name: " + hit.collider.name);
                    draggingObject = hit.collider.gameObject;
                    draggingObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    tempScale = draggingObject.gameObject.transform.localScale;
                    //Debug.Log("Temp Scale: " + tempScale);
                    startPos = draggingObject.transform.localPosition;
                    beingDragged = true;
                    beforeParent = draggingObject.transform.parent.gameObject;
                    Debug.Log("Before Parent: " + beforeParent.name);
                }

                // trying to drag wire  
                else
                {
                    Debug.Log("trying to drag wire !!");
                }

            }
        }


        if (beingDragged)
        {
            //Set the position to the mouse position
            draggingObject.transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,
                                             cam.ScreenToWorldPoint(Input.mousePosition).y,
                                             0.0f);
        }

        if (Input.GetMouseButtonUp(0) && draggingObject != null)
        {

            beingDragged = false;

            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane);

            //Vector3 mousePosFar = new Vector3(draggingObject.transform.position.x, draggingObject.transform.position.y, cam.farClipPlane);
            //Vector3 mousePosNear = new Vector3(draggingObject.transform.position.x, draggingObject.transform.position.y, cam.nearClipPlane);

            Vector3 mousePosF = cam.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = cam.ScreenToWorldPoint(mousePosNear);


            Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.green);

            //Debug.Log("Start: " + mousePosNear);
            //Debug.Log("Direction: " + (mousePosFar - mousePosNear));
            RaycastHit hit;
            Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit, Mathf.Infinity);

            draggingObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

            if (hit.collider != null)
            {
                //Debug.Log("cam world: " + cam.ScreenToWorldPoint(Input.mousePosition));
                //Debug.Log("dragging object: " + draggingObject.transform.localPosition);
                //Debug.Log("dragging object world: " + cam.ScreenToWorldPoint(draggingObject.transform.localPosition));
                Debug.Log("Hit: " + hit.collider.gameObject.name + " --- " + hit.collider.gameObject.transform.rotation.eulerAngles);


                //collided with wire
                if (hit.collider.gameObject.CompareTag("Line"))
                {

                    // wire top 0
                    if (hit.collider.transform.rotation.eulerAngles.z == 0.0)
                    {
                        draggingObject.gameObject.transform.SetParent(hit.collider.gameObject.transform);

                        // coming from right/left wire
                        if (beforeParent.gameObject.transform.rotation.eulerAngles.z == 270.0 || beforeParent.gameObject.transform.rotation.eulerAngles.z == 90.0 && draggingObject.gameObject.transform.parent.CompareTag("Line"))
                        {
                            if (beforeParent.CompareTag("Line"))
                            {
                                Debug.Log("Coming from right/left wire !!");
                                draggingObject.gameObject.transform.localScale = tempScale;
                            }

                            else
                            {
                                Debug.Log("Coming from inventory !!");
                                draggingObject.gameObject.transform.localScale = SetItemScale(draggingObject);
                            }
                        }

                        if (draggingObject.CompareTag("Battery"))
                        {
                            Debug.Log("Battery dropped !!");
                            draggingObject.transform.localEulerAngles = new Vector3(0, 0, 180);

                            int startIndex = GameManager.Instance.wires.IndexOf(beforeParent.gameObject.transform);
                            int destIndex = GameManager.Instance.wires.IndexOf(hit.collider.gameObject.transform);
                            List<Transform> tempWires = new List<Transform>(new Transform[GameManager.Instance.wires.Count]);
                            Debug.Log("Temp Wires length: " + tempWires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                int x = (i - (destIndex - startIndex));
                                int newIndex = Mod(x, GameManager.Instance.wires.Count);
                                Debug.Log("New index for " + GameManager.Instance.wires[i].name + " : " + newIndex);
                                tempWires[newIndex] = GameManager.Instance.wires[i];

                            }
                            GameManager.Instance.wires = tempWires;

                            Debug.Log("Wires length: " + GameManager.Instance.wires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                Debug.Log("Wire " + i + " : " + GameManager.Instance.wires[i].name);
                            }
                        }
                        else
                        {
                            Debug.Log("Other item dropped !!");
                            draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        }
                    }

                    // wire right 270
                    if (hit.collider.transform.rotation.eulerAngles.z == 270.0)
                    {
                        draggingObject.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                        Vector3 newPos = draggingObject.transform.localPosition;
                        newPos.y = 0;
                        draggingObject.transform.localPosition = newPos;


                        // coming from top/bottom wire
                        if (beforeParent.gameObject.transform.rotation.eulerAngles.z == 0.0 || beforeParent.gameObject.transform.rotation.eulerAngles.z == 180.0 && draggingObject.gameObject.transform.parent.CompareTag("Line"))
                        {
                            if (beforeParent.CompareTag("Line"))
                            {
                                Debug.Log("Coming from top/bottom wire !!");
                                draggingObject.gameObject.transform.localScale = tempScale;
                            }

                            else
                            {
                                Debug.Log("Coming from inventory !!");
                                draggingObject.gameObject.transform.localScale = SetItemScale(draggingObject);
                            }
                        }

                        if (draggingObject.CompareTag("Battery"))
                        {
                            Debug.Log("Battery dropped !!");
                            draggingObject.transform.localEulerAngles = new Vector3(0, 0, 180);

                            int startIndex = GameManager.Instance.wires.IndexOf(beforeParent.gameObject.transform);
                            int destIndex = GameManager.Instance.wires.IndexOf(hit.collider.gameObject.transform);
                            List<Transform> tempWires = new List<Transform>(new Transform[GameManager.Instance.wires.Count]);
                            Debug.Log("Temp Wires length: " + tempWires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                int x = (i - (destIndex - startIndex));
                                int newIndex = Mod(x, GameManager.Instance.wires.Count);
                                Debug.Log("New index for " + GameManager.Instance.wires[i].name + " : " + newIndex);
                                tempWires[newIndex] = GameManager.Instance.wires[i];

                            }
                            GameManager.Instance.wires = tempWires;

                            Debug.Log("Wires length: " + GameManager.Instance.wires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                Debug.Log("Wire " + i + " : " + GameManager.Instance.wires[i].name);
                            }


                        }
                        else
                        {
                            Debug.Log("Other item dropped !!");
                            draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        }

                    }

                    // wire bottom 180
                    if (hit.collider.transform.rotation.eulerAngles.z == 180)
                    {

                        draggingObject.gameObject.transform.SetParent(hit.collider.gameObject.transform);

                        // coming from right/left wire
                        if (beforeParent.gameObject.transform.rotation.eulerAngles.z == 270.0 || beforeParent.gameObject.transform.rotation.eulerAngles.z == 90.0 && draggingObject.gameObject.transform.parent.CompareTag("Line"))
                        {
                            if (beforeParent.CompareTag("Line"))
                            {
                                Debug.Log("Coming from right/left wire !!");
                                draggingObject.gameObject.transform.localScale = tempScale;
                                if (draggingObject.CompareTag("Battery"))
                                {

                                }
                            }

                            else
                            {
                                Debug.Log("Coming from inventory !!");
                                draggingObject.gameObject.transform.localScale = SetItemScale(draggingObject);
                            }
                        }
                        Debug.Log("Hit euler: " + hit.collider.gameObject.transform.rotation.eulerAngles);
                        Debug.Log("Hit local euler: " + hit.collider.gameObject.transform.localRotation.eulerAngles);
                        //draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        //draggingObject.gameObject.transform.Rotate(hit.collider.gameObject.transform.rotation.eulerAngles);
                        if (draggingObject.CompareTag("Battery"))
                        {
                            Debug.Log("Battery dropped !!");
                            draggingObject.transform.localEulerAngles = new Vector3(0, 0, 180);

                            int startIndex = GameManager.Instance.wires.IndexOf(beforeParent.gameObject.transform);
                            int destIndex = GameManager.Instance.wires.IndexOf(hit.collider.gameObject.transform);
                            List<Transform> tempWires = new List<Transform>(new Transform[GameManager.Instance.wires.Count]);
                            Debug.Log("Temp Wires length: " + tempWires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                int x = (i - (destIndex - startIndex));
                                int newIndex = Mod(x, GameManager.Instance.wires.Count);
                                Debug.Log("New index for " + GameManager.Instance.wires[i].name + " : " + newIndex);
                                tempWires[newIndex] = GameManager.Instance.wires[i];

                            }
                            GameManager.Instance.wires = tempWires;

                            Debug.Log("Wires length: " + GameManager.Instance.wires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                Debug.Log("Wire " + i + " : " + GameManager.Instance.wires[i].name);
                            }
                        }
                        else
                        {
                            Debug.Log("Other item dropped !!");
                            draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        }

                    }


                    // wire left 90
                    if (hit.collider.transform.rotation.eulerAngles.z == 90.0)
                    {
                        //Debug.Log("Before Parent Rotation: " + beforeParent.gameObject.transform.rotation.eulerAngles);
                        draggingObject.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                        Debug.Log("Hit euler: " + hit.collider.gameObject.transform.rotation.eulerAngles);
                        Debug.Log("Hit local euler: " + hit.collider.gameObject.transform.localRotation.eulerAngles);
                        Vector3 newPos = draggingObject.transform.localPosition;
                        newPos.y = 0;
                        draggingObject.transform.localPosition = newPos;

                        // coming from top/bottom wire
                        if (beforeParent.gameObject.transform.rotation.eulerAngles.z == 0.0 || beforeParent.gameObject.transform.rotation.eulerAngles.z == 180.0)
                        {
                            if (beforeParent.CompareTag("Line"))
                            {
                                Debug.Log("Coming from top/bottom wire !!");
                                draggingObject.gameObject.transform.localScale = tempScale;
                            }

                            else
                            {
                                Debug.Log("Coming from inventory !!");
                                draggingObject.gameObject.transform.localScale = SetItemScale(draggingObject);
                            }


                        }

                        if (draggingObject.CompareTag("Battery"))
                        {
                            Debug.Log("Battery dropped !!");
                            draggingObject.transform.localEulerAngles = new Vector3(0, 0, 180);

                            int startIndex = GameManager.Instance.wires.IndexOf(beforeParent.gameObject.transform);
                            int destIndex = GameManager.Instance.wires.IndexOf(hit.collider.gameObject.transform);
                            List<Transform> tempWires = new List<Transform>(new Transform[GameManager.Instance.wires.Count]);
                            Debug.Log("Temp Wires length: " + tempWires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                int x = (i - (destIndex - startIndex));
                                int newIndex = Mod(x, GameManager.Instance.wires.Count);
                                Debug.Log("New index for " + GameManager.Instance.wires[i].name + " : "  + newIndex);
                                tempWires[newIndex] = GameManager.Instance.wires[i];

                            }
                            GameManager.Instance.wires = tempWires;

                            Debug.Log("Wires length: " + GameManager.Instance.wires.Count);
                            for (int i = 0; i < GameManager.Instance.wires.Count; i++)
                            {
                                Debug.Log("Wire " + i + " : " + GameManager.Instance.wires[i].name);
                            }

                        }
                        else
                        {
                            Debug.Log("Other item dropped !!");
                            draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        }
                    }
                }


                //collided with an another object
                else
                {
                    Debug.Log("RETURN TO START POS !!");
                    draggingObject.transform.localPosition = startPos;
                }
            }
            else
            {
                // drop to craft panel
                if (draggingObject.gameObject.transform.position.y >= -5.0f && draggingObject.gameObject.transform.position.y <= 2.5f)
                {
                    Debug.Log("drop to craft panel !!");
                    draggingObject.gameObject.transform.SetParent(CraftPanel.gameObject.transform);
                    Vector3 newPos = draggingObject.transform.localPosition;
                    //newPos.z = -1;
                    draggingObject.transform.localPosition = newPos;

                }
                else
                {
                    Debug.Log("most probably drop to inventory panel !!");
                    GameManager.Instance.inventory.Add(draggingObject);
                    GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
                    desk.name = draggingObject.name;
                    draggingObject.gameObject.transform.SetParent(desk.transform);
                    draggingObject.gameObject.transform.localPosition = Vector3.zero;
                    draggingObject.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

                }
            }
            if (beforeParent.gameObject.CompareTag("Desk"))
            {
                Destroy(beforeParent);
            }
            draggingObject = null;
        }

    }

    int Mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            newScale.x = 0.25f;
            newScale.y = 6f;
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = 0.15f;
            newScale.y = 2.5f;
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = 0.25f;
            newScale.y = 10f;
        }

        DestroyImmediate(item.GetComponent<PolygonCollider2D>(), true);
        item.AddComponent<PolygonCollider2D>();
        return newScale;
    }

    public void Shift(int sourceIndex, int destinationIndex)
    {
        Debug.Log("Shifting !!");
        Transform[] arr = GameManager.Instance.wires.ToArray<Transform>();
        Transform[] temp = new Transform[arr.Length];
        temp[arr.Length - (sourceIndex - destinationIndex)] = arr[destinationIndex];
        Debug.Log("TEMP[1]: " + temp[1]);
        //temp[sourceIndex] = arr[destinationIndex];
        Array.Copy(arr, sourceIndex, temp, destinationIndex, arr.Length - 1);
        arr = temp;
        Debug.Log("arr[0] : " + arr[0]);
        Debug.Log("arr[1] : " + arr[1]);
        Debug.Log("arr[2] : " + arr[2]);
        Debug.Log("arr[3] : " + arr[3]);
        GameManager.Instance.wires = arr.OfType<Transform>().ToList<Transform>();
    }



    public void Shift2()
    {
        Transform[] source = GameManager.Instance.wires.ToArray<Transform>();
        Transform[] destination = new Transform[source.Length];

        for (int i = 0; i < source.Length - 1; i++)
        {
            Array.Copy(source, 1, destination, 0, source.Length - 1);
        }

        Debug.Log("SOURCE[0] : " + source[0]);
        Debug.Log("SOURCE[3] : " + source[3]);
    }

}




