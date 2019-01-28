using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGuide : MonoBehaviour
{
    public GameObject QuestionMark;
    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;

    public GameObject FadeOutPanel;

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
        GameManager.Instance.LevelFinishEvent.AddListener(Finishlevel);
        guideManager.hideTipGuide.AddListener(HideTipGuide);
    }

    public void ShowLevelBeginGuide()
    {
        GuidePanelDetails guidePanelDetails = LevelSelector.instance.currentLevel.details;
        guidePanel = Instantiate(guideModal.inGameGuideObject, gameObject.transform);
        guidePanel.GetComponent<Animator>().SetBool("showGuide", true);
        guideModal.NewChoice(guidePanelDetails, "begin");
        StartCoroutine(DisableQuestionMark());
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

    void HideTipGuide()
    {
        guidePanel.GetComponent<Animator>().SetBool("showGuide", false);
    }

    void Finishlevel()
    {
        FadeOutPanel.GetComponent<Animator>().SetBool("fadeOut", true);
        ShowLevelFinishGuide();
    }
}
