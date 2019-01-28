using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    public GameObject LevelContent;
    public GameObject StarHolder;
    public Button LevelButton;

    //private int[] levels = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    List<LevelModel> levels;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        levels = GuideManager.Instance.CurrentPlayer.levels;
        for (int i = 0; i < levels.Count; i++)
        {
            Button newLevel = Instantiate(LevelButton, LevelContent.transform);
            GameObject newStarHolder = Instantiate(StarHolder, newLevel.transform);
            newLevel.name = levels[i].number.ToString();
            newLevel.transform.GetChild(0).GetComponent<Text>().text = "" + levels[i].number.ToString();  // Burayı Doğa ile yaptık zaten yerleştirme ve font ile alakalı

            if (!levels[i].isLocked)
            {
                newLevel.interactable = true;
                newLevel.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                newLevel.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                foreach (Transform child in newStarHolder.transform)
                {
                    child.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
            }
            else
            {
                newLevel.interactable = false;
            }

            GameObject[] levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");

            for (int j = 0; j < levelButtons.Length; j++)
            {
                if (levelButtons[j].GetComponent<Button>().interactable)
                {
                    int levelNumber = j + 1;
                    levelButtons[j].GetComponent<Button>().onClick.AddListener(delegate { OpenLevel(levelNumber); });
                }

            }
        }
    }

    void OpenLevel(int name)
    {
        LevelSelector.instance.openLevelEvent.Invoke(name);

    }
}
