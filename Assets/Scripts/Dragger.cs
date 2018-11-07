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

    public EventSystem m_EventSystem;
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;

    PhysicsRaycaster physicRaycaster;
    Physics2DRaycaster physic2DRaycaster;




    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        m_Raycaster = cam.GetComponent<GraphicRaycaster>();
        physic2DRaycaster = cam.GetComponent<Physics2DRaycaster>();
        physicRaycaster = cam.GetComponent<PhysicsRaycaster>();
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


    void Update()
    {

        /*ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            print("NAMEEEE : " + hit.collider.name);
        }*/

        //mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //If we've pressed down on the mouse (or touched on the iphone)
        if (Input.GetMouseButtonDown(0))
        {


            PointerEventData pointerData = new PointerEventData(EventSystem.current);

            pointerData.position = Input.mousePosition; // use the position from controller as start of raycast instead of mousePosition.

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            /*
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }*/




            /*if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                beingDragged = true;
            }*/

            draggingObject = results[0].gameObject.transform.GetChild(0).gameObject;
            startPos = draggingObject.transform.position;
            Debug.Log("Dragging Item " + draggingObject.gameObject.name);
            beingDragged = true;

        }





        if (beingDragged)
        {
            //Set the position to the mouse position
            draggingObject.transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,
                                             cam.ScreenToWorldPoint(Input.mousePosition).y,
                                             0.0f);
        }

        if (Input.GetMouseButtonUp(0))
        {
           PointerEventData pointerData = new PointerEventData(EventSystem.current);

            Debug.Log("MOUSE POS: " + Input.mousePosition);
            Debug.Log("Cam POS: " + cam.transform.position);
            Debug.Log("Cam Pos 2: " + cam.ScreenToWorldPoint(cam.transform.position));
            Debug.Log("Object POS: " + draggingObject.transform.position);

            pointerData.position = Input.mousePosition; // use the position from controller as start of raycast instead of mousePosition.
            beingDragged = false;
            draggingObject.transform.position = startPos;


            List<RaycastResult> results = new List<RaycastResult>();
            //EventSystem.current.RaycastAll(pointerData, results);
            physicRaycaster.Raycast(pointerData, results);


            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);
            }

            //target = results[0].gameObject;

            //Debug.Log("Target: " + target.name);

            /*
            RaycastHit hit;
            Ray forwardRay = new Ray(cam.transform.position, cam.WorldToScreenPoint(cam.transform.position));
            if (Physics.Raycast(forwardRay, out hit))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }*/
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



