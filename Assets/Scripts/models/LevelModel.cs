using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;


[Serializable]
public class LevelModel
{
    public int number;
    public bool isLocked;
    public bool lightIsOn;
    public bool hasParallel;
    public float[] starTimes;
    public int stars;
    public GuidePanelDetails details;
    public Dictionary<int, int> inventoryItems;
    public Dictionary<int, int> mazeItems;
    public Dictionary<int, int> circuitItems;

    public LevelModel(int number)
    {
        this.number = number;
        details = new GuidePanelDetails();
        details.sceneIndex = 2;
        stars = 0;

        Dictionary<string, List<string>> levelMessages = new Dictionary<string, List<string>>();
        List<string> beginMessages = new List<string>();
        List<string> tipMessages = new List<string>();
        List<string> finishMessages = new List<string>();
        List<string> errorMessages = new List<string>();

        if (this.number == 1)
        {
            isLocked = false;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Oppps.., sanki bir problem var");
            beginMessages.Add("istersen çantanı bir kontrol edelim");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Sanki devrende bir şeyler eksik");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");

            errorMessages.Add("Devreyi bir kontrol et derim");

            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);



            details.levelMessages = levelMessages;
        }
        if (this.number == 2)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Oppps.., sanki bir problem var");
            beginMessages.Add("istersen çantanı bir kontrol edelim");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Sanki devrende bir şeyler eksik");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");


            errorMessages.Add("Devreyi bir kontrol et derim");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 3)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Labirentte seni bir süpriz bekliyor");
            //beginMessages.Add("Let's check your inventory together");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Ampül parlaklığını arttırmanın bir yolu var sanki");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");


            errorMessages.Add("Devreyi bir kontrol et derim");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 4)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Labirentte nelerle karşılaşacaksın");
            //beginMessages.Add("Let's check your inventory together");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Direnci devreye ekle ve neler olduğunu gör");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");


            errorMessages.Add("Devreyi bir kontrol et derim");

            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 5)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Hmm ... labirentte bir ampül daha var");
            beginMessages.Add("Bakalım 2 ampül ile neler yapabilirsin");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");

            //tipMessages.Add("Hmmm ...");
            tipMessages.Add("Bakalim parlaklik nasıl değişecek");


            errorMessages.Add("Devreyi bir kontrol et derim");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 6)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Orda birden fazla mi devre elemani var !");
            //beginMessages.Add("Let's check your inventory together");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Deneyerek öğrenme zamanı");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");


            errorMessages.Add("Devreyi bir kontrol et derim");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 7)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Labirent iyice büyümüş gibi");
            beginMessages.Add("Ama sana yardim edeceğim");
            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");


            errorMessages.Add("Devreyi bir kontrol et derim");

            tipMessages.Add("Burası çok aydınlık olucak gibi");
            //tipMessages.Add("Looks like something missing in your circuit");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 8)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Yeni seyler öğrenmeye hazir misin?");
            //beginMessages.Add("Let's check your inventory together");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Paralel baglamak ister misin?");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 9)
        {
            starTimes = new float[2] { 10f, 15f };
            isLocked = true;
            //beginMessages.Add("Labirent iyice büyümüş gibi");
            //beginMessages.Add("Ama sana yardım edeceğim");

            tipMessages.Add("Pili paralel baglayınca ne olacak");
            tipMessages.Add("Ben cok merak ettim");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
        if (this.number == 10)
        {
            isLocked = true;
            starTimes = new float[2] { 10f, 15f };
            beginMessages.Add("Hadi son bölüm");
            beginMessages.Add("Yaparsin sen");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");

            tipMessages.Add("Konuyu öğrendiğini düşünüyorum");
            //tipMessages.Add("Looks like something missing in your circuit");


            levelMessages.Add("begin", beginMessages);
            levelMessages.Add("tip", tipMessages);
            levelMessages.Add("finish", finishMessages);
            levelMessages.Add("error", errorMessages);
            details.levelMessages = levelMessages;
        }
    }

    public string ToJSON(LevelModel level)
    {
        return JsonConvert.SerializeObject(level);
    }

    public static LevelModel CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<LevelModel>(jsonString);
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
            lightIsOn = true;
            hasParallel = false;
        }
        if (this.number == 6)
        {
            lightIsOn = true;
            hasParallel = false;
        }

        if (this.number == 7)
        {
            lightIsOn = true;
            hasParallel = false;
        }

        if (this.number == 8)
        {
            lightIsOn = true;
            hasParallel = true;
        }

        if (this.number == 9)
        {
            lightIsOn = true;
            hasParallel = true;
        }

        if (this.number == 10)
        {
            lightIsOn = true;
            hasParallel = true;
        }
    }

}