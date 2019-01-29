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


    public GameObject CableDesk;
    public GameObject ParallelDesk;
    public GameObject SeriesDesk;
    public Color offColor = new Color(0.38f, 0.42f, 0.35f, 1.0f);
    public LineFactory lineFactory;
    private Line drawnLine;


    public GameObject Parallel;



    // Use this for initialization
    void Start()
    {
        animator = panelOpenButton.GetComponent<Animator>();
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
        GameManager.Instance.drawEvent.AddListener(DrawEnabled);
        GameManager.Instance.itemCollected.AddListener(AddItem);
    }

    void LevelFinish()
    {
        Debug.Log("Level Finish From Circuit UI");
        GameManager.Instance.Maze.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void RunCurrent()
    {
        CurrentRunner.gameObject.SetActive(true);
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
            OpenCircuitPanel();
        }
        else
        {
            CloseCircuitPanel();
        }
    }

    void OpenCircuitPanel()
    {
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
        circuitPanel.gameObject.SetActive(false);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));
        CurrentRunner.gameObject.SetActive(false);
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
            newScale.y = 100f;
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


    public void DrawEnabled(bool val)
    {
        if (val)
        {
            CableDesk.GetComponent<Image>().color = Color.white;
            CableDesk.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            ParallelDesk.gameObject.SetActive(true);
            SeriesDesk.gameObject.SetActive(true);
            //DrawManager.gameObject.SetActive(true);
        }
        else
        {
            CableDesk.GetComponent<Image>().color = offColor;
            //DrawManager.gameObject.SetActive(false);
        }

    }

    public void AddParallelCable()
    {

        foreach (Transform wire in GameManager.Instance.wires)
        {
            GameObject newParallel = Instantiate(Parallel, wire.transform);
            //parallel.GetComponent<SpriteRenderer>().color = new Color(1, 0.43f, 0, 0.153f);

            /*if (wire.name.Equals("WireBottom"))
            {
                Vector3 newLocalRotation = newParallel.transform.localRotation.eulerAngles;
                newLocalRotation.y = 180.0f;
                newParallel.transform.Rotate(newLocalRotation);
            }

            if (wire.name.Equals("WireLeft"))
            {
                Debug.Log("Rotating WireLeftParallel !!");
                //Vector3 newLocalRotation = newParallel.transform.localRotation.eulerAngles;
                //newLocalRotation.x = 180.0f;
                //newLocalRotation.y = 180.0f;
                //newLocalRotation.z = 180.0f;
                //newParallel.transform.localRotation = Quaternion.Euler(180.0f, 180.0f, 270.0f);
                //newParallel.transform.Ro
                //burdayımmm
            }*/

            newParallel.name = wire.name + "Parallel";
            GameManager.Instance.parallels.Add(newParallel);
        }
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
