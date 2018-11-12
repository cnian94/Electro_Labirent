﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores a pool of Line. Allows retrieval and initialisation of pooled line.
/// </summary>
public class LineFactory : MonoBehaviour
{

	public GameObject linePrefab;
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

	void Start ()
	{
		pooledLines = new Line[GameManager.Instance.maxLines];
		
		for (int i = 0; i < GameManager.Instance.maxLines; i++) {
			var line = Instantiate (linePrefab);
			line.SetActive (false);
			line.transform.SetParent (transform);
            line.AddComponent<BoxCollider2D>();
			pooledLines[i] = line.GetComponent<Line> ();
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
	public Line GetLine (Vector2 start, Vector2 end, float width, Color color, Transform parent)
	{
		var line = pooledLines [currentIndex];

        line.gameObject.transform.SetParent(parent);
        line.Initialise (start, end, width, color);
		line.gameObject.SetActive (true);


		currentIndex = (currentIndex + 1) % pooledLines.Length;

		return line;
	}

	/// <summary>
	/// Returns all active lines.
	/// </summary>
	/// <returns>The active lines.</returns>
	public List<Line> GetActive()
	{
		var activeLines = new List<Line> ();

		foreach (var line in pooledLines) {
			if (line.gameObject.activeSelf) {
				activeLines.Add (line);
			}
		}

		return activeLines;
	}

    Vector3 SetItemScale(GameObject item)
    {
        Vector3 newScale = item.transform.localScale;
        if (item.CompareTag("Battery"))
        {
            newScale.x = 0.3f;
            newScale.y = 5f;
        }

        if (item.CompareTag("Bulb"))
        {
            newScale.x = 0.2f;
            newScale.y = 3f;
        }

        if (item.CompareTag("Resistor"))
        {
            newScale.x = 0.4f;
            newScale.y = 9f;
        }
        return newScale;
    }

    void CreateBasicCircuit()
    {

        float start_x = -Screen.width / 4.0f;
        float start_y = (-gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2.0f) + Screen.width / 4.0f;
        //float start_y = UICamera.ScreenToWorldPoint(gameObject.transform.parent.transform.localPosition).y / 4;
        //Debug.Log("Start y: " + start_y);
        Vector2 point1 = new Vector2(start_x, start_y);
        Vector2 point2 = point1;
        //Debug.Log("PANEL Local POS: " + gameObject.transform.parent.transform.localPosition);
        //Debug.Log("PANEL World Local POS: " + UICamera.ScreenToWorldPoint(gameObject.transform.parent.transform.localPosition));
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                point2.x = point1.x + Screen.width / 2.0f;
                drawnLine = GetLine(point1, point2, 10.0f, Color.black, gameObject.transform.parent.transform);
                drawnLine.name = "WireTop";

                //Debug.Log("Top line Local position" + drawnLine.transform.localPosition);
                //Debug.Log("Top Line ScreenToWorld local position" + UICamera.ScreenToWorldPoint(drawnLine.transform.localPosition));

                GameObject resistor = Instantiate(GameManager.Instance.inventory[2], drawnLine.transform);
                Vector3 newPos = resistor.transform.localPosition;
                newPos.x -= 0.3f;
                resistor.transform.localPosition = newPos;
                resistor.transform.localScale = SetItemScale(resistor);
                resistor.name = GameManager.Instance.inventory[2].name;


                GameObject bulb = Instantiate(GameManager.Instance.inventory[0], drawnLine.transform);
                newPos = bulb.transform.localPosition;
                newPos.x += 0.6f;
                newPos.y += 5.0f;
                newPos.z = -1.0f;
                bulb.transform.localPosition = newPos;
                bulb.transform.localScale = SetItemScale(bulb);
                bulb.name = GameManager.Instance.inventory[0].name;
            }

            if (i == 1)
            {
                point1 = point2;
                //Debug.Log("POINT 1: " + point1);

                //point2.y = -gameObject.transform.parent.transform.localPosition.y / 4.0f - (gameObject.transform.parent.GetComponent<RectTransform>().rect.height / 2.0f);
                point2.y = point1.y - Screen.width / 2.0f;
                //Debug.Log("POINT 2: " + point2);
                drawnLine = GetLine(point1, point2, 10f, Color.black, gameObject.transform.parent.transform);
                drawnLine.name = "WireRight";
            }
            /*
            if (i == 2)
            {
                point1 = point2;
                point2.x = point2.x - 250f;
                drawnLine = GetLine(point1, point2, 10f, Color.black, gameObject.transform.parent.transform);
                drawnLine.name = "WireBottom";
                GameObject battery = Instantiate(GameManager.Instance.inventory[1], drawnLine.transform);
                Vector3 newPos = battery.transform.localPosition;
                newPos.z = -1;
                battery.transform.localScale = SetItemScale(battery);
                battery.transform.Rotate(180, 180, 0);
                battery.name = GameManager.Instance.inventory[1].name;
            }

            if (i == 3)
            {
                point1 = point2;
                point2.y = point2.y + 125f;
                drawnLine = GetLine(point1, point2, 10f, Color.black, gameObject.transform.parent.transform);
                drawnLine.name = "WireLeft";

            }*/

        }

    }

}