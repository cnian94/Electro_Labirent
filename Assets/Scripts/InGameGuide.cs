using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGuide : MonoBehaviour
{
    public GameObject QuestionMark;
    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;

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

    void HideTipGuide()
    {
        guidePanel.GetComponent<Animator>().SetBool("showGuide", false);
    }
}
