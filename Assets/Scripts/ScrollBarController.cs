using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    //public GameObject LevelScrollBar;
    private Scrollbar scrollBar;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Name:" + gameObject.name);
        gameObject.GetComponent<ScrollRect>().verticalScrollbar.value = 1;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
