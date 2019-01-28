using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GuideManager : MonoBehaviour
{
    /*
    private static GuideManager guideManager;

    public static GuideManager Instance()
    {
        if (!guideManager)
        {
            guideManager = FindObjectOfType(typeof(GuideManager)) as GuideManager;
            if (!guideManager)
                Debug.LogError("There needs to be one active GuideManager script on a GameObject in your scene.");
        }

        return guideManager;
    }*/

    public static GuideManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("GuideManager").AddComponent<GuideManager>()); }
    }

    private static GuideManager _instance;

    public UnityEvent firstTimeEvent;

    [System.Serializable]
    public class RegisterEvent : UnityEngine.Events.UnityEvent<string> { }
    public RegisterEvent registerEvent;

    [System.Serializable]
    public class OpenMemberPanelEvent : UnityEngine.Events.UnityEvent<bool> { }
    public OpenMemberPanelEvent memberPanelEvent;

    public UnityEvent hideTipGuide;

    public PlayerModel CurrentPlayer;

    public Text displayText;
    public float displayTime;
    public float fadeTime;



    private IEnumerator fadeAlpha;

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

    void Start()
    {
        registerEvent.AddListener(CreateUser);

        Debug.Log("Saved Player:" + PlayerPrefs.GetString("player"));
        if (PlayerPrefs.GetString("player") == null || PlayerPrefs.GetString("player") == "")
        {
            Debug.Log("First Time User !!");
            firstTimeEvent.Invoke();
        }
    }

    void CreateUser(string name)
    {
        CurrentPlayer = new PlayerModel(name);

        for (int i = 1; i <= 10; i++)
        {
            LevelModel level = new LevelModel(i);
            CurrentPlayer.levels.Add(level);
        }
        Debug.Log("Player's Levels:" + CurrentPlayer.levels.Count);
        Debug.Log("New Player Model:" + CurrentPlayer.ToJSON(CurrentPlayer));
        memberPanelEvent.Invoke(true);
        
    }

    public void DisplayMessage(string message)
    {
        displayText.text = message;
        SetAlpha();
    }

    void SetAlpha()
    {
        if (fadeAlpha != null)
        {
            StopCoroutine(fadeAlpha);
        }
        fadeAlpha = FadeAlpha();
        StartCoroutine(fadeAlpha);
    }

    IEnumerator FadeAlpha()
    {
        Color resetColor = displayText.color;
        resetColor.a = 1;
        displayText.color = resetColor;

        yield return new WaitForSeconds(displayTime);

        while (displayText.color.a > 0)
        {
            Color displayColor = displayText.color;
            displayColor.a -= Time.deltaTime / fadeTime;
            displayText.color = displayColor;
            yield return null;
        }
        yield return null;
    }
}
