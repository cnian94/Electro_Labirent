using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public UnityEvent FindPlayer;

    public bool isCircuitPanelActive = false;

    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> inventory = new List<GameObject>();
    public List<Transform> batteries = new List<Transform>();
    public List<Transform> wires = new List<Transform>();
    public List<GameObject> parallels = new List<GameObject>();

    public Dictionary<GameObject, GameObject> parallelRunners = new Dictionary<GameObject, GameObject>();

    public GameObject MazeGenerator;
    public int xSize;
    public int ySize;

    //public Camera MainCamera;
    //public Camera UICamera;

    public GameObject Maze;

    //public GameObject CurrentRunner;
    //public GameObject ParallelRunner;

    public UnityEvent ShowLevelBeginMessageEvent;
    public UnityEvent ShowLevelTipMessageEvent;

    public UnityEvent panelButtonEvent;
    public UnityEvent LevelFinishEvent;


    [System.Serializable]
    public class AdjustLevelLightEvent : UnityEngine.Events.UnityEvent<bool> { }
    public AdjustLevelLightEvent adjustLevelLightEvent;


    [System.Serializable]
    public class ItemCollectedEvent : UnityEngine.Events.UnityEvent<GameObject> { }
    public ItemCollectedEvent itemCollected;


    [System.Serializable]
    public class DrawEvent : UnityEngine.Events.UnityEvent<bool> { }
    public DrawEvent drawEvent;

    [System.Serializable]
    public class ParallelRunnerEvent : UnityEngine.Events.UnityEvent<Transform, GameObject> { }
    public ParallelRunnerEvent parallelRunner;

    [System.Serializable]
    public class RunParallelRunner : UnityEngine.Events.UnityEvent<Transform, int> { }
    public RunParallelRunner runParallelRunner;


    public bool isDrawingAllowed = false;
    public int maxLines = 50;

    //public int level;

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


    public void Draw()
    {
        if (isDrawingAllowed)
        {
            isDrawingAllowed = false;
            drawEvent.Invoke(false);
        }
        else
        {
            isDrawingAllowed = true;
            drawEvent.Invoke(true);
        }

    }

}
