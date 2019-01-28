using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour
{
    public GameObject PlayButton;

    public float delay = 0.12f;
    //public string fullText;
    //public string[] messages;

    private string currentText = "";


    // Use this for initialization
    void Start()
    {

        PlayButton = GameObject.FindGameObjectWithTag("btnPlay");
    }

    public IEnumerator ShowInGameGuideText(Dictionary<string, List<string>> levelMessages, string key)
    {
        Debug.Log("Key:" + key);
        Debug.Log("Level Messages Count:" + levelMessages.Count);
        List<string> messages = levelMessages[key];
        foreach (string msg in messages)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                currentText = msg.Substring(0, i + 1);
                this.GetComponent<Text>().text = currentText;

                if (i < msg.Length - 1)
                {
                    yield return new WaitForSeconds(delay);
                }

                if (i == msg.Length - 1)
                {
                    Debug.Log("Waiting for 1 seconds !!");
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        if (!key.Equals("tip"))
        {
            GameManager.Instance.panelButtonEvent.Invoke();
        }
        else
        {
            GuideManager.Instance.hideTipGuide.Invoke();
        }

        yield return null;
    }


    public IEnumerator ShowLevelSelectorGuideText(List<string> messages)
    {
        foreach (string msg in messages)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                currentText = msg.Substring(0, i + 1);
                this.GetComponent<Text>().text = currentText;

                if (i < msg.Length - 1)
                {
                    yield return new WaitForSeconds(delay);
                }

                if (i == msg.Length - 1)
                {
                    Debug.Log("Waiting for 1 seconds !!");
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }

    public IEnumerator ShowMenuGuideText(List<string> messages)
    {

        foreach (string msg in messages)
        {
            for (int i = 0; i < msg.Length; i++)
            {
                currentText = msg.Substring(0, i + 1);
                this.GetComponent<Text>().text = currentText;

                if (i < msg.Length - 1)
                {
                    yield return new WaitForSeconds(delay);
                }

                if (i == msg.Length - 1)
                {
                    Debug.Log("Waiting for 1 seconds !!");
                    yield return new WaitForSeconds(1f);
                }
            }
        }
        PlayButton.transform.GetChild(0).gameObject.SetActive(true);

    }

}
