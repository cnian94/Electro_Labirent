using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

    public float lifeTime;

    // Use this for initialization
    void Start () {
        lifeTime = 10f;
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
