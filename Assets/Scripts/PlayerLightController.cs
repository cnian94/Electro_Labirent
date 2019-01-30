using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    LevelModel currentLevel;
    Light light;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Player light start !!");
        GameManager.Instance.adjustLevelLightEvent.AddListener(AdjustLevelLight);
        GameManager.Instance.setLevelLightEvent.AddListener(SetLevelLight);
        currentLevel = LevelSelector.instance.currentLevel;
        light = gameObject.GetComponent<Light>();
        GameManager.Instance.changeLightRangeEvent.AddListener(changeLightRange);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void changeLightRange(float number)
    {
        Debug.Log("Before Range: " + gameObject.GetComponent<Light>().range);
        Debug.Log("Number: " + number);
        gameObject.GetComponent<Light>().range = number;

    }

    void SetLevelLight(bool val)
    {

        if (val)
        {
            light.enabled = true;
        }
        else
        {
            light.enabled = false;
        }
    }



    void AdjustLevelLight(bool circuitApproved)
    {
        if (currentLevel.number == 1)
        {
            if (circuitApproved)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
        if (currentLevel.number == 2)
        {
            if (circuitApproved)
            {
                light.enabled = true;
            }
        }

        if (currentLevel.number == 3)
        {
            Debug.Log("Turning on the light !!");
            light.enabled = true;
        }

        if (currentLevel.number == 4)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }
        if (currentLevel.number == 5)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }
        if (currentLevel.number == 6)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }
        if (currentLevel.number == 7)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }
        if (currentLevel.number == 8)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }
        if (currentLevel.number == 9)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }
        if (currentLevel.number == 10)
        {
            //Debug.Log("Turning on the light !!");
            light.enabled = true;
        }

    }
}
