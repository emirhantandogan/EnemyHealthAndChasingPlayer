using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    //Variables for chasing player
    public Transform target;
    public float speed;
    public Vector2 direction;

    //Variables for flip image of enemy when chasing
    public bool shouldFlip;//Check if you want to flip the image of the enemy on the x axis.
    public float scaleMagnitudeX;

    //Variables for enemy health
    public float enemyHealth=100;
    public GameObject blood;//Particle effect for blood
    public float damage;//The amount of damage that will be taken from the enemy's health


    void Start()
    {
        scaleMagnitudeX = this.gameObject.transform.localScale.x;
        playerHealthScript = GameObject.Find("Player").GetComponent<playerHealth>();
    }

    void Update()
    {
        //Chasing the player
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        direction = (target.transform.position - transform.position).normalized;

        //Enemy image flip
        if (shouldFlip == true) 
        {
            if (direction.x > 0)
            {
                this.gameObject.transform.localScale = new Vector2(-scaleMagnitudeX, transform.localScale.y);
            }
            else
                this.gameObject.transform.localScale = new Vector2(scaleMagnitudeX, transform.localScale.y);
        }

        //Destroying enemy and creating blood
        if (enemyHealth <= 0) 
        {
            Instantiate(blood);//blood is the particle effect
            Destroy(this.gameObject);
        }
    }

    //Code that checks if the bullet thouched the enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet") 
        {
            enemyHealth -= damage;
        }
    }
}
