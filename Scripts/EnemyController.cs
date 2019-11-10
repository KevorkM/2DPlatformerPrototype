using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private float PATROL_TIME = 1.0f;
    private float PATROL_FORCE = 200f;

    private float patrolTimer;
    private Vector2 direction;



    public void Start()
    {
        this.direction = Vector2.right * (Random.Range(0f, 1f) > 0.5 ? 1 : -1);
    }

    public void Update()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= PATROL_TIME)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            patrolTimer = 0;
            direction *= -1;
        }

        this.GetComponent<Rigidbody2D>().AddForce(this.direction * PATROL_FORCE * Time.deltaTime);
    }
}
