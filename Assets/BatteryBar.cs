using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{

    private Image imageComp;
    public bool isBatteryPlugged;


    void Start()
    {
        imageComp = gameObject.GetComponent<Image>();

        if (LevelSelector.instance.currentLevel.number == 1)
        {
            isBatteryPlugged = false;
        }
        else
        {
            isBatteryPlugged = true;
        }

        GameManager.Instance.activateBatteryLifeBarEvent.AddListener(RunCounter);
    }

    void RunCounter()
    {
        StartCoroutine(StartLifeTimeCounter());
    }

    void SetFillAmount()
    {
        if (GameManager.Instance.parallels.Count == 0)
        {
            imageComp.fillAmount = GameManager.Instance.totalLifeTime / (GameManager.Instance.batteries.Count * 10);
        }

    }

    IEnumerator StartLifeTimeCounter()
    {
        GameManager.Instance.isBatteryLifeTimePaused = false;
        GameManager.Instance.CalculateTotalLifeTime();
        SetFillAmount();
        float totalLifeTime = GameManager.Instance.totalLifeTime;
        float x = 1.0f / totalLifeTime;

        Debug.Log("XXXXX katsayı:" + x);
        Debug.Log("is Paused:" + GameManager.Instance.isBatteryLifeTimePaused);
        StartCoroutine(GameManager.Instance.ReduceBatteriesLife());
        while (!GameManager.Instance.isBatteryLifeTimePaused)
        {
            if (totalLifeTime > 0.0f)
            {
                imageComp.fillAmount = imageComp.fillAmount - x;
                totalLifeTime -= 1;
                yield return new WaitForSeconds(1);
            }
            else
            {
                Debug.Log("Battery Died !!");
                GameManager.Instance.setLevelLightEvent.Invoke(false);
                GameManager.Instance.isBatteryLifeTimePaused = true;
                GameManager.Instance.setBatteryLifeTimePausedEvent.Invoke(true);
                yield return null;
            }
        }
    }
}
