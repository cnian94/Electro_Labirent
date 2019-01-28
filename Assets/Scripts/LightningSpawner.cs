using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{

    public GameObject lightning;
    public GameObject MainCamera;
    private Camera cam;

    private float x;
    private float y;

    // Use this for initialization
    void Start()
    {
        cam = MainCamera.GetComponent<Camera>();
        StartCoroutine(GenerateLightnings());
    }

    IEnumerator GenerateLightnings()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            GameObject lightning = Instantiate(this.lightning);
            Vector3 newPos = new Vector3(Random.Range(-3, 4), Random.Range(5, 13), 100);
            lightning.transform.localPosition = newPos;
            Destroy(lightning, 2f);
        }
    }

}
