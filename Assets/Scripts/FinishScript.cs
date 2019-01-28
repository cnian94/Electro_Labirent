using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colliding with: " + col.gameObject.name);
            GameManager.Instance.LevelFinishEvent.Invoke();
        }

    }
}
