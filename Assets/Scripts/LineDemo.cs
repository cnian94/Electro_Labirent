using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDemo : MonoBehaviour 
{

    public Camera UICamera;
    
	public LineFactory lineFactory;

	private Vector2 start;
	private Line drawnLine;

    public GameObject craftPanel;

    private void Start()
    {
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }

    void Update ()
	{

		if (Input.GetMouseButtonDown (0) && GameManager.Instance.isDrawingAllowed && Input.mousePosition.y < Screen.height / 2) {
            //Debug.Log("POS: " + Input.mousePosition);
            var pos = UICamera.ScreenToWorldPoint (Input.mousePosition); // Start line drawing
			drawnLine = lineFactory.GetLine (pos, pos, 0.05f, Color.black, craftPanel.transform);
		} else if (Input.GetMouseButtonUp (0)) {
			drawnLine = null; // End line drawing
		}

		if (drawnLine != null) {
			drawnLine.end = UICamera.ScreenToWorldPoint (Input.mousePosition); // Update line end
		}
	}

	/// <summary>
	/// Get a list of active lines and deactivates them.
	/// </summary>
	public void Clear()
	{
		var activeLines = lineFactory.GetActive ();

		foreach (var line in activeLines) {
			line.gameObject.SetActive(false);
		}
	}
}
