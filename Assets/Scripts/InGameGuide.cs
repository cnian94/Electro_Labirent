using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameGuide : MonoBehaviour
{
    public GameObject QuestionMark;
    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;

    public GameObject FadeOutPanel;
    public GameObject LevelFinishPanel;
    public GameObject LevelFinishButton;
    public Sprite filledStar;
    public GameObject MenuButton;
    public GameObject NextLevelButton;

    GameObject guidePanel;

    public GuideManager guideManager;
    private GuideModal guideModal;

    void Awake()
    {
        guideModal = GuideModal.Instance;
        guideManager = GuideManager.Instance;
    }

    void Start()
    {
        GameManager.Instance.ShowLevelBeginMessageEvent.AddListener(ShowLevelBeginGuide);
        GameManager.Instance.ShowLevelTipMessageEvent.AddListener(ShowLevelTipGuide);
        GameManager.Instance.ShowLevelErrorMessageEvent.AddListener(ShowLevelErrorGuide);
        GameManager.Instance.LevelFinishEvent.AddListener(Finishlevel);
        GameManager.Instance.ShowLevelFinishButtonsEvent.AddListener(ShowLevelFinishButtons);
        guideManager.hideTipGuide.AddListener(HideTipGuide);
    }

    public void ShowLevelBeginGuide()
    {
        GuidePanelDetails guidePanelDetails = LevelSelector.instance.currentLevel.details;
        guidePanel = Instantiate(guideModal.inGameGuideObject, gameObject.transform);
        guidePanel.GetComponent<Animator>().SetBool("showGuide", true);
        guideModal.NewChoice(guidePanelDetails, "begin");
        //StartCoroutine(DisableQuestionMark());
    }

    IEnumerator DisableQuestionMark()
    {
        yield return new WaitForSeconds(1.5f);
        QuestionMark.SetActive(false);
    }

    public void ShowLevelTipGuide()
    {
        GuidePanelDetails guidePanelDetails = LevelSelector.instance.currentLevel.details;
        guidePanel.GetComponent<Animator>().SetBool("showGuide", true);
        guideModal.NewChoice(guidePanelDetails, "tip");
    }

    public void ShowLevelFinishGuide()
    {
        GuidePanelDetails guidePanelDetails = LevelSelector.instance.currentLevel.details;
        guidePanel.GetComponent<Animator>().SetBool("showGuide", true);
        guideModal.NewChoice(guidePanelDetails, "finish");
    }

    public void ShowLevelErrorGuide()
    {
        GuidePanelDetails guidePanelDetails = LevelSelector.instance.currentLevel.details;
        guidePanel.GetComponent<Animator>().SetBool("showGuide", true);
        guideModal.NewChoice(guidePanelDetails, "error");
    }

    void HideTipGuide()
    {
        guidePanel.GetComponent<Animator>().SetBool("showGuide", false);
    }

    int CalculateRecievedStars()
    {
        int recievedStars = 0;
        float passedTime = 3f;
        float[] times = LevelSelector.instance.currentLevel.starTimes;
        if (passedTime <= times[0])
        {
            recievedStars = 2;
        }
        if (passedTime > times[0] && passedTime <= times[1])
        {
            recievedStars = 1;
        }


        return recievedStars;
    }

    void SetPlayerModel()
    {

    }

    void Finishlevel()
    {
        GameManager.Instance.inventory.Clear();
        GameManager.Instance.wires.Clear();
        GameManager.Instance.batteries.Clear();
        GameManager.Instance.resistors.Clear();
        GameManager.Instance.bulbs.Clear();
        GameManager.Instance.parallels.Clear();
        StartCoroutine(FinishThelevel());

    }

    IEnumerator FinishThelevel()
    {
        int recievedStars = CalculateRecievedStars() + 1;
        Animator panelAnimator = FadeOutPanel.GetComponent<Animator>();
        Animator buttonAnimator = LevelFinishButton.GetComponent<Animator>();
        panelAnimator.SetBool("fadeOut", true);
        yield return new WaitForSeconds(1f);
        panelAnimator.SetBool("fadeOut", false);
        panelAnimator.SetBool("fadeIn", true);
        yield return new WaitForSeconds(1f);
        panelAnimator.SetBool("fadeIn", false);
        LevelFinishPanel.SetActive(true);
        LevelFinishButton.transform.GetChild(0).GetComponent<Text>().text = LevelSelector.instance.currentLevel.number.ToString();
        buttonAnimator.SetBool("throwFinish", true);
        yield return new WaitForSeconds(1f);
        Transform StarHolder = LevelFinishButton.transform.GetChild(1);

        for (int i = 0; i < recievedStars; i++)
        {
            StarHolder.GetChild(i).gameObject.GetComponent<Image>().sprite = filledStar;
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("Current Level Number:" + LevelSelector.instance.currentLevel.number);
        GuideManager.Instance.CurrentPlayer.levels[LevelSelector.instance.currentLevel.number - 1].stars = recievedStars;
        GuideManager.Instance.CurrentPlayer.levels[LevelSelector.instance.currentLevel.number].isLocked = false;
        PlayerPrefs.SetString("player", GuideManager.Instance.CurrentPlayer.ToJSON(GuideManager.Instance.CurrentPlayer));

        ShowLevelFinishGuide();
    }

    void ShowLevelFinishButtons()
    {
        MenuButton.SetActive(true);
        NextLevelButton.SetActive(true);
    }

    public void OpenMenu()
    {
        //GuideManager.Instance.memberPanelEvent.Invoke();
        SceneManager.LoadScene(0);
    }

    public void OpenNextLevel()
    {
        LevelSelector.instance.openLevelEvent.Invoke(LevelSelector.instance.currentLevel.number + 1);
    }

}
