using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitUIController : MonoBehaviour
{

    //public Camera MainCamera;
    //public Camera UICamera;

    //public GameObject panelOpenButton;
    //public GameObject circuitPanel;

    //public GameObject craftPanel;

    public GameObject inventoryContent;
    public GameObject inventoryDesk;

    public GameObject scrollbar;


    public GameObject CableDesk;
    public GameObject ParallelDesk;
    public GameObject SeriesDesk;
    public Color offColor = new Color(0.38f, 0.42f, 0.35f, 1.0f);

    //public GameObject DrawManager;
    public LineFactory lineFactory;
    private Line drawnLine;

    
    public GameObject Parallel;

    //public GameObject CurrentRenderer;
    //public GameObject CurrentRunner;


    // Use this for initialization
    void Start()
    {
        //Debug.Log("inventory length: " + GameManager.Instance.inventory.Count);
        /*for (int i = 0; i < GameManager.Instance.inventory.Count; i++)
        {
            GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
            GameObject item = Instantiate(GameManager.Instance.inventory[i], desk.transform);
            desk.name = GameManager.Instance.inventory[i].name;
            item.name = GameManager.Instance.inventory[i].name;
        }*/
        //CreateBasicCircuit();
        GameManager.Instance.drawEvent.AddListener(DrawEnabled);
        GameManager.Instance.itemCollected.AddListener(AddItem);
    }

    void AddItem(GameObject item)
    {
        GameObject desk = Instantiate(inventoryDesk, inventoryContent.transform);
        item.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        desk.name = item.name;
        item.transform.SetParent(desk.transform);
        Vector3 newPos = Vector3.zero;
        item.transform.localPosition = newPos;
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

    // Update is called once per frame
    void Update()
    {

    }

}
