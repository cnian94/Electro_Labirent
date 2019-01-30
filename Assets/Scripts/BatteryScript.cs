using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryScript : MonoBehaviour {

    //public float lifeTime;

    // Use this for initialization
    void Start () {
        //lifeTime = 10f;
        //GameManager.Instance.setBatteryLifeTimePausedEvent.AddListener(SetIsBatteryLifeTimePaused);
        //StartCoroutine(StartLifeTimeCounter());
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

    void ActivateCounter(float totalLifeTime)
    {
        //StartCoroutine(StartLifeTimeCounter());
    }

    /*
    IEnumerator StartLifeTimeCounter()
    {
        float x = 1.0f / lifeTime;
        Debug.Log("katsayı from battery:" + x);
        while (!isBatteryLifeTimePaused)
        {
            if (lifeTime > 0.0f)
            {
                yield return new WaitForSeconds(1);
                lifeTime -= 1;
                //GameManager.Instance.batteries.Find(battery => battery.transform == gameObject.transform).gameObject.GetComponent<BatteryScript>().lifeTime = lifeTime;
            }
        }
    }*/
}
