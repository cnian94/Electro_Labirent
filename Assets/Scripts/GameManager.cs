using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public UnityEvent FindPlayer;

    public bool isCircuitPanelActive = false;

    public List<GameObject> inventory = new List<GameObject>();

    public static GameManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("GM").AddComponent<GameManager>()); }
    }

    private static GameManager _instance;

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
    }
}
