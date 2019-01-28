using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores a pool of Line. Allows retrieval and initialisation of pooled line.
/// </summary>
public class LineFactory : MonoBehaviour
{

    public GameObject linePrefab;
    public GameObject station;
    public Camera UICamera;

    public GameObject DrawManager;
    private Line drawnLine;


    /// <summary>
    /// The number to pool. This is the maximum number of lines that can be retrieve from this factory.
    /// When a number of lines greater that this number is requested previous lines are overwritten.
    /// </summary>
    //public int maxLines = 50;

    private Line[] pooledLines;
    private int currentIndex = 0;

    private Transform endPoint;

    void Start()
    {
        pooledLines = new Line[GameManager.Instance.maxLines];

        for (int i = 0; i < GameManager.Instance.maxLines; i++)
        {
            var line = Instantiate(linePrefab);
            line.SetActive(false);
            line.transform.SetParent(transform);
            line.AddComponent<BoxCollider>();
            Vector3 newSize = new Vector3(2.1066f, 4f, 0);
            line.GetComponent<BoxCollider>().size = newSize;

            pooledLines[i] = line.GetComponent<Line>();
        }

        CreateBasicCircuit();
    }

    /// <summary>
    /// Gets an initialised and active line. The line is retrieved from the pool and set active.
    /// </summary>
    /// <returns>The active line.</returns>
    /// <param name="start">Start position in world space.</param>
    /// <param name="end">End position in world space.</param>
    /// <param name="width">Width of line.</param>
    /// <param name="color">Color of line.</param>
    public Line GetLine(Vector2 start, Vector2 end, float width, Color color, Transform parent)
    {
        var line = pooledLines[currentIndex];

        line.gameObject.transform.SetParent(parent);
        line.Initialise(start, end, width, color);
        line.gameObject.SetActive(true);


        currentIndex = (currentIndex + 1) % pooledLines.Length;

        return line;
    }

    /// <summary>
    /// Returns all active lines.
    /// </summary>
    /// <returns>The active lines.</returns>
    public List<Line> GetActive()
    {
        var activeLines = new List<Line>();

        foreach (var line in pooledLines)
        {
            if (line.gameObject.activeSelf)
            {
                activeLines.Add(line);
            }
        }

        return activeLines;
    }

    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            newScale.x = 0.4f;
            newScale.y = 10f;
            Vector3 tmpSize = item.GetComponent<BoxCollider>().size;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
            item.GetComponent<BoxCollider>().size = tmpSize;
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = 0.4f;
            newScale.y = 8f;
            float tmpRadius = item.GetComponent<SphereCollider>().radius;
            DestroyImmediate(item.GetComponent<SphereCollider>(), true);
            item.AddComponent<SphereCollider>();
            item.GetComponent<SphereCollider>().radius = tmpRadius;
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = 0.4f;
            newScale.y = 10f;
            Vector3 tmpSize = item.GetComponent<BoxCollider>().size;
            DestroyImmediate(item.GetComponent<BoxCollider>(), true);
            item.AddComponent<BoxCollider>();
            item.GetComponent<BoxCollider>().size = tmpSize;
        }


        return newScale;
    }

    bool HasBattery(Transform parentWire)
    {
        bool hasBattery = false;
        foreach (Transform child in parentWire)
        {
            if (child.CompareTag("Battery"))
            {
                hasBattery = true;
            }
        }
        return hasBattery;
    }

    bool HasBulb(Transform parentWire)
    {
        bool hasBulb = false;
        foreach (Transform child in parentWire)
        {
            if (child.CompareTag("Bulb"))
            {
                hasBulb = true;
            }
        }
        return hasBulb;
    }

    bool HasResistor(Transform parentWire)
    {
        bool hasResistor = false;
        foreach (Transform child in parentWire)
        {
            if (child.CompareTag("Resistor"))
            {
                hasResistor = true;
            }
        }
        return hasResistor;
    }

    Transform GetParentWireByIndex(int index)
    {
        return GameManager.Instance.wires[index];
    }

    void GenerateBulbs()
    {
        Transform parentWire = GetParentWireByIndex(2);

        if (!HasBulb(parentWire) && !HasBattery(parentWire) && !HasResistor(parentWire))
        {
            GameObject bulb = Instantiate(GameManager.Instance.items[0], parentWire.transform);
            Vector3 newPos = bulb.transform.localPosition;
            newPos.x += 0.6f;
            newPos.y += 4f;
            bulb.transform.localPosition = newPos;
            bulb.transform.localScale = SetItemScale(bulb);
            bulb.name = GameManager.Instance.items[0].name;
        }
        else
        {

        }
    }

    void GenerateBatteries()
    {
        Transform parentWire = GetParentWireByIndex(0);

        if (!HasBulb(parentWire) && !HasBattery(parentWire) && !HasResistor(parentWire))
        {

            GameObject battery = Instantiate(GameManager.Instance.items[1], parentWire.transform);
            Vector3 newPos = battery.transform.localPosition;
            battery.transform.localPosition = newPos;
            battery.transform.localScale = SetItemScale(battery);
            battery.transform.localEulerAngles = parentWire.transform.eulerAngles;
            battery.name = GameManager.Instance.items[1].name;
            GameManager.Instance.batteries.Add(battery.transform);
            endPoint = battery.transform.GetChild(1).gameObject.transform;
        }
        else
        {

        }
    }

    void GenerateResistors()
    {

        Transform parentWire = GetParentWireByIndex(3);
        Debug.Log("Resistor Parent Name:" + parentWire.name);

        if (!HasBulb(parentWire) && !HasBattery(parentWire) && !HasResistor(parentWire))
        {
            GameObject resistor = Instantiate(GameManager.Instance.items[2], parentWire.transform);
            Vector3 newPos = resistor.transform.localPosition;
            newPos.x += 0.3f;
            resistor.transform.localPosition = newPos;
            resistor.transform.localScale = SetItemScale(resistor);
            resistor.name = GameManager.Instance.items[2].name;
        }
        else
        {

        }
    }


    void GenerateInitialItems()
    {
        Debug.Log("Circuit items:" + LevelSelector.instance.currentLevel.inventoryItems.Count);


        foreach (KeyValuePair<int, int> item in LevelSelector.instance.currentLevel.circuitItems)
        {
            // do something with entry.Value or entry.Key
            Debug.Log("key:" + item.Key);
            Debug.Log("val:" + item.Value);

            // bulb
            if (item.Key == 0)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    GenerateBulbs();
                }
            }

            // battery
            if (item.Key == 1)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    GenerateBatteries();
                }
            }

            // resistor
            if (item.Key == 2)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    Debug.Log("Generating resistor #" + (i + 1));
                    GenerateResistors();
                }
            }
        }
    }

    void CreateBasicCircuit()
    {

        //float start_x = -Screen.width / 4.0f;
        //float start_y = (-gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2.0f) + Screen.width / 4.0f;


        float start_x = Screen.width / 4.0f;
        float start_y = (-gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2.0f) - Screen.width / 4.0f;


        Vector2 point1 = new Vector2(start_x, start_y);
        Vector2 point2 = point1;
        Vector3 stationPos = Vector3.zero;
        Color line_color = new Color(0.58f, 0.55f, 0.6f, 1f);

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                point2.x = point1.x - Screen.width / 2.0f;
                drawnLine = GetLine(point1, point2, 10.0f, line_color, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                stationPos = station.gameObject.transform.localPosition;
                stationPos.x = 1f;
                stationPos.z = 1f;
                station.gameObject.transform.localPosition = stationPos;

                station.name = i.ToString();
                drawnLine.name = "WireBottom";

                /*
                GameObject battery = Instantiate(GameManager.Instance.items[1], drawnLine.transform);
                Vector3 newPos = battery.transform.localPosition;
                battery.transform.localPosition = newPos;
                battery.transform.localScale = SetItemScale(battery);
                battery.transform.localEulerAngles = drawnLine.transform.eulerAngles;
                battery.name = GameManager.Instance.items[1].name;
                GameManager.Instance.batteries.Add(battery.transform);
                endPoint = battery.transform.GetChild(1).gameObject.transform;
                */

            }

            if (i == 1)
            {
                point1 = point2;
                point2.y = point1.y + Screen.width / 2.0f;
                drawnLine = GetLine(point1, point2, 10f, line_color, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                stationPos.x = 1f;
                station.gameObject.transform.localPosition = stationPos;

                station.name = i.ToString();
                drawnLine.name = "WireLeft";
            }

            if (i == 2)
            {
                point1 = point2;
                point2.x = point2.x + Screen.width / 2.0f;

                drawnLine = GetLine(point1, point2, 10f, line_color, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                station.gameObject.transform.localPosition = stationPos;
                station.name = i.ToString();
                drawnLine.name = "WireTop";


                /*
                GameObject resistor = Instantiate(GameManager.Instance.items[2], drawnLine.transform);
                Vector3 newPos = resistor.transform.localPosition;
                newPos.x -= 0.3f;
                resistor.transform.localPosition = newPos;
                resistor.transform.localScale = SetItemScale(resistor);
                resistor.name = GameManager.Instance.items[2].name;


                GameObject bulb = Instantiate(GameManager.Instance.items[0], drawnLine.transform);
                newPos = bulb.transform.localPosition;
                newPos.x += 0.6f;
                newPos.y += 4f;
                bulb.transform.localPosition = newPos;
                bulb.transform.localScale = SetItemScale(bulb);
                bulb.name = GameManager.Instance.items[0].name;
                */
            }

            if (i == 3)
            {
                point1 = point2;
                point2.y = point1.y - Screen.width / 2.0f;
                drawnLine = GetLine(point1, point2, 10f, line_color, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                station.gameObject.transform.localPosition = stationPos;
                station.name = i.ToString();
                //GameManager.Instance.stations.Add(station.gameObject.transform);
                drawnLine.name = "WireRight";

            }

        }

        GenerateInitialItems();

        //GameManager.Instance.stations.Add(endPoint.transform);
        //Debug.Log("First last wire: " + GameManager.Instance.wires[3]);

    }

}
