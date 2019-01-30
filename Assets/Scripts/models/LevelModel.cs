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
            beginMessages.Add("İstersen çantanı bir kontrol edelim");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Sanki devrende bir şeyler eksik");
            tipMessages.Add("Devreyi kurmak için ayar tuşuna bas");
            tipMessages.Add("Kurduktan sonra ayar tuşuna tekrar bas");
            tipMessages.Add("Son olarak devreyi kontrol tuşuna bas");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Artık devreye gücü pilin verdiğini biliyorsun");
            finishMessages.Add("Haydi durma, daha öğrenecek çok şeyimiz var");

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
            beginMessages.Add("İstersen çantanı bir kontrol edelim");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Sanki devrende bir şeyler eksik");

            finishMessages.Add("İlk iki bölüm bitti bile !!");
            finishMessages.Add("Artık devrede direnç olması gerektiğini biliyorsun.");
            finishMessages.Add("3. bölüm seni bekliyor..");

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

            finishMessages.Add("İşte bu kadar !!");
            finishMessages.Add("2 pil ile ampül parlaklığını nasıl arttırdığını gördün");
            finishMessages.Add("Yeni şeyler öğrenmek için durma");

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

            finishMessages.Add("İşte bu!!");
            finishMessages.Add("Direnç sayısı artarsa ampül daha az parlaklık verir");
            finishMessages.Add("Haydi durma, daha öğrenecek çok şeyimiz var");

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

            finishMessages.Add("Yarısı bitti bile !!");
            finishMessages.Add("Ampüller seri bağlandığı zaman daha az ışık verirler");
            finishMessages.Add("Bir sonraki bölüm biraz daha çeşitli olacak");

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
            beginMessages.Add("Orda birden fazla mı devre elemanı var !");
            //beginMessages.Add("Let's check your inventory together");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Deneyerek öğrenme zamanı!!");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Bu bölümü de kolaylıkla bitirdin");
            finishMessages.Add("Gözlerime inanamıyorum");

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
            beginMessages.Add("Ama sana yardım edeceğim");

            tipMessages.Add("Burası çok aydınlık olucak gibi");
            //tipMessages.Add("Looks like something missing in your circuit");

            finishMessages.Add("Aferin evlat !!");
            finishMessages.Add("Seri ne kadar pil bağlarsan aydınlık o kadar artar!");
            finishMessages.Add("Artık yeni şeyler öğrenme vakti..");

            errorMessages.Add("Devreyi bir kontrol et derim");

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
            beginMessages.Add("Yeni şeyler öğrenmeye hazır mısın?");
            //beginMessages.Add("Let's check your inventory together");

            tipMessages.Add("Hmmm ...");
            tipMessages.Add("Paralel baglamak ister misin?");
            tipMessages.Add("Bunun için kablo tuşuna basman yeterli");
            tipMessages.Add("Unutma paralel kabloları boş bırakmamalısın");

            finishMessages.Add("Yeni konuyu hemen kaptın");
            finishMessages.Add("Pili paralel bağlamak parlaklığı arttırmaz..");
            finishMessages.Add("Ancak yanma süresini arttırır!!");
            finishMessages.Add("Hadi durma, daha öğrenecek çok şeyimiz var");




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

            tipMessages.Add("Ampülü paralel bağlayınca ne olacak");
            tipMessages.Add("Ben çok merak ettim");

            finishMessages.Add("Bitti sayılır !!");
            finishMessages.Add("Ampülü paralel bağlarsan parlaklık değişmez!!");
            finishMessages.Add("Son bölüm için şimdiden heyecanlandım");


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
            beginMessages.Add("Haydi son bölüm");
            beginMessages.Add("Yaparsın sen");

            tipMessages.Add("Konuyu öğrendiğini düşünüyorum");
            tipMessages.Add("ve seni yaratıcılığınla baş başa bırakıyorum..");

            finishMessages.Add("VEEEEE OYUNUN SONU !!");
            finishMessages.Add("Devre ve ampül parlaklığı konularını öğrendin");
            finishMessages.Add("Seninle oyun oynamak benim için bir zevkti..");

            


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