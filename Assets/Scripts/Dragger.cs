using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

// © 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net



public class Dragger : MonoBehaviour
{
    public Camera cam;
    private bool beingDragged;
    Vector3 startPos;

    public GameObject draggingObject;
    public GameObject target;

    public GameObject InventoryContent;

    public float rayLength = 300.0f;
    public LayerMask layermask;

    Vector3 tempScale;

    GameObject beforeParent;

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
        //&& !EventSystem.current.IsPointerOverGameObject()
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane);

            Vector3 mousePosF = cam.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = cam.ScreenToWorldPoint(mousePosNear);
            Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.green);

            RaycastHit2D hit = Physics2D.Raycast(mousePosN, mousePosF - mousePosN);

            if (hit.collider != null)
            {
                Debug.Log("Press Name: " + hit.collider.name);
                draggingObject = hit.collider.gameObject;
                tempScale = draggingObject.gameObject.transform.localScale;
                Debug.Log("Temp Scale: " + tempScale);
                startPos = draggingObject.transform.localPosition;
                beingDragged = true;
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

            Vector3 mousePosF = cam.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = cam.ScreenToWorldPoint(mousePosNear);
            Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.green);

            RaycastHit2D hit = Physics2D.Raycast(mousePosN, mousePosF - mousePosN);

            if (hit.collider != null)
            {
                //Debug.Log("cam world: " + cam.ScreenToWorldPoint(Input.mousePosition));
                //Debug.Log("dragging object: " + draggingObject.transform.localPosition);
                //Debug.Log("dragging object world: " + cam.ScreenToWorldPoint(draggingObject.transform.localPosition));
                Debug.Log("Hit: " + hit.collider.gameObject.name + " --- " + hit.collider.gameObject.transform.rotation.eulerAngles);


                beforeParent = draggingObject.transform.parent.gameObject;
                Debug.Log("Before Parent: " + beforeParent.name);


                if (draggingObject.gameObject == hit.collider.gameObject)
                {
                    Vector3 newPos = draggingObject.transform.localPosition;
                    newPos.z = -1;
                    draggingObject.transform.localPosition = newPos;
                }

                else
                {
                    //collided with wire
                    if (hit.collider.gameObject.CompareTag("Line"))
                    {

                        // wire top 0
                        if (hit.collider.transform.rotation.eulerAngles.z == 0.0)
                        {
                            //Debug.Log("Before Wire Rotation: " + beforeParent.gameObject.transform.rotation.eulerAngles);
                            //Debug.Log("Dragging Object Rotation : " + draggingObject.gameObject.transform.rotation);
                            //Debug.Log("Wire Rotation: " + hit.collider.gameObject.transform.rotation.eulerAngles);

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

                            draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        }

                        // wire right 270
                        if (hit.collider.transform.rotation.eulerAngles.z == 270.0)
                        {
                            draggingObject.gameObject.transform.SetParent(hit.collider.gameObject.transform);


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

                            draggingObject.gameObject.transform.rotation = hit.collider.transform.rotation;

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
                                }

                                else
                                {
                                    Debug.Log("Coming from inventory !!");
                                    draggingObject.gameObject.transform.localScale = SetItemScale(draggingObject);
                                }
                            }

                            draggingObject.gameObject.transform.rotation = hit.collider.gameObject.transform.rotation;
                        }


                        // wire left 90
                        if (hit.collider.transform.rotation.eulerAngles.z == 90.0)
                        {
                            Debug.Log("Before Parent Rotation: " + beforeParent.gameObject.transform.rotation.eulerAngles);
                            draggingObject.gameObject.transform.SetParent(hit.collider.gameObject.transform);


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

                            draggingObject.gameObject.transform.rotation = hit.collider.transform.rotation;
                        }
                    }


                    //collided with an another object
                    else
                    {
                        Debug.Log("RETURN TO START POS !!");
                        draggingObject.transform.localPosition = startPos;
                    }
                }
            }
            draggingObject = null;
        }

    }

    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            newScale.x = 0.3f;
            newScale.y = 5f;
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = 0.2f;
            newScale.y = 3f;
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = 0.4f;
            newScale.y = 9f;
        }
        return newScale;
    }
}




