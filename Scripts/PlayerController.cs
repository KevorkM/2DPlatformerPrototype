using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public delegate void OnHitSpikeAction();
    public delegate void OnHitGoombaAction();
    public delegate void OnHitOrbAction();

    public OnHitGoombaAction OnHitGoomba;
    public OnHitSpikeAction OnHitSpike;
    public OnHitOrbAction OnHitOrb;

    private float Speed = 1000;
    private float jumpSpeed = 50000;

    private Vector3 leftBound;
    private Vector3 rightBound;
    private bool canJump;

    public void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey("left") || Input.GetKey("a")) { this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Speed * Time.deltaTime); }
        if (Input.GetKey("right") || Input.GetKey("d")) { this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Speed * Time.deltaTime); }

        if (Input.GetKeyDown("up") || Input.GetKeyDown("w") || Input.GetKeyDown("space")) { Jump(); }
    }

    private void Jump(bool force = false)
    {
        if (canJump || force)
        {
            canJump = false;

            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bound")
        {
            canJump = true;
        }

        if (collision.gameObject.GetComponent<SpikeController>() != null)
        {
            if (OnHitSpike != null)
            {
                OnHitSpike();
            }
        }
        else if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            if (this.transform.position.y > enemy.transform.position.y + enemy.GetComponent<BoxCollider2D>().size.y / 2)
            {
                GameObject.Destroy(collision.gameObject);

                Jump(true);

                if (OnHitGoomba != null)
                {
                    OnHitGoomba();
                }
            }
            else
            {
                if (OnHitSpike != null) { OnHitSpike(); }
            }
        }
        else if (collision.gameObject.GetComponent<OrbController>() != null)
        {
            if (OnHitOrb != null)
            {
                OnHitOrb();
            }
        }
    }



}
