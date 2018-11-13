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
                startPos = draggingObject.transform.position;
                beingDragged = true;
            }
        }


        if (beingDragged)
        {
            Debug.Log("Draggin !!");
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
                Debug.Log("cam world: " + cam.ScreenToWorldPoint(Input.mousePosition));
                Debug.Log("dragging object: " + draggingObject.transform.localPosition);
                Debug.Log("dragging object world: " + cam.ScreenToWorldPoint(draggingObject.transform.localPosition));
                Debug.Log("Hit: " + hit.collider.gameObject.name);

                //collided with wire
                if (hit.collider.gameObject.CompareTag("Line"))
                {

                    if (hit.collider.transform.rotation.z != 0)
                    {
                        // drop to an another vertical wire
                        if (draggingObject.transform.parent.name != hit.collider.gameObject.name)
                        {


                            Debug.Log("Vertical Line !!");
   
                            draggingObject.transform.Rotate(new Vector3(0, 0, -90), Space.Self);

                            Debug.Log("ROTATION OF " + hit.collider.gameObject.name + ": " + hit.collider.gameObject.transform.localRotation);
                            Debug.Log("local scale: " + draggingObject.transform.localScale);
                            float temp_x = tempScale.x;
                            tempScale.x = tempScale.y;
                            tempScale.y = temp_x;
                            draggingObject.gameObject.transform.localScale = tempScale;
                            draggingObject.transform.parent = hit.collider.gameObject.transform;

                            Vector3 newPos = draggingObject.transform.localPosition;
                            newPos.z = -1;
                            draggingObject.transform.localPosition = newPos;
                        }

                        // drop to the same vertical wire
                        else
                        {
                            Vector3 newPos = draggingObject.transform.localPosition;
                            newPos.z = -1;
                            draggingObject.transform.localPosition = newPos;
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
            draggingObject = null;
        }

    }
}




