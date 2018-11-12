using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitUIController : MonoBehaviour
{

    public Camera MainCamera;
    public Camera UICamera;

    public GameObject panelOpenButton;
    public GameObject circuitPanel;

    public GameObject craftPanel;

    public GameObject inventoryContent;
    public GameObject inventoryDesk;

    public GameObject CableDesk;
    public Color offColor = new Color(0.38f, 0.42f, 0.35f, 1.0f);

    public GameObject DrawManager;
    public LineFactory lineFactory;
    private Line drawnLine;


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.inventory.Count; i++)
        {
            GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
            GameObject item = Instantiate(GameManager.Instance.inventory[i], desk.transform);
            desk.name = GameManager.Instance.inventory[i].name;
            item.name = GameManager.Instance.inventory[i].name;
        }
        //CreateBasicCircuit();
        GameManager.Instance.drawEvent.AddListener(DrawEnabled);
    }


    public void DrawEnabled(bool val)
    {
        if (val)
        {
            CableDesk.GetComponent<Image>().color = Color.white;
            DrawManager.gameObject.SetActive(true);
        }
        else
        {
            CableDesk.GetComponent<Image>().color = offColor;
            DrawManager.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

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
        MainCamera.gameObject.SetActive(false);
        UICamera.gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().worldCamera = UICamera;
        GameManager.Instance.isCircuitPanelActive = true;
        panelOpenButton.transform.SetParent(circuitPanel.transform);
        circuitPanel.gameObject.SetActive(true);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));


    }


    void CloseCircuitPanel()
    {
        UICamera.gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(true);
        gameObject.GetComponent<Canvas>().worldCamera = MainCamera;
        GameManager.Instance.isCircuitPanelActive = false;
        panelOpenButton.transform.SetParent(gameObject.transform);
        circuitPanel.gameObject.SetActive(false);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));

    }


}
