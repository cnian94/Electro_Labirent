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
    public LayerMask dragLayerMask;
    public LayerMask dropLayermask;

    Vector3 tempScale;

    GameObject beforeParent;

    public GameObject CraftPanel;
    public GameObject InventoryPanel;
    public GameObject inventoryContent;
    public GameObject inventoryDesk;

    //Transform[] tempWires = { };

void Start()
    {
        cam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        beingDragged = false;
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
            Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit, Mathf.Infinity, dragLayerMask);

            if (hit.collider != null)
            {
                // trying to drag another item
                if (!hit.collider.gameObject.CompareTag("Line"))
                {
                    InventoryPanel.GetComponent<ScrollRect>().StopMovement();
                    InventoryPanel.GetComponent<ScrollRect>().enabled = false;
                    Debug.Log("Press Name: " + hit.collider.name);
                    Debug.Log("Press Local Scale: " + hit.transform.lossyScale);
                    draggingObject = hit.collider.gameObject;
                    draggingObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
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
            InventoryPanel.GetComponent<ScrollRect>().enabled = true;
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
            Physics.Raycast(mousePosN, mousePosF - mousePosN, out hit, Mathf.Infinity, dropLayermask);

            //draggingObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

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

                        Vector3 newPos = draggingObject.transform.localPosition;
                        if (draggingObject.CompareTag("Bulb"))
                        {
                            newPos.y = 4;
                        }

                        else
                        {
                            newPos.y = 0;
                        }
                        draggingObject.transform.localPosition = newPos;

                        // coming from right/left wire,  && draggingObject.gameObject.transform.parent.CompareTag("Line")
                        if (beforeParent.gameObject.transform.rotation.eulerAngles.z == 270.0 || beforeParent.gameObject.transform.rotation.eulerAngles.z == 90.0)
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
                        if (draggingObject.CompareTag("Bulb"))
                        {
                            newPos.y = 4;
                        }

                        else
                        {
                            newPos.y = 0;
                        }
                        draggingObject.transform.localPosition = newPos;


                        // coming from top/bottom wire,  draggingObject.gameObject.transform.parent.CompareTag("Line")
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

                        Vector3 newPos = draggingObject.transform.localPosition;
                        if (draggingObject.CompareTag("Bulb"))
                        {
                            newPos.y = 4;
                        }

                        else
                        {
                            newPos.y = 0;
                        }
                        draggingObject.transform.localPosition = newPos;

                        // coming from right/left wire,  && draggingObject.gameObject.transform.parent.CompareTag("Line")
                        if (beforeParent.gameObject.transform.rotation.eulerAngles.z == 270.0 || beforeParent.gameObject.transform.rotation.eulerAngles.z == 90.0)
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
                        Debug.Log("Hit euler: " + hit.collider.gameObject.transform.rotation.eulerAngles);
                        Debug.Log("Hit local euler: " + hit.collider.gameObject.transform.localRotation.eulerAngles);

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
                        if (draggingObject.CompareTag("Bulb"))
                        {
                            newPos.y = 4;
                        }

                        else
                        {
                            newPos.y = 0;
                        }

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
                    draggingObject.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                    //Vector3 newPos = draggingObject.transform.localPosition;
                    //newPos.z = -1;
                    //draggingObject.transform.localPosition = newPos;

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
            draggingObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
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
            newScale.x = 0.4f;
            newScale.y = 10f;
            Vector3 tmpSize = item.GetComponent<BoxCollider>().size;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
            item.GetComponent<BoxCollider>().size = tmpSize;
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = 0.4f;
            newScale.y = 8f;
            float tmpRadius = item.GetComponent<SphereCollider>().radius;
            DestroyImmediate(item.GetComponent<SphereCollider>(), true);
            item.AddComponent<SphereCollider>();
            item.GetComponent<SphereCollider>().radius = tmpRadius;
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = 0.4f;
            newScale.y = 10f;
            Vector3 tmpSize = item.GetComponent<BoxCollider>().size;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
            item.GetComponent<BoxCollider>().size = tmpSize;
        }


        return newScale;
    }

    /*
    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            newScale.x = item.transform.lossyScale.x;
            newScale.y = item.transform.lossyScale.y;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = item.transform.lossyScale.x;
            newScale.y = item.transform.lossyScale.y;
            DestroyImmediate(item.GetComponent<SphereCollider>(), true);
            item.AddComponent<SphereCollider>();
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = item.transform.lossyScale.x;
            newScale.y = item.transform.lossyScale.y;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
        }

        return newScale;
    }*/


}




