using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDragger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.Instance.dragManagerEvent.AddListener(_ActivateDragger);
    }

    void _ActivateDragger(bool val)
    {
        gameObject.GetComponent<Dragger>().enabled = val;
    }
}
