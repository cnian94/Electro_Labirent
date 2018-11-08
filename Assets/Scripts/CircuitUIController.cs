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
        CreateBasicCircuit();
        GameManager.Instance.drawEvent.AddListener(DrawEnabled);
    }

    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            Debug.Log("SCALING BATTERY !!");
            newScale.x = 0.3f;
            newScale.y = 10;
        }

        if (item.CompareTag("Bulb"))
        {
            Debug.Log("SCALING BATTERY !!");
            newScale.x = 0.2f;
            newScale.y = 5;
        }

        if (item.CompareTag("Resistor"))
        {
            Debug.Log("SCALING BATTERY !!");
            newScale.x = 0.2f;
            newScale.y = 5;
        }
        return newScale;
    }

    void CreateBasicCircuit()
    {
        float start_x = -1.25f;
        float start_y = UICamera.ScreenToWorldPoint(craftPanel.transform.localPosition).y / 4;
        Vector2 point1 = new Vector2(start_x, start_y);
        Vector2 point2 = point1;
        Debug.Log("PANEL POS: " + UICamera.ScreenToWorldPoint(craftPanel.transform.position));
        Debug.Log("PANEL Local POS: " + UICamera.ScreenToWorldPoint(craftPanel.transform.localPosition));
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                point2.x = point1.x + 2.5f;
                drawnLine = lineFactory.GetLine(point1, point2, 0.05f, Color.black);
                drawnLine.name = "WireTop";


                GameObject resistor = Instantiate(GameManager.Instance.inventory[2], drawnLine.transform);
                Vector3 newPos = resistor.transform.localPosition;
                newPos.x -= 0.5f;
                resistor.transform.localPosition = newPos;
                resistor.transform.localScale = SetItemScale(resistor);
                resistor.name = GameManager.Instance.inventory[2].name;


                GameObject bulb = Instantiate(GameManager.Instance.inventory[0], drawnLine.transform);
                newPos = bulb.transform.localPosition;
                newPos.x += 0.5f;
                newPos.y += 8;
                bulb.transform.localPosition = newPos;
                bulb.transform.localScale = SetItemScale(bulb);
                bulb.name = GameManager.Instance.inventory[0].name;
            }

            if (i == 1)
            {
                point1 = point2;
                point2.y = point2.y - 1.25f;
                drawnLine = lineFactory.GetLine(point1, point2, 0.05f, Color.black);
                drawnLine.name = "WireRight";
            }

            if (i == 2)
            {
                point1 = point2;
                point2.x = point2.x - 2.5f;
                drawnLine = lineFactory.GetLine(point1, point2, 0.05f, Color.black);
                drawnLine.name = "WireBottom";
                GameObject item = Instantiate(GameManager.Instance.inventory[1], drawnLine.transform);
                item.transform.localScale = SetItemScale(item);
                item.transform.Rotate(180, 180, 0);
                item.name = GameManager.Instance.inventory[1].name;
            }

            if (i == 3)
            {
                point1 = point2;
                point2.y = point2.y + 1.25f;
                drawnLine = lineFactory.GetLine(point1, point2, 0.05f, Color.black);
                drawnLine.name = "WireLeft";

            }

        }
        Debug.Log("Screen POS: " + craftPanel.transform.position);

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
