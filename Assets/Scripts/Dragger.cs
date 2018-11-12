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
                startPos = draggingObject.transform.position;
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

        if (Input.GetMouseButtonUp(0) && draggingObject)
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
                Debug.Log("Release: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Line"))
                {
                    Debug.Log("hit world: " + cam.ScreenToWorldPoint(hit.collider.gameObject.transform.localPosition));
                    Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
                    Debug.Log("ROTATION: " + hit.collider.transform.rotation);
                    Debug.Log("Dragging parent: " + draggingObject.transform.parent.name);
                    Debug.Log("hit object: " + hit.collider.gameObject.name);
                    if (hit.collider.transform.rotation.z != 0)
                    {

                        if (draggingObject.transform.parent.name != hit.collider.gameObject.name)
                        {

                            Debug.Log("Vertical Line !!");
                            Debug.Log("NEW POS: " + newPos);
                            //float temp_x = newPos.x;
                            if (newPos.x <= 0)
                            {
                                newPos.x = newPos.x / -5;
                            }
                            else
                            {
                                newPos.x = newPos.x / 5;
                            }

                            newPos.y = 0;
                            newPos.z = -1.0f;
                            draggingObject.transform.parent = hit.collider.gameObject.transform;
                            draggingObject.transform.localPosition = newPos;
                            draggingObject.transform.Rotate(new Vector3(0, 0, -90));

                            Debug.Log("ROTATION OF " + hit.collider.gameObject.name + ": " + hit.collider.gameObject.transform.localRotation);
                            Debug.Log("local scale: " + draggingObject.transform.localScale);
                            Vector3 newObjectScale = draggingObject.transform.localScale;
                            float tempScale_X = newObjectScale.x;
                            newObjectScale.x = newObjectScale.y;
                            newObjectScale.y = tempScale_X;
                            draggingObject.transform.localScale = newObjectScale;
                            //draggingObject.transform.Rotate(new Vector3(0, 0, -90));

                        }
                        else
                        {
                            if (newPos.x <= 0)
                            {
                                newPos.x = newPos.x / -5;
                            }
                            else
                            {
                                newPos.x = newPos.x / 5;
                            }

                            newPos.y = 0;
                            newPos.z = -1.0f;
                            draggingObject.transform.localPosition = newPos;
                            //draggingObject.transform.Rotate(new Vector3(0, 0, -90));
                        }
                    }
                }
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



/*
public class Dragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

   Camera UICamera;
   float zAxis = 0;
   Vector3 clickOffset = Vector3.zero;

   // Use this for initialization
   void Start()
   {
       //Debug.Log("Dragger Start !!");
       //Comment this Section if EventSystem system is already in the Scene
       //addEventSystem();


       UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
       UICamera.gameObject.AddComponent<Physics2DRaycaster>();

       zAxis = transform.position.z;

       //gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, UICamera.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
   }


   public void OnBeginDrag(PointerEventData eventData)
   {
       Debug.Log("Drag Begin !!");
       clickOffset = transform.position - UICamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, zAxis));
   }

   public void OnDrag(PointerEventData eventData)
   {
       Debug.Log("Dragin !!");
       //Use Offset To Prevent Sprite from Jumping to where the finger is
       Vector3 tempVec = UICamera.ScreenToWorldPoint(eventData.position) + clickOffset;
       tempVec.z = zAxis; //Make sure that the z zxis never change

       transform.position = tempVec;
   }

   public void OnEndDrag(PointerEventData eventData)
   {
       Debug.Log("Drag End !!");
   }

   //Add Event Syste to the Camera
   void addEventSystem()
   {
       GameObject eventSystem = new GameObject("EventSystem");
       eventSystem.AddComponent<EventSystem>();
       eventSystem.AddComponent<StandaloneInputModule>();
   }
}
*/



