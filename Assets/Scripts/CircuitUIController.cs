using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitUIController : MonoBehaviour
{

    public GameObject panelOpenButton;
    public GameObject circuitPanel;

    public GameObject inventoryContent;
    public GameObject inventoryDesk;



    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.inventory.Count; i++)
        {
            GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
            desk.name = i.ToString();
            GameObject item =  Instantiate(GameManager.Instance.inventory[i], desk.transform);
            item.name = i.ToString();
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
        GameManager.Instance.isCircuitPanelActive = true;
        circuitPanel.gameObject.SetActive(true);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));


    }


    void CloseCircuitPanel()
    {
        GameManager.Instance.isCircuitPanelActive = false;
        circuitPanel.gameObject.SetActive(false);
        panelOpenButton.transform.Rotate(new Vector3(0, 0, 180));

    }


}
