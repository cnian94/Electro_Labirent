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
    //public List<Transform> stations = new List<Transform>();
    public List<Transform> batteries = new List<Transform>();
    public List<Transform> wires = new List<Transform>();

    public GameObject MazeGenerator;
    public int xSize;
    public int ySize;

    public Camera MainCamera;
    public Camera UICamera;

    public GameObject Maze;

    public GameObject CircuitCanvas;
    public GameObject panelOpenButton;
    public GameObject circuitPanel;
    public GameObject craftPanel;

    public UnityEvent panelButtonEvent;

    public GameObject CurrentRunner;


    [System.Serializable]
    public class ItemCollectedEvent : UnityEngine.Events.UnityEvent<GameObject> { }
    public ItemCollectedEvent itemCollected;


    [System.Serializable]
    public class DrawEvent : UnityEngine.Events.UnityEvent<bool> { }
    public DrawEvent drawEvent;


    public bool isDrawingAllowed = false;
    public int maxLines = 50;

    public int level;

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
        CalcMazeSize();
        _instance.panelButtonEvent.AddListener(RevealPanelButton);
    }

    void RevealPanelButton()
    {
        //Debug.Log("Anim state info: " + panelOpenButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reveal"));
        if (!panelOpenButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reveal"))
        {
            panelOpenButton.GetComponent<Animator>().SetTrigger("Reveal");
        }

    }

    void CalcMazeSize()
    {
        int dif = LevelSelector.instance.levelName;
        xSize = LevelSelector.instance.base_size + dif;
        ySize = LevelSelector.instance.base_size + dif;
        MazeGenerator.gameObject.SetActive(true);
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

    public void SetCircuitPanel()
    {
        if (!GameManager.Instance.isCircuitPanelActive)
        {
            OpenCircuitPanel();
        }
        else
        {
            CloseCircuitPanel();
        }
    }

    void OpenCircuitPanel()
    {
        Maze.gameObject.SetActive(false);
        panelOpenButton.GetComponent<Image>().color = Color.black;
        MainCamera.gameObject.SetActive(false);
        UICamera.gameObject.SetActive(true);
        CircuitCanvas.gameObject.GetComponent<Canvas>().worldCamera = UICamera;
        GameManager.Instance.isCircuitPanelActive = true;
        circuitPanel.gameObject.SetActive(true);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));
        Debug.Log("Anim state info: " + panelOpenButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reveal"));
        if (panelOpenButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Reveal"))
        {
            Debug.Log("Returning to base state !!");
            panelOpenButton.GetComponent<Animator>().SetTrigger("Reveal");
        }
        panelOpenButton.transform.SetParent(circuitPanel.transform);
    }


    void CloseCircuitPanel()
    {
        Maze.gameObject.SetActive(true);
        panelOpenButton.GetComponent<Image>().color = Color.white;
        if (!panelOpenButton.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base"))
        {
            panelOpenButton.GetComponent<Animator>().SetTrigger("Reveal");
        }
        UICamera.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        CircuitCanvas.gameObject.GetComponent<Canvas>().worldCamera = MainCamera;
        GameManager.Instance.isCircuitPanelActive = false;
        panelOpenButton.transform.SetParent(CircuitCanvas.gameObject.transform);
        circuitPanel.gameObject.SetActive(false);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));
        CurrentRunner.gameObject.SetActive(false);
    }

    public void RunCurrent()
    {
        CurrentRunner.gameObject.SetActive(true);
    }
}
