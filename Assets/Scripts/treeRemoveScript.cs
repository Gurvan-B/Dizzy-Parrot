using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMoveScript : MonoBehaviour
{

    public float moveSpeed = 5;
    public float deadZone;
    private LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < deadZone + logic.getParrot().transform.position.x)
        {
            Destroy(gameObject);
        }

    }
}
