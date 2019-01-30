using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{

    private RectTransform rectComponent;
    private Image imageComp;


    void Start()
    {
        imageComp = gameObject.GetComponent<Image>();
        GameManager.Instance.SetBatteryBarFillAmount.AddListener(SetFillAmount);
        GameManager.Instance.ActivateBatteryLifeBarEvent.AddListener(ActivateCounter);
        //StartCoroutine(StartLifeTimeCounter());
    }

    void SetFillAmount()
    {
        imageComp.fillAmount = 1;
    }

    void ActivateCounter()
    {
        StartCoroutine(StartLifeTimeCounter());
    }

    IEnumerator StartLifeTimeCounter()
    {
        float totalLifeTime = CalculateTotalLifeTime();
        float x = 1.0f / totalLifeTime;
        Debug.Log("XXXXX katsayı:" + x);
        while (!GameManager.Instance.isBatteryLifeTimePaused)
        {
            if(totalLifeTime > 0.0f)
            {
                yield return new WaitForSeconds(1);
                imageComp.fillAmount = imageComp.fillAmount - x;
                totalLifeTime -= 1;
            }
            else
            {
                Debug.Log("Battery Died !!");
                GameManager.Instance.adjustLevelLightEvent.Invoke(false);
                yield return null;
            }
        }
    }

    void Update()
    {
        /*
        if (imageComp.fillAmount != 0.0f)
        {
            imageComp.fillAmount = imageComp.fillAmount - Time.deltaTime * speed;
        }

        else
        {
            imageComp.fillAmount = 1.0f;

        }*/
    }

    float CalculateTotalLifeTime()
    {
        float totalLifeTime = 0f;
        foreach (Transform child in GameManager.Instance.batteries)
        {
            totalLifeTime += child.gameObject.GetComponent<BatteryScript>().lifeTime;
        }
        Debug.Log("Total Life Time:" + totalLifeTime);
        return totalLifeTime;
    }
}
