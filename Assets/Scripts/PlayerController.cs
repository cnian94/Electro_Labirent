using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    Vector2 movement;
    public float moveSpeed;

    Rigidbody rb;

    Touch touch;
    Vector3 touchPosition, whereToMove;

    float previousDistanceToTouchPos, currentDistanceToTouchPos;

    public GameObject Explosion;
    public GameObject timer;
    Vector3 tempScale;

    public Vector3 dirInit = Vector3.zero;

    Matrix4x4 calibrationMatrix;

    private bool isColliding = false;
    private bool isMoving = false;

    public float jokerDivider = 21;



    void Awake()
    {
        //moveSpeed = Screen.width / 0.35f; //Joker Control 127 dan sonrasını da değiştir

    }

    // Use this for initialization
    float GetPlayerScaleX()
    {
        float x = Screen.width / 25f;  // yeni playerlar için      Screen.width / 25f  olucak
        return x;
    }
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        CalibrateAccelerometer();
        //Debug.Log("JokerDivider" + jokerDivider);
    }

    //Method for calibration 
    void CalibrateAccelerometer()
    {
        dirInit = Input.acceleration;

        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), dirInit);
        //create identity matrix ... rotate our matrix to match up with down vec
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, rotateQuaternion, new Vector3(1f, 1f, 1f));

        //get the inverse of the matrix
        calibrationMatrix = matrix.inverse;

    }

    //Method to get the calibrated input 
    Vector3 FixAcceleration(Vector3 accelerator)
    {
        Vector3 accel = this.calibrationMatrix.MultiplyVector(accelerator);
        return accel;
    }

    private void FixedUpdate()
    {
        //rb.AddForce(movement); //tilt control açar
    }


    Vector2 _InputDir;
    // Update is called once per frame
    void Update()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //playerCurrentPos = gameObject.transform.position;

        //Vector3 position = gameObject.transform.position;


        //_InputDir = FixAcceleration(Input.acceleration); //tilt control açar
        //movement = new Vector2(_InputDir.x, _InputDir.y) * moveSpeed;  //tilt control açar



        if (isMoving) //dokunmatik oynamak için 
        {
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }


        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.isCircuitPanelActive)
        {
            previousDistanceToTouchPos = 0;
            currentDistanceToTouchPos = 0;
            isMoving = true;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            touchPosition.y = 0;
            whereToMove = (touchPosition - transform.position).normalized;
            rb.velocity = new Vector3(whereToMove.x * moveSpeed, 0.0f, whereToMove.z * moveSpeed);
        }

        if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (Input.GetMouseButtonUp(0) || GameManager.Instance.isCircuitPanelActive) //yeni hareket için
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (isMoving)
        {
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }  //buraya kadar dokunmatik oynamak için

    }

    /*
    void OnTriggerExit()
    {
        if (isColliding)
        {
            isColliding = false; //Allows for another object to be struck by this one
        }
    }*/
}
