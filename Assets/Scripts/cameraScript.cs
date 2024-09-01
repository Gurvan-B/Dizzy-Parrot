using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject parrot;
    public GameObject border;
    public GameObject treeSpawner;
    public GameObject clouds;
    public GameObject logicManager;

    // Update is called once per frame
    void Update()
    {
        float deltaX = parrot.transform.position.x - transform.position.x;
        border.transform.position += new Vector3(deltaX, 0, 0);
        clouds.transform.position += new Vector3(deltaX, 0, 0);
        logicManager.transform.position += new Vector3(deltaX, 0, 0);
        transform.position = new Vector3 (parrot.transform.position.x, transform.position.y, 0);
    }
}
