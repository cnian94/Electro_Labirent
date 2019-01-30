using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Maze : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north; // 1
        public GameObject east; // 2
        public GameObject west; // 3
        public GameObject south; // 4 
    }

    GameObject[] allWalls;

    public GameObject FinishLine;
    public GameObject Wall;
    public float wallLength;
    public int xSize;
    public int ySize;

    private Vector3 initialPos;
    private GameObject WallHolder;
    private Cell[] cells;
    public int CurrentCell = 0;
    private int TotalCells;
    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int wallToBreak = 0;

    public GameObject Player;

    public GameObject Bulb;
    public GameObject Battery;
    public GameObject Resistor;
    public GameObject Cable;

    private void Awake()
    {
        GameManager.Instance.items.Add(Bulb);
        GameManager.Instance.items.Add(Battery);
        GameManager.Instance.items.Add(Resistor);
    }

    // Use this for initialization
    void Start()
    {
        CalcMazeSize();
        CreateWalls();
        GameManager.Instance.Maze = WallHolder.gameObject;
    }

    void CalcMazeSize()
    {

        if (LevelSelector.instance.currentLevel.number == 1)
        {
            //int dif = 4;
            xSize = 5;
            ySize = 5;
        }

        else if (LevelSelector.instance.currentLevel.number == 2)
        {
            //int dif = 4;
            xSize = 5;
            ySize = 5;
        }

        else if (LevelSelector.instance.currentLevel.number == 3)
        {
            //int dif = 4;
            xSize = 10;
            ySize = 10;
        }

        else if (LevelSelector.instance.currentLevel.number == 4)
        {
            //int dif = 4;
            xSize = 10;
            ySize = 10;
        }

        else if (LevelSelector.instance.currentLevel.number == 5)
        {
            //int dif = 4;
            xSize = 10;
            ySize = 10;
        }

        else if (LevelSelector.instance.currentLevel.number == 6)
        {
            //int dif = 4;
            xSize = 15;
            ySize = 15;
        }

        else if (LevelSelector.instance.currentLevel.number == 7)
        {
            //int dif = 4;
            xSize = 20;
            ySize = 20;
        }

        else if (LevelSelector.instance.currentLevel.number == 8)
        {
            //int dif = 4;
            xSize = 10;
            ySize = 10;
        }

        else if (LevelSelector.instance.currentLevel.number == 9)
        {
            //int dif = 4;
            xSize = 10;
            ySize = 10;
        }

        else if (LevelSelector.instance.currentLevel.number == 10)
        {
            //int dif = 4;
            xSize = 15;
            ySize = 15;
        }

        //xSize = LevelSelector.instance.base_size + dif;
        //ySize = LevelSelector.instance.base_size + dif;
        //gameObject.SetActive(true);
    }

    void GenerateInventoryItems()
    {
        Dictionary<int, int> itemsToCreate = LevelSelector.instance.GetInventoryItems();
        foreach (KeyValuePair<int, int> item in itemsToCreate)
        {
            // do something with entry.Value or entry.Key

            if (item.Key == 0)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    GameObject bulb = GameManager.Instance.items[item.Key];
                    GameManager.Instance.inventory.Add(bulb);
                }

            }

            if (item.Key == 1)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    Debug.Log("Adding Battery to inventory !!");
                    GameObject battery = GameManager.Instance.items[item.Key];
                    GameManager.Instance.inventory.Add(battery);
                }

            }

            if (item.Key == 2)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    GameObject resistor = GameManager.Instance.items[item.Key];
                    GameManager.Instance.inventory.Add(resistor);
                }

            }

            if (item.Key == 3)
            {
                for (int i = 0; i < item.Value; i++)
                {
                    GameObject cable = GameManager.Instance.items[item.Key];
                    GameManager.Instance.inventory.Add(cable);
                }

            }
        }
    }

    void GenerateMazeItems()
    {
        Dictionary<int, int> itemsToCreate = LevelSelector.instance.GetMazeItems();
        Vector3 endPos = allWalls[0].transform.position;

        foreach (KeyValuePair<int, int> item in itemsToCreate)
        {
            // do something with entry.Value or entry.Key
            for (int i = 0; i < item.Value; i++)
            {
                int randomIndex = Random.Range(allWalls.Length * 3 / 4, allWalls.Length - 1);
                Vector3 itemPos = allWalls[randomIndex].gameObject.transform.position;
                //Debug.Log("SELECTED WALL POS: " + itemPos);
                if (itemPos.z < 0)
                {
                    itemPos.z += 0.5f;
                }
                else
                {
                    itemPos.z -= 0.5f;
                }

                GameObject randomItem = Instantiate(GameManager.Instance.items[item.Key], itemPos, Quaternion.identity);
                randomItem.name = GameManager.Instance.items[item.Key].name;
                Vector3 newScale = randomItem.gameObject.transform.localScale;
                newScale.x = 0.4f;
                newScale.y = 0.4f;
                randomItem.gameObject.transform.localScale = newScale;


                randomItem.transform.Rotate(new Vector3(-90, 0, 0));
                StartCoroutine(SetRandomItemParent(randomItem.transform));

            }
        }

    }

    IEnumerator SetRandomItemParent(Transform item)
    {
        yield return new WaitForSeconds(0.5f);
        item.SetParent(GameManager.Instance.Maze.transform);
    }


    void SpawnPlayer(GameObject[] allWalls)
    {
        Destroy(allWalls[0].gameObject);
        GenerateInventoryItems();
        GenerateMazeItems();
        Vector3 playerPos = Vector3.zero;
        playerPos.x = allWalls[allWalls.Length - 1].transform.position.x;
        playerPos.z = allWalls[allWalls.Length - 1].transform.position.z - 0.5f;
        Vector3 newScale = new Vector3(0.4f, 0.4f, 0.4f);

        Player = Instantiate(Player, playerPos, Quaternion.identity) as GameObject;
        Player.transform.localScale = newScale;
        Player.name = "Player";
        Player.transform.SetParent(WallHolder.transform);
        GameObject finishLine = Instantiate(FinishLine, allWalls[0].transform.position, Quaternion.identity);
        finishLine.transform.Rotate(90, 0, 0);
        finishLine.transform.SetParent(WallHolder.transform);
     


        GameManager.Instance.FindPlayer.Invoke();
    }

    void CreateWalls()
    {
        WallHolder = new GameObject();
        WallHolder.name = "Maze";
        WallHolder.tag = "Maze";
        initialPos = new Vector3((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2) + wallLength / 2);
        Vector3 myPos = initialPos;
        GameObject tempWall;

        //For x axis 
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 0.0f, initialPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(Wall, myPos, Quaternion.identity) as GameObject;
                //Vector3 newScale = tempWall.transform.localScale;
                //newScale.y = newScale.y * wallLength;
                //tempWall.transform.localScale = newScale;
                tempWall.transform.parent = WallHolder.transform;
                //Physics.IgnoreCollision(Player.GetComponent<Collider>(), tempWall.GetComponent<Collider>());
            }
        }

        //For y axis 
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), 0.0f, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(Wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                //Vector3 newScale = tempWall.transform.localScale;
                //newScale.y = newScale.y * wallLength;
                //tempWall.transform.localScale = newScale;
                tempWall.transform.parent = WallHolder.transform;
                //Physics.IgnoreCollision(Player.GetComponent<Collider>(), tempWall.GetComponent<Collider>());

            }
        }

        CreateCells();

    }


        void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        TotalCells = xSize * ySize;
        int children = WallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = new Cell[xSize * ySize];
        int eastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = WallHolder.transform.GetChild(i).gameObject;
        }

        for (int cellProcess = 0; cellProcess < cells.Length; cellProcess++)
        {
            cells[cellProcess] = new Cell();
            cells[cellProcess].east = allWalls[eastWestProcess];
            cells[cellProcess].south = allWalls[childProcess + (xSize + 1) * ySize];
            if (termCount == xSize)
            {
                eastWestProcess += 2;
                termCount = 0;
            }
            else
            {
                eastWestProcess++;
            }
            termCount++;
            childProcess++;
            cells[cellProcess].west = allWalls[eastWestProcess];
            cells[cellProcess].north = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];
        }
        //GiveMeNeighbour();
        CreateMaze();
        SpawnPlayer(allWalls);
    }

    void CreateMaze()
    {

        while (visitedCells < TotalCells)
        {
            if (startedBuilding)
            {
                GiveMeNeighbour();
                if (!cells[currentNeighbour].visited && cells[CurrentCell].visited)
                {
                    BreakWall();
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    lastCells.Add(CurrentCell);
                    CurrentCell = currentNeighbour;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                CurrentCell = Random.Range(0, TotalCells);
                cells[CurrentCell].visited = true;
                visitedCells++;
                startedBuilding = true;
            }
            //Invoke("CreateMaze", 0.0f);
        }
    }

    void BreakWall()
    {
        switch (wallToBreak)
        {
            case 1:
                Destroy(cells[CurrentCell].north);
                break;

            case 2:
                Destroy(cells[CurrentCell].east);
                break;

            case 3:
                Destroy(cells[CurrentCell].west);
                break;

            case 4:
                Destroy(cells[CurrentCell].south);
                break;


        }
    }

    void GiveMeNeighbour()
    {
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;
        check = (CurrentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += xSize;

        //west
        if (CurrentCell + 1 < TotalCells && (CurrentCell + 1) != check)
        {
            if (!cells[CurrentCell + 1].visited)
            {
                neighbours[length] = CurrentCell + 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        //east 
        if (CurrentCell - 1 >= 0 && CurrentCell != check)
        {
            if (!cells[CurrentCell - 1].visited)
            {
                neighbours[length] = CurrentCell - 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        //north
        if (CurrentCell + xSize < TotalCells)
        {
            if (!cells[CurrentCell + xSize].visited)
            {
                neighbours[length] = CurrentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        //south
        if (CurrentCell - xSize >= 0)
        {
            if (!cells[CurrentCell - xSize].visited)
            {
                neighbours[length] = CurrentCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }
        if (length != 0)
        {
            int theChosenOne = Random.Range(0, length);
            currentNeighbour = neighbours[theChosenOne];
            wallToBreak = connectingWall[theChosenOne];
        }
        else
        {
            if (backingUp > 0)
            {
                CurrentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
