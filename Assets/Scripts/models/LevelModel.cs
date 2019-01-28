using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public class LevelModel
{
    public int number;
    public bool isLocked;
    public bool lightIsOn;
    public bool hasParallel;
    public GuidePanelDetails details;
    public Dictionary<int, int> inventoryItems;
    public Dictionary<int, int> mazeItems;
    public Dictionary<int, int> circuitItems;

    public LevelModel(int number)
    {
        this.number = number;
        details = new GuidePanelDetails();
        details.sceneIndex = 2;
        Dictionary<string, List<string>> levelMessages = new Dictionary<string, List<string>>();
        List<string> beginMessages = new List<string>();
        List<string> tipMessages = new List<string>();

        if (this.number == 1)
        {
            isLocked = false;
            beginMessages.Add("Oppps.., something wrong isn't it?");
            beginMessages.Add("Let's check your inventory together");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Looks like something missing in your circuit");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            details.levelMessages = levelMessages;
        }
        else
        {
            isLocked = true;
        }
    }

    public void SetAttributes()
    {
        if (this.number == 1)
        {
            lightIsOn = false;
            hasParallel = false;
        }

        if (this.number == 2)
        {
            lightIsOn = false;
            hasParallel = false;

        }

        if (this.number == 3)
        {
            lightIsOn = true;
            hasParallel = false;
        }

        if (this.number == 4)
        {
            lightIsOn = true;
            hasParallel = false;
        }

        if (this.number == 5)
        {

        }
        if (this.number == 6)
        {

        }

        if (this.number == 7)
        {

        }

        if (this.number == 8)
        {

        }

        if (this.number == 9)
        {

        }

        if (this.number == 10)
        {

        }
    }

}