using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManagerScript;

public class birdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public float flapStrength;
    public float defaultSlideSpeed;
    public LogicScript logic;
    public bool birdIsAlive = true;

    private bool dashing;
    private bool dashAvailable = true;
    private float dashingVelocity = 14f;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 1f;

    public void Awake()
    {
        myRigidbody2D.gravityScale = 0;
        myRigidbody2D.velocity = new Vector3(defaultSlideSpeed, myRigidbody2D.velocity.y, 0);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        myRigidbody2D.centerOfMass = new Vector2(1,0);
        myRigidbody2D.mass = 0.1f;
        GameManagerScript.instance.subscribeHideMenuCallback(delegate ()
        {
            myRigidbody2D.gravityScale = 4;
            jump();
        });
    }

    // TODO SI appuye SPACE alors Retour menu principal puis (espace pour commencer)

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.instance.getShowMenu() == true || birdIsAlive != true) return;

        if (Input.GetKeyDown(KeyCode.Space) == true && !dashing)
        {
            jump();
        }
        if (Input.GetKeyDown(KeyCode.RightShift) == true && dashAvailable ) 
        {
            StartCoroutine(dash());
        }
    }

    private void FixedUpdate()
    {
        if (GameManagerScript.instance.getShowMenu() == true) return;
        processRotate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameOver();
        if (collision.collider.CompareTag("Ground"))
        {
            myRigidbody2D.freezeRotation = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tornado"))
        {
            gameOver();
        }
    }

    private void gameOver()
    {
        if (birdIsAlive)
        {
            logic.gameOver();
            birdIsAlive = false;
        }
    }

    public void jump()
    {
        myRigidbody2D.velocity = new Vector3(myRigidbody2D.velocity.x, (Vector3.up.y * flapStrength), 0);
        GetComponent<Animator>().Play("Jumping Parrot Animation", -1, 0f);
        AudioManager.instance.playSFX("Jump", 1f);
    }

    private IEnumerator dash()
    {
        dashAvailable = false;
        dashing = true;
        myRigidbody2D.velocity = new Vector3((Vector3.right.x * (defaultSlideSpeed + dashingVelocity)), myRigidbody2D.velocity.y, 0);
        GetComponent<Animator>().Play("Jumping Parrot Animation", -1, 0f);
        AudioManager.instance.playSFX("Dash", 1f);

        yield return new WaitForSeconds(dashingTime);
        dashing = false;
        myRigidbody2D.velocity = new Vector3(defaultSlideSpeed, 0, 0);

        yield return new WaitForSeconds(dashingCooldown);
        dashAvailable = true;
    }

    private void processRotate()
    {
        if (myRigidbody2D.freezeRotation != false) return;

        if (myRigidbody2D.velocity.y > 0)
        {
            if (Mathf.Round(transform.rotation.eulerAngles.z) != 10)
            {
                transform.Rotate(new Vector3(0, 0, 1f));
            }
        }
        else
        {
            if (Mathf.Round(transform.rotation.eulerAngles.z) != 270)
            {
                transform.Rotate(new Vector3(0, 0, -0.4f));
            }
        }
    }
}
