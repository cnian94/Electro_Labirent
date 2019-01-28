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
        get { return _instance ?? (_instance = new GameObject("LevelSelector").AddComponent<LevelSelector>()); }
    }

    [System.Serializable]
    public class LevelItemsEvent : UnityEngine.Events.UnityEvent<Dictionary<int, int>> { }
    public LevelItemsEvent levelItemsEvent;


    [System.Serializable]
    public class OpenLevelEvent : UnityEngine.Events.UnityEvent<int> { }
    public OpenLevelEvent openLevelEvent;

    public LevelModel currentLevel;

    public int levelName;
    public int base_size;




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
        openLevelEvent.AddListener(OpenLevel);
        base_size = 4;   //GameManager  CalcMazeSize()
    }

    void OpenLevel(int number)
    {
        //this.currentLevel = new LevelModel(name);
        currentLevel = GuideManager.Instance.CurrentPlayer.levels[number - 1];
        print("current levell: " + currentLevel.number);
        currentLevel.inventoryItems = GetInventoryItems();
        currentLevel.mazeItems = GetMazeItems();
        currentLevel.circuitItems = GetCircuitItems();
        currentLevel.SetAttributes();
        SoundManager.Instance.Play("Button");
        SceneManager.LoadScene(2);
    }

    public Dictionary<int, int> GetInventoryItems()
    {
        Dictionary<int, int> items = new Dictionary<int, int>();

        // 0 = bulb   1 = battery  2 = resistor, 3 = cable   itemsAdd(hangi item, sayısı) 

        if (this.currentLevel.number == 1)
        {
            items.Add(1, 1);
        }

        else if (this.currentLevel.number == 2)
        {
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 3)
        {
            Debug.Log("There is no element in the inventory at level 3 !!");
        }

        else if (this.currentLevel.number == 4)
        {
            Debug.Log("There is no element in the inventory at level 4 !!");
        }

        else if (this.currentLevel.number == 5)
        {
            Debug.Log("There is no element in the inventory at level 5 !!");
        }

        else if (this.currentLevel.number == 6)
        {
            Debug.Log("There is no element in the inventory at level 6 !!");
        }

        else if (this.currentLevel.number == 7)
        {
            Debug.Log("There is no element in the inventory at level 7 !!");
        }

        else if (this.currentLevel.number == 8)
        {
            Debug.Log("There is no element in the inventory at level 8 !!");
        }

        else if (this.currentLevel.number == 9)
        {
            Debug.Log("There is no element in the inventory at level 9 !!");
        }

        else if (this.currentLevel.number == 10)
        {
            Debug.Log("There is no element in the inventory at level 10 !!");
        }

        return items;
    }

    public Dictionary<int, int> GetMazeItems()
    {
        Dictionary<int, int> items = new Dictionary<int, int>();

        // 0 = bulb   1 = battery  2 = resistor, 3 = cable   itemsAdd(hangi item, sayısı) 

        if (this.currentLevel.number == 1)
        {
            Debug.Log("There is no element in the maze at level 1 !!");
        }

        else if (this.currentLevel.number == 2)
        {
            Debug.Log("There is no element in the maze at level 2 !!");
        }

        else if (this.currentLevel.number == 3)
        {
            items.Add(1, 1);
        }

        else if (this.currentLevel.number == 4)
        {
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 5)
        {
            items.Add(0, 1);
        }

        else if (this.currentLevel.number == 6)
        {
            items.Add(0, 1);
            items.Add(1, 1);
        }

        else if (this.currentLevel.number == 7)
        {
            items.Add(1, 3);
        }

        else if (this.currentLevel.number == 8)
        {
            items.Add(1, 1);
        }

        else if (this.currentLevel.number == 9)
        {
            items.Add(0, 1);
        }

        else if (this.currentLevel.number == 10)
        {
            items.Add(0, 1);
            items.Add(1, 1);
        }

        return items;
    }

    public Dictionary<int, int> GetCircuitItems()
    {
        Dictionary<int, int> items = new Dictionary<int, int>();

        // 0 = bulb   1 = battery  2 = resistor, 3 = cable   itemsAdd(hangi item, sayısı) 

        if (this.currentLevel.number == 1)
        {
            items.Add(0, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 2)
        {
            items.Add(0, 1);
            items.Add(1, 1);
        }

        else if (this.currentLevel.number == 3)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 4)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 5)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 6)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 7)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 8)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 9)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        else if (this.currentLevel.number == 10)
        {
            items.Add(0, 1);
            items.Add(1, 1);
            items.Add(2, 1);
        }

        return items;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
