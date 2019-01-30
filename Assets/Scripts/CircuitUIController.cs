using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitUIController : MonoBehaviour
{
    public Camera MainCamera;
    public Camera UICamera;

    public GameObject inGameGuide;

    //public GameObject Maze;
    public GameObject CurrentRunner;
    public GameObject ParallelRunner;

    public GameObject panelOpenButton;
    Animator animator;
    public GameObject circuitPanel;
    public GameObject craftPanel;


    public GameObject inventoryContent;
    public GameObject inventoryDesk;

    public GameObject scrollbar;

    public GameObject circuitDesigner;



    public GameObject CableDesk;
    public GameObject ParallelDesk;
    public GameObject SeriesDesk;
    public Color offColor = new Color(0.38f, 0.42f, 0.35f, 1.0f);
    public LineFactory lineFactory;
    private Line drawnLine;


    public GameObject Parallel;

    public GameObject joyStick;

    public GameObject runCurrentButton;

    public GameObject batteryBar;



    // Use this for initialization
    void Start()
    {
        animator = panelOpenButton.GetComponent<Animator>();
        GameManager.Instance.setPanelButtonEvent.AddListener(SetPanelButton);
        GameManager.Instance.panelButtonEvent.AddListener(RevealPanelButton);
        GameManager.Instance.parallelRunner.AddListener(AddParallelRunner);
        GameManager.Instance.LevelFinishEvent.AddListener(LevelFinish);
        for (int i = 0; i < GameManager.Instance.inventory.Count; i++)
        {
            GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
            GameObject item = Instantiate(GameManager.Instance.inventory[i], desk.transform);
            desk.name = GameManager.Instance.inventory[i].name;
            item.name = GameManager.Instance.inventory[i].name;
        }
        //CreateBasicCircuit();
        //GameManager.Instance.drawEvent.AddListener(DrawEnabled);
        GameManager.Instance.itemCollected.AddListener(AddItem);
    }

    public void AddParallelCable()
    {

        if(LevelSelector.instance.currentLevel.number == 8)
        {
            Transform existing_battery = GameManager.Instance.batteries[0];
            GameObject newParallel = Instantiate(Parallel, existing_battery.parent.transform);
            newParallel.name = existing_battery.parent.name + "Parallel";
            GameManager.Instance.parallels.Add(newParallel);
        }

        if (LevelSelector.instance.currentLevel.number == 9)
        {
            Transform existing_bulb = GameManager.Instance.bulbs[0];
            GameObject newParallel = Instantiate(Parallel, existing_bulb.parent.transform);
            newParallel.name = existing_bulb.parent.name + "Parallel";
            GameManager.Instance.parallels.Add(newParallel);
        }

        if (LevelSelector.instance.currentLevel.number == 10)
        {
            Transform existing_bulb = GameManager.Instance.bulbs[0];
            GameObject newParallel = Instantiate(Parallel, existing_bulb.parent.transform);
            newParallel.name = existing_bulb.parent.name + "Parallel";
            GameManager.Instance.parallels.Add(newParallel);
        }
    }


    public void Draw()
    {
        if (GameManager.Instance.isDrawingAllowed)
        {
            GameManager.Instance.isDrawingAllowed = false;
        }
        else
        {
            GameManager.Instance.isDrawingAllowed = true;
        }

    }

    void LevelFinish()
    {
        Debug.Log("Level Finish From Circuit UI");
        GameManager.Instance.Maze.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CircuitMakerButton()
    {
        if (circuitDesigner.GetComponent<Image>().color != Color.white)
        {
            circuitDesigner.GetComponent<Image>().color = Color.white;
            GameManager.Instance.dragManagerEvent.Invoke(true);
            runCurrentButton.SetActive(false);
        }
        else
        {
            circuitDesigner.GetComponent<Image>().color = Color.grey;
            GameManager.Instance.dragManagerEvent.Invoke(false);
            runCurrentButton.SetActive(true);
        }
    }

    public void RunCurrent()
    {
        if (GameManager.Instance.IsCircuitApproved())
        {
            CurrentRunner.gameObject.SetActive(true);
        }
        else
        {
            GameManager.Instance.ShowLevelErrorMessageEvent.Invoke();
        }
    }

    void SetPanelButton(bool val)
    {
        panelOpenButton.SetActive(val);
    }


    void RevealPanelButton()
    {
        if (!panelOpenButton.activeSelf)
        {
            panelOpenButton.SetActive(true);
        }

        animator.SetBool("reveal", true);

    }


    public void SetCircuitPanel()
    {
        inGameGuide = GameObject.FindGameObjectWithTag("GuidePanel");
        if (!GameManager.Instance.isCircuitPanelActive)
        {
            joyStick.SetActive(false);
            OpenCircuitPanel();
        }
        else
        {
            joyStick.SetActive(true);
            CloseCircuitPanel();
        }
    }

    void OpenCircuitPanel()
    {
        GameManager.Instance.isBatteryLifeTimePaused = true;
        GameManager.Instance.setBatteryLifeTimePausedEvent.Invoke(true);
        GameManager.Instance.Maze.gameObject.SetActive(false);
        inGameGuide.GetComponent<Animator>().SetBool("showGuide", false);
        animator.SetBool("reveal", false);
        MainCamera.gameObject.SetActive(false);
        UICamera.gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().worldCamera = UICamera;

        GameManager.Instance.isCircuitPanelActive = true;
        circuitPanel.gameObject.SetActive(true);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));
        panelOpenButton.transform.SetParent(circuitPanel.transform);
        GameManager.Instance.setPanelButtonEvent.Invoke(false);
        GameManager.Instance.ShowLevelTipMessageEvent.Invoke();
    }


    void CloseCircuitPanel()
    {
        GameManager.Instance.Maze.gameObject.SetActive(true);
        panelOpenButton.GetComponent<Image>().color = Color.white;
        animator.SetBool("reveal", false);
        UICamera.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().worldCamera = MainCamera;
        GameManager.Instance.isCircuitPanelActive = false;
        panelOpenButton.transform.SetParent(gameObject.transform);
        //circuitPanel.gameObject.SetActive(false);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));
        GameManager.Instance.setPanelButtonEvent.Invoke(true);
        CurrentRunner.gameObject.SetActive(false);
        if(GameManager.Instance.batteries.Count > 0)
        {
            GameManager.Instance.isBatteryLifeTimePaused = false;
            GameManager.Instance.setBatteryLifeTimePausedEvent.Invoke(false);
            //GameManager.Instance.setBatteryBarFillAmount.Invoke(GameManager.Instance.CalculateTotalLifeTime());
            Image imageComp = batteryBar.GetComponent<Image>();
            //GameManager.Instance.CalculateTotalLifeTime();
            //imageComp.fillAmount = GameManager.Instance.totalLifeTime;
            GameManager.Instance.activateBatteryLifeBarEvent.Invoke();
        }

    }

    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            newScale.x = 75f;
            newScale.y = 100f;
            Vector3 tmpSize = item.GetComponent<BoxCollider>().size;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
            item.GetComponent<BoxCollider>().size = tmpSize;
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = 75f;
            newScale.y = 80f;
            float tmpRadius = item.GetComponent<SphereCollider>().radius;
            DestroyImmediate(item.GetComponent<SphereCollider>(), true);
            item.AddComponent<SphereCollider>();
            item.GetComponent<SphereCollider>().radius = tmpRadius;
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = 75f;
            newScale.y = 100f;
            Vector3 tmpSize = item.GetComponent<BoxCollider>().size;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
            item.GetComponent<BoxCollider>().size = tmpSize;
        }


        return newScale;
    }

    void AddItem(GameObject item)
    {
        GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
        item.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        desk.name = item.name;
        item.transform.SetParent(desk.transform);
        Vector3 newPos = Vector3.zero;
        item.transform.localPosition = newPos;
        item.transform.localScale = SetItemScale(item);
        scrollbar.GetComponent<Scrollbar>().value = 1.0f;
    }


    void AddParallelRunner(Transform wire, GameObject currentWire)
    {
        GameObject newParallelRunner = Instantiate(ParallelRunner, craftPanel.transform);
        newParallelRunner.gameObject.GetComponent<TrailRenderer>().widthMultiplier = 0.5f;
        GameManager.Instance.runParallelRunner.Invoke(wire, newParallelRunner.gameObject.GetInstanceID());
        GameManager.Instance.parallelRunners.Add(currentWire, newParallelRunner);
        Debug.Log("CurrentWire: " + currentWire.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
