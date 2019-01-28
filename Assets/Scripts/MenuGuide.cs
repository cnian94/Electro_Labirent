using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGuide : MonoBehaviour
{

    public Sprite icon;
    //public Transform spawnPoint;
    //public GameObject thingToSpawn;
    //public GuideManager guideManager;

    private GuideModal guideModal;

    void Awake()
    {
        guideModal = GuideModal.Instance;
        //guideManager = GuideManager.Instance;
    }

    void Start()
    {
        ShowMenuSceneGuide();
    }

    public void ShowMenuSceneGuide()
    {
        GuidePanelDetails guidePanelDetails = new GuidePanelDetails();
        guidePanelDetails.sceneIndex = 0;
        GameObject guidePanel = Instantiate(guideModal.guidePanelObject, gameObject.transform);
        if (GuideManager.Instance.CurrentPlayer.isFirst)
        {
            Debug.Log("Firt Time User Guide !!");
            guidePanelDetails.messages.Add("Selam " + GuideManager.Instance.CurrentPlayer.name + " Elektro Labirente Hoşgeldin");
            guidePanelDetails.messages.Add("Sana oyun boyunca yardım edeceğim");
            guidePanelDetails.messages.Add("Eğer sen de hazırsan başlayalım");
        }

        else
        {
            guidePanelDetails.messages.Add("Selam " + GuideManager.Instance.CurrentPlayer.name + " tekrar hoşgeldin");
        }

        //guidePanelDetails.iconImage = icon;
        guideModal.NewChoice(guidePanelDetails);

    }


    /*
    //  Send to the Modal Panel to set up the Buttons and functions to call
    public void TestC()
    {
        GuidePanelDetails guidePanelDetails = new GuidePanelDetails();
        guidePanelDetails.question = "This is an announcement!\nIf you don't like it, shove off!";
        guidePanelDetails.button1Details = new EventButtonDetails();
        guidePanelDetails.button1Details.buttonTitle = "Gotcha!";
        guidePanelDetails.button1Details.action = TestCancelFunction;

        guideModal.NewChoice(guidePanelDetails);
    }
    public void TestCI()
    {
        GuidePanelDetails modalPanelDetails = new GuidePanelDetails { question = "This is an announcement!\nIf you don't like it, shove off!", iconImage = icon };
        modalPanelDetails.button1Details = new EventButtonDetails { buttonTitle = "Gotcha!", action = TestCancelFunction };

        guideModal.NewChoice(modalPanelDetails);
    }

    //  public void TestYN () {
    //      modalPanel.Choice ("Cheese on your burger?", TestYesFunction, TestNoFunction);
    //  }
    //  
    //  public void TestYNC () {
    //      modalPanel.Choice ("Would you like a poke in the eye?\nHow about with a sharp stick?", TestYesFunction, TestNoFunction, TestCancelFunction);
    //  }
    //  
    //  public void TestYNI () {
    //      modalPanel.Choice ("Do you like this icon?", icon, TestYesFunction, TestNoFunction, TestCancelFunction);
    //  }
    //  
    //  public void TestYNCI () {
    //      modalPanel.Choice ("Do you want to use this icon?", icon, TestYesFunction, TestNoFunction, TestCancelFunction);
    //  }
    //  
    //  public void TestLambda () {
    //      modalPanel.Choice ("Do you want to create this sphere?", () => { InstantiateObject(thingToSpawn); }, TestNoFunction);
    //  }
    //  
    //  public void TestLambda2 () {
    //      modalPanel.Choice ("Do you want to create two spheres?", () => { InstantiateObject(thingToSpawn, thingToSpawn); }, TestNoFunction);
    //  }

    public void TestLambda3()
    {
        GuidePanelDetails modalPanelDetails = new GuidePanelDetails { question = "Do you want to create three spheres?" };
        modalPanelDetails.button1Details = new EventButtonDetails
        {
            buttonTitle = "Yes Please!",
            action = () => { InstantiateObject(thingToSpawn); InstantiateObject(thingToSpawn, thingToSpawn); }
        };
        modalPanelDetails.button2Details = new EventButtonDetails
        {
            buttonTitle = "No thanks!",
            action = TestNoFunction
        };

        guideModal.NewChoice(modalPanelDetails);
    }

    //  The function to call when the button is clicked
    void TestYesFunction()
    {
        guideManager.DisplayMessage("Heck, yeah!");
    }

    void TestNoFunction()
    {
        guideManager.DisplayMessage("No way, Jose!");
    }

    void TestCancelFunction()
    {
        guideManager.DisplayMessage("I give up!");
    }

 

    void InstantiateObject(GameObject thingToInstantiate)
    {
        guideManager.DisplayMessage("Here you go!");
        Instantiate(thingToInstantiate, spawnPoint.position, spawnPoint.rotation);
    }

    void InstantiateObject(GameObject thingToInstantiate, GameObject thingToInstantiate2)
    {
        guideManager.DisplayMessage("Here you go!");
        Instantiate(thingToInstantiate, spawnPoint.position - Vector3.one, spawnPoint.rotation);
        Instantiate(thingToInstantiate2, spawnPoint.position + Vector3.one, spawnPoint.rotation);
    }   */
}

