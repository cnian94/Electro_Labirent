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
            Vector3 newSize = new Vector3(2.1066f, 0.6f, 0);
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

    void CreateBasicCircuit()
    {

        //float start_x = -Screen.width / 4.0f;
        //float start_y = (-gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2.0f) + Screen.width / 4.0f;


        float start_x = Screen.width / 4.0f;
        float start_y = (-gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2.0f) - Screen.width / 4.0f;


        Vector2 point1 = new Vector2(start_x, start_y);
        Vector2 point2 = point1;
        Vector3 stationPos = Vector3.zero;

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                point2.x = point1.x - Screen.width / 2.0f;

                drawnLine = GetLine(point1, point2, 10.0f, Color.black, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                stationPos = station.gameObject.transform.localPosition;
                stationPos.x = 1f;
                stationPos.z = 1f;
                station.gameObject.transform.localPosition = stationPos;
                //Debug.Log("Station 1 Pos: " + station.gameObject.transform.position);
                //Debug.Log("Station 1 LocalPos: " + station.gameObject.transform.localPosition);

                station.name = i.ToString();
                drawnLine.name = "WireBottom";

                GameObject battery = Instantiate(GameManager.Instance.items[1], drawnLine.transform);
                Vector3 newPos = battery.transform.localPosition;
                battery.transform.localPosition = newPos;
                battery.transform.localScale = SetItemScale(battery);
                //battery.transform.rotation = Quaternion.AngleAxis(180, Vector3.down);
                //battery.transform.eulerAngles = drawnLine.transform.eulerAngles;
                battery.transform.localEulerAngles = drawnLine.transform.eulerAngles;
                battery.name = GameManager.Instance.items[1].name;
                GameManager.Instance.batteries.Add(battery.transform);

                //GameManager.Instance.stations.Add(station.gameObject.transform);
                //GameManager.Instance.stations.Insert(0, battery.transform.GetChild(0).gameObject.transform);
                endPoint = battery.transform.GetChild(1).gameObject.transform;

            }

            if (i == 1)
            {
                point1 = point2;
                point2.y = point1.y + Screen.width / 2.0f;
                drawnLine = GetLine(point1, point2, 10f, Color.black, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                stationPos.x = 1f;
                station.gameObject.transform.localPosition = stationPos;

                station.name = i.ToString();
                //GameManager.Instance.stations.Add(station.gameObject.transform);
                drawnLine.name = "WireLeft";
            }

            if (i == 2)
            {
                point1 = point2;
                point2.x = point2.x + Screen.width / 2.0f;

                drawnLine = GetLine(point1, point2, 10f, Color.black, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                station.gameObject.transform.localPosition = stationPos;
                station.name = i.ToString();
                //GameManager.Instance.stations.Add(station.gameObject.transform);
                drawnLine.name = "WireTop";

                GameObject resistor = Instantiate(GameManager.Instance.items[2], drawnLine.transform);
                Vector3 newPos = resistor.transform.localPosition;
                newPos.x -= 0.3f;
                resistor.transform.localPosition = newPos;
                resistor.transform.localScale = SetItemScale(resistor);
                resistor.name = GameManager.Instance.items[2].name;

                /*GameObject text = new GameObject();
                TextMesh t = text.AddComponent<TextMesh>();
                t.text = "10";
                t.fontSize = 30;

                text.transform.SetParent(resistor.transform);

                //t.transform.localEulerAngles += new Vector3(90, 0, 0);
                t.transform.localPosition = Vector3.zero;**/


                GameObject bulb = Instantiate(GameManager.Instance.items[0], drawnLine.transform);
                newPos = bulb.transform.localPosition;
                newPos.x += 0.6f;
                newPos.y += 4f;
                bulb.transform.localPosition = newPos;
                bulb.transform.localScale = SetItemScale(bulb);
                bulb.name = GameManager.Instance.items[0].name;
            }

            if (i == 3)
            {
                point1 = point2;
                point2.y = point1.y - Screen.width / 2.0f;
                drawnLine = GetLine(point1, point2, 10f, Color.black, gameObject.transform.parent.transform);
                GameManager.Instance.wires.Add(drawnLine.transform);
                GameObject station = Instantiate(this.station, drawnLine.transform);
                station.gameObject.transform.localPosition = stationPos;
                station.name = i.ToString();
                //GameManager.Instance.stations.Add(station.gameObject.transform);
                drawnLine.name = "WireRight";

            }

        }

        //GameManager.Instance.stations.Add(endPoint.transform);
        //Debug.Log("First last wire: " + GameManager.Instance.wires[3]);

    }

}
