using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


public class EventButtonDetails
{
    public string buttonTitle;
    public Sprite buttonBackground;  // Not implemented
    public UnityAction action;
}

public class EventSliderDetails
{

}

[Serializable]
public class GuidePanelDetails
{
    public int sceneIndex;
    public List<string> messages;
    public Dictionary<string, List<string>> levelMessages;
    //public Sprite panelBackgroundImage; // Not implemented
    //public EventButtonDetails button1Details;
    //public EventButtonDetails button2Details;
    //public EventButtonDetails button3Details;
    //public EventButtonDetails button4Details;
    //public EventSliderDetails sliderDetails;

    public GuidePanelDetails()
    {
        this.messages = new List<string>();
        this.levelMessages = new Dictionary<string, List<string>>();
    }

    public string ToJSON(GuidePanelDetails details)
    {
        return JsonConvert.SerializeObject(details);
    }

    public static GuidePanelDetails CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<GuidePanelDetails>(jsonString);
    }
}

public class GuideModal : MonoBehaviour
{

    public static GuideModal Instance
    {
        get { return _instance ?? (_instance = new GameObject("GuideModal").AddComponent<GuideModal>()); }
    }

    private static GuideModal _instance;


    public GameObject guidePanelObject;
    public GameObject inGameGuideObject;

    public Text message;
    public Image iconImage;
    public Button button1;
    public Button button2;
    public Button button3;

    public Text button1Text;
    public Text button2Text;
    public Text button3Text;

    TypeWriterEffect type_writer;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }


    /*
    private static GuideModal guideModal;

    public static GuideModal Instance()
    {
        if (!guideModal)
        {
            guideModal = FindObjectOfType(typeof(GuideModal)) as GuideModal;
            if (!guideModal)
                Debug.LogError("There needs to be one active GuidePanel script on a GameObject in your scene.");
        }

        return guideModal;
    }*/


    //  //  Announcement with Image:  A string, a Sprite and Cancel event;
    //  public void Choice (string question, UnityAction cancelEvent, Sprite iconImage = null) {
    //      modalPanelObject.SetActive (true);
    //      
    //      button3.onClick.RemoveAllListeners();
    //      button3.onClick.AddListener (cancelEvent);
    //      button3.onClick.AddListener (ClosePanel);
    //      
    //      this.question.text = question;
    //      if (iconImage)
    //          this.iconImage.sprite = iconImage;
    //      
    //      if (iconImage)
    //          this.iconImage.gameObject.SetActive(true);
    //      else
    //          this.iconImage.gameObject.SetActive(false);
    //      button1.gameObject.SetActive(false);
    //      button2.gameObject.SetActive(false);
    //      button3.gameObject.SetActive(true);
    //  }

    public void NewChoice(GuidePanelDetails details, string key="")
    {

        foreach (string msg in details.messages)
        {
            Debug.Log("msg:" + msg);

        }

        message = GameObject.FindGameObjectWithTag("GuideMessage").GetComponent<Text>();
        type_writer = message.gameObject.GetComponent<TypeWriterEffect>();


        if (details.sceneIndex == 0)
        {
            StartCoroutine(type_writer.ShowMenuGuideText(details.messages));
        }

        if (details.sceneIndex == 1)
        {
            StartCoroutine(type_writer.ShowLevelSelectorGuideText(details.messages));
        }

        if (details.sceneIndex == 2)
        {
            StartCoroutine(type_writer.ShowInGameGuideText(details.levelMessages, key));
        }


        /*
        if (details.iconImage)
        {
            iconImage.sprite = details.iconImage;
            iconImage.gameObject.SetActive(true);
        }


        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        */

        /*
        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(details.button1Details.action);
        button1.onClick.AddListener(ClosePanel);
        button1Text.text = details.button1Details.buttonTitle;
        button1.gameObject.SetActive(true);

        if (details.button2Details != null)
        {
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(details.button2Details.action);
            button2.onClick.AddListener(ClosePanel);
            button2Text.text = details.button2Details.buttonTitle;
            button2.gameObject.SetActive(true);
        }

        if (details.button3Details != null)
        {
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(details.button3Details.action);
            button3.onClick.AddListener(ClosePanel);
            button3Text.text = details.button3Details.buttonTitle;
            button3.gameObject.SetActive(true);
        }*/
    }

    void ClosePanel()
    {
        guidePanelObject.SetActive(false);
    }
}
