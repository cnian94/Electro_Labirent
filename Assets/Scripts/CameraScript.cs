using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public int max_size;
    public GameObject MazeGenerator;
    public GameObject Player;

    //private Vector3 offset;
    public bool isCameraAdjusted = false;


    private void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        max_size = Mathf.Max(MazeGenerator.GetComponent<Maze>().xSize, MazeGenerator.GetComponent<Maze>().ySize);
        gameObject.GetComponent<Camera>().orthographicSize = max_size;
        Vector3 newPosition = transform.position;
        if (MazeGenerator.GetComponent<Maze>().xSize % 2 == 0)
        {
            newPosition.x = 0.0f;
        }
        else
        {
            newPosition.x = 0.5f;
        }

        if (MazeGenerator.GetComponent<Maze>().ySize % 2 == 0)
        {

            newPosition.z = -0.5f;
        }
        else
        {
            newPosition.z = 0.0f;
        }

        newPosition.y = -10;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(-90, 0, 0);

        GameManager.Instance.FindPlayer.AddListener(FindPlayer);

    }

    void FindPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vector3 endPos = Player.transform.position;
        //endPos.x = Screen.width / 58.14f;
        endPos.y = transform.position.y;
        //endPos.z = Screen.height / 124.67f;
        //Debug.Log("POSS: " + Player.transform.position);
        StartCoroutine(ZoomToPlayer(endPos, 2));

    }

    IEnumerator ZoomToPlayer(Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        float x = (gameObject.GetComponent<Camera>().orthographicSize - 4) / seconds;
        Vector3 startingPos = transform.position;
        Color ambient = RenderSettings.ambientLight;
        float color_reduce = 1 / (seconds * 140);
        //Debug.Log("REDUCER: " + color_reduce);
        //RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, Color.black, seconds);
        while (elapsedTime < seconds)
        {
            ambient.r -= color_reduce;
            ambient.g -= color_reduce;
            ambient.b -= color_reduce;
            //Debug.Log("Ambient: " + RenderSettings.ambientLight);
            RenderSettings.ambientLight = ambient;
            gameObject.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            gameObject.GetComponent<Camera>().orthographicSize = gameObject.GetComponent<Camera>().orthographicSize - x * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        RenderSettings.ambientLight = Color.black;
        gameObject.transform.position = end;
        isCameraAdjusted = true;
        Player.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        if (isCameraAdjusted)
        {
            Vector3 newPos = transform.position;
            newPos.x = Player.transform.position.x;
            newPos.z = Player.transform.position.z;
            transform.position = newPos;
        }
    }
}
