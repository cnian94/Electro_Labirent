﻿using System.Collections;
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
        endPos.y = transform.position.y;
        Debug.Log("POSS: " + Player.transform.position);
        //offset = transform.position - Player.transform.position;
        StartCoroutine(ZoomToPlayer(endPos, 2));

    }

    IEnumerator ZoomToPlayer(Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        float x = (gameObject.GetComponent<Camera>().orthographicSize - 4) / seconds;
        Vector3 startingPos = transform.position;
        while (elapsedTime < seconds)
        {
            gameObject.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            gameObject.GetComponent<Camera>().orthographicSize = gameObject.GetComponent<Camera>().orthographicSize - x * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.transform.position = end;
        isCameraAdjusted = true;
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
