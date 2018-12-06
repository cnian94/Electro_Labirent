using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    private static LevelSelector _instance;

    public static LevelSelector instance

   
    {
        get { return _instance ?? (_instance = new GameObject("NetworkManager").AddComponent<LevelSelector>()); }
    }

    public Font Font;
    public GameObject LevelContent;
    public Button LevelButton;


    public GameObject StarHolder;

    public int levelName;
    public int base_size;

    private int[] levels = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            //Debug.Log("NOT NULL");
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

    }

    // Use this for initialization
    void Start()
    {
        base_size = 4;
        for (int i = 0; i < levels.Length; i++)
        {
            Button newLevel = Instantiate(LevelButton, LevelContent.transform);
            StarHolder = Instantiate(StarHolder, newLevel.transform);
            //Debug.Log("Level " + levels[i].ToString());
            newLevel.name = levels[i].ToString();
            newLevel.transform.GetChild(0).GetComponent<Text>().text = "" + levels[i].ToString();
            newLevel.transform.GetChild(0).GetComponent<Text>().font = Font ;
            newLevel.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            newLevel.transform.GetChild(0).localScale = new Vector3(0.6f, 0.6f, 1);
            

        }

        for (int i = 0; i < levels.Length; i++)
        {
            GameObject btn = GameObject.Find((i + 1).ToString());
            int levelName = i + 1;
            btn.GetComponent<Button>().onClick.AddListener(delegate { OpenLevel(levelName); });
        }
    }

    void OpenLevel(int name)
    {
        SoundManager.Instance.Play("Button");
        levelName = name;
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
