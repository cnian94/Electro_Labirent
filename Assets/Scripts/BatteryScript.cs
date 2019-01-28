using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

    //public GameObject panelOpenButton;

    // Use this for initialization
    void Start () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colliding with: " + col.gameObject.name);
            GameManager.Instance.inventory.Add(gameObject);
            GameManager.Instance.itemCollected.Invoke(gameObject);
            //panelOpenButton.GetComponent<Animator>().SetTrigger("Reveal");
            GameManager.Instance.panelButtonEvent.Invoke();
        }

    }
}
