using System.Collections.Generic;
using UnityEngine;

public class zombie : character
{
    public int health;
    public int damage;
    public float attackRange = 0.5f;
    public float chaseRange;


    private GameObject closestHuman;
    private List<GameObject> humansInRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Human")
        {
            humansInRange.Add(collision.gameObject);
            UpdateClosestHuman();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Human")
        {
            humansInRange.Remove(collision.gameObject);
            UpdateClosestHuman();
        }
    }

    private void UpdateClosestHuman()
    {
        closestHuman = null;

        foreach (GameObject human in humansInRange)
        {
            if (closestHuman == null || Vector3.Distance(transform.position, human.transform.position) < Vector3.Distance(transform.position, closestHuman.transform.position))
            {
                closestHuman = human;
            }
        }
    }

    private void Update()
    {
        if (closestHuman != null)
        {
            ChaseAndAttack();
        }
        //else just wander about...
    }

    private void ChaseAndAttack()
    {
        Vector3 attackPoint = closestHuman.transform.position + new Vector3(-1, 0);
        Vector3 distanceVector = attackPoint - transform.position;

        if (distanceVector.magnitude > 0.1)
        {
            //chase
            MoveTo(attackPoint);
        }
        else
        {
            //attack
            // transform.position = Vector2.Lerp(transform.position, closestHuman.transform.position, 0.4f);
            StartCoroutine(Attack());
        }

    }
}
