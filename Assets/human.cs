using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human : character
{
    public GameObject zombie;
    public GameObject DialogBubble;
    public List<Vector2> targetList;
    private Vector2 target;
    private int targetCount;
    bool firstTimeReachTarget = true;



    private void Start()
    {
        SetNewTarget();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DialogBubble.transform.localScale = new Vector3(horizontalScale, 1);
        if (Vector2.Distance(target, transform.position) < 0.1)
        {
            if (firstTimeReachTarget)
            {
                firstTimeReachTarget = false;
                StartCoroutine(SetNewTarget());
            }
        }
        else
        {
            firstTimeReachTarget = true;
        }
        MoveTo(target);
    }


    private IEnumerator SetNewTarget()
    {
        // int randomX = Random.Range(-10, 10);
        // int randomY = Random.Range(-10, 10);
        // target = new Vector2(randomX, randomY);
        targetCount += 1;
        if (targetCount > targetList.Count - 1)
        {
            targetCount = 0;
        }
        target = targetList[targetCount];
        speedScale = 0;
        anime.SetBool("isRunning", false);
        yield return new WaitForSeconds(1);
        speedScale = 1;
        anime.SetBool("isRunning", true);

    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        if (hitPoint < 0)
        {
            Instantiate(zombie, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private IEnumerator RenderDialogBubble()
    {
        DialogBubble.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        // Hide the dialog bubble
        DialogBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("human vision entered", other);
        // StartCoroutine(RenderDialogBubble());
    }

}
