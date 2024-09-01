using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class TreeSpawnerScript : MonoBehaviour
{
    public GameObject tree;
    public GameObject parrot;
    private float spawnRate;
    private float timer;
    private float defaultMoveSpeed;
    private float distanceBetweenTrees = 16;

    // Start is called before the first frame update
    void Awake()
    {
        defaultMoveSpeed = parrot.GetComponent<birdScript>().defaultSlideSpeed;
        spawnRate = distanceBetweenTrees / defaultMoveSpeed;
        timer = spawnRate;
        GameManagerScript.instance.subscribeHideMenuCallback(delegate ()
        {
            transform.position += Vector3.right * parrot.transform.position.x;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.getShowMenu() == false)
        {
            float multiplier = parrot.GetComponent<Rigidbody2D>().velocity.x / defaultMoveSpeed;
            if (timer < spawnRate)
            {
                timer = timer + Time.deltaTime * multiplier;
            }
            else
            {
                spawnPipe();
                timer = 0;
            }
        }
    }

    public void spawnPipe()
    {
        float heightOffset = 6;
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Instantiate(tree, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 10), transform.rotation);
        transform.position += Vector3.right * distanceBetweenTrees;
    }
}
