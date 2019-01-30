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
    public List<Transform> bulbs = new List<Transform>();
    public List<Transform> resistors = new List<Transform>();
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

    public UnityEvent ShowLevelErrorMessageEvent;
    public UnityEvent ShowLevelBeginMessageEvent;
    public UnityEvent ShowLevelTipMessageEvent;

    public UnityEvent panelButtonEvent;
    public UnityEvent LevelFinishEvent;
    public UnityEvent ShowLevelFinishButtonsEvent;
    public UnityEvent SetBatteryBarFillAmount;
    public UnityEvent ActivateBatteryLifeBarEvent;


    [System.Serializable]
    public class AdjustLevelLightEvent : UnityEngine.Events.UnityEvent<bool> { }
    public AdjustLevelLightEvent adjustLevelLightEvent;

    [System.Serializable]
    public class ChangeLightRangeEvent : UnityEngine.Events.UnityEvent<float> { }
    public ChangeLightRangeEvent changeLightRangeEvent;


    [System.Serializable]
    public class ItemCollectedEvent : UnityEngine.Events.UnityEvent<GameObject> { }
    public ItemCollectedEvent itemCollected;


    /*
    [System.Serializable]
    public class DrawEvent : UnityEngine.Events.UnityEvent<bool> { }
    public DrawEvent drawEvent;*/

    [System.Serializable]
    public class ParallelRunnerEvent : UnityEngine.Events.UnityEvent<Transform, GameObject> { }
    public ParallelRunnerEvent parallelRunner;

    [System.Serializable]
    public class RunParallelRunner : UnityEngine.Events.UnityEvent<Transform, int> { }
    public RunParallelRunner runParallelRunner;

    [System.Serializable]
    public class DragManagerEvent : UnityEngine.Events.UnityEvent<bool> { }
    public DragManagerEvent dragManagerEvent;

    public bool isBatteryLifeTimePaused = false;

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

    public bool IsCircuitApproved()
    {

        if (_instance.batteries.Count == 0 || _instance.resistors.Count == 0 || _instance.bulbs.Count == 0)
        {
            return false;
        }
        else
        {
            _instance.SetBatteryBarFillAmount.Invoke();

            if (_instance.batteries.Count == 1 && _instance.resistors.Count == 1 && _instance.bulbs.Count == 1)
            {
                //light
                _instance.changeLightRangeEvent.Invoke(4);
            }
            if (_instance.batteries.Count == 2 && _instance.resistors.Count == 1 && _instance.bulbs.Count == 1)
            {
                //light*2
                _instance.changeLightRangeEvent.Invoke(8);
            }
            if(_instance.batteries.Count == 1 && _instance.resistors.Count == 2 && _instance.bulbs.Count == 1)
            {
                // light/2
                _instance.changeLightRangeEvent.Invoke(2);
            }
            if (_instance.batteries.Count == 1 && _instance.resistors.Count == 1 && _instance.bulbs.Count == 2)
            {
                // light/2
                _instance.changeLightRangeEvent.Invoke(2);
            }
            if (_instance.batteries.Count == 2 && _instance.resistors.Count == 1 && _instance.bulbs.Count == 2)
            {
                // light
                _instance.changeLightRangeEvent.Invoke(4);
            }
            if (_instance.batteries.Count == 3 && _instance.resistors.Count == 1 && _instance.bulbs.Count == 1)
            {
                // light*3
                _instance.changeLightRangeEvent.Invoke(12);
            }
            if (_instance.batteries.Count == 4 && _instance.resistors.Count == 1 && _instance.bulbs.Count == 1)
            {
                // light*4
                _instance.changeLightRangeEvent.Invoke(16);
            }
            return true;
        }
    }

}
