//The character class provides basic functions for the zombie and human class,
//namely movement, dealing damage, and receiving damage.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public int speed;
    public float speedScale = 1;
    public int hitPoint;
    public float attackTime;
    public GameObject hitEffect;
    protected Animator anime;
    protected int horizontalScale = 1;
    protected SpriteRenderer sprite;

    void Start()
    {
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void MoveTo(Vector2 target)
    {
        //set movement direction
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime * speedScale);
        anime.SetBool("isRunning", true);
        //adjust facing direction according to movement
        if (target.x < transform.position.x)
        {
            sprite.flipX = true;

        }
        else if (target.x > transform.position.x)
        {
            sprite.flipX = false;
        }
    }

    public virtual void TakeDamage(int damageAmount)
    {
        hitPoint -= damageAmount;
        Instantiate(hitEffect, transform);
        //if i am human, me speed will slow down
        if (gameObject.tag == "Human")
        {
            speedScale = 0.3f;
            StartCoroutine(RestoreSpeedAfterDelay(1));
        }
        else
        {
            //this is only for Zombie, the human will extent this on it own
            if (hitPoint < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator RestoreSpeedAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedScale = 1; // Restore the original speed
    }

    protected virtual IEnumerator Attack()
    {
        // Debug.Log("attack trig");
        anime.SetBool("isAttacking", true);
        yield return null;
        anime.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackTime);
    }
}
