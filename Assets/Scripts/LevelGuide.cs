using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGuide : MonoBehaviour
{

    public Sprite icon;
    public Transform spawnPoint;
    public GameObject thingToSpawn;
    public GuideManager guideManager;

    private GuideModal guideModal;

    void Awake()
    {
        guideModal = GuideModal.Instance;
        guideManager = GuideManager.Instance;
    }

    void Start()
    {
        ShowLevelSceneGuide();
    }

    public void ShowLevelSceneGuide()
    {
        GuidePanelDetails guidePanelDetails = new GuidePanelDetails();
        guidePanelDetails.sceneIndex = 1;
        GameObject guidePanel = Instantiate(guideModal.guidePanelObject, gameObject.transform);

        if (GuideManager.Instance.CurrentPlayer.isFirst)
        {
            Debug.Log("Firt Time User Guide !!");
            guidePanelDetails.messages.Add("Here, our level selector");
            guidePanelDetails.messages.Add("Feel free to start any time");
        }
        else
        {
            guidePanelDetails.messages.Add("Hey, " + GuideManager.Instance.CurrentPlayer.name + " welcome back !");
            guidePanelDetails.iconImage = icon;
        }

        //guidePanelDetails.iconImage = icon;
        guideModal.NewChoice(guidePanelDetails);

    }

}
