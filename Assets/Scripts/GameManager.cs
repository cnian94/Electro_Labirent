using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public UnityEvent FindPlayer;

    public bool isCircuitPanelActive = false;

    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> inventory = new List<GameObject>();
    public List<Transform> batteries = new List<Transform>();
    public List<float> batteriesLifeTimes = new List<float>();
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


    [System.Serializable]
    public class SetPanelButtonEvent : UnityEngine.Events.UnityEvent<bool> { }
    public SetPanelButtonEvent setPanelButtonEvent;

    public UnityEvent panelButtonEvent;
    public UnityEvent LevelFinishEvent;
    public UnityEvent ShowLevelFinishButtonsEvent;

    /*
    [System.Serializable]
    public class SetBatteryBarFillAmount : UnityEngine.Events.UnityEvent<float> { }
    public SetBatteryBarFillAmount setBatteryBarFillAmount;
        */

    [System.Serializable]
    public class SetBatteryLifeTimePausedEvent : UnityEngine.Events.UnityEvent<bool> { }
    public SetBatteryLifeTimePausedEvent setBatteryLifeTimePausedEvent;

    public bool isBatteryLifeTimePaused = true;
    public float totalLifeTime;
    public UnityEvent activateBatteryLifeBarEvent;


    [System.Serializable]
    public class AdjustLevelLightEvent : UnityEngine.Events.UnityEvent<bool> { }
    public AdjustLevelLightEvent adjustLevelLightEvent;

    [System.Serializable]
    public class SetLevelLightEvent : UnityEngine.Events.UnityEvent<bool> { }
    public SetLevelLightEvent setLevelLightEvent;

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

    public IEnumerator ReduceBatteriesLife()
    {
        Debug.Log("is Paused 0:" + isBatteryLifeTimePaused);
        while (!isBatteryLifeTimePaused)
        {
            Debug.Log("Reducing life times for:" + _instance.batteriesLifeTimes[0]);
            //_instance.batteriesLifeTimes.ForEach(i => i = i - 1.0f);
            for (int i = 0; i < _instance.batteriesLifeTimes.Count; i++)
            {
                if (_instance.batteriesLifeTimes[i] > 0.0f)
                {
                    _instance.batteriesLifeTimes[i] -= 1.0f;
                }
            }
            Debug.Log("life time important: " + _instance.batteriesLifeTimes[0]);
            yield return new WaitForSeconds(1f);
            /*
            for (int i = 0; i < _instance.batteriesLifeTimes.Count; i++)
            {
                if (_instance.batteriesLifeTimes[i] > 0.0f)
                {
                    _instance.batteriesLifeTimes[i] -= 1.0f;
                    yield return new WaitForSeconds(1f);
                }
            }*/
        }
    }


    public void CalculateTotalLifeTime()
    {
        _instance.totalLifeTime = 0;

        foreach (float time in _instance.batteriesLifeTimes)
        {
            Debug.Log("life time:" + time);
            _instance.totalLifeTime += time;
        }

        Debug.Log("Total lifeeee:" + _instance.totalLifeTime);
    }

    public bool IsCircuitApproved()
    {

        if (_instance.batteries.Count == 0 || _instance.resistors.Count == 0 || _instance.bulbs.Count == 0)
        {
            return false;
        }
        else
        {

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
            if (_instance.batteries.Count == 1 && _instance.resistors.Count == 2 && _instance.bulbs.Count == 1)
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
