using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float speed = 60f;
    public int damage = 1;

    private Rigidbody2D rb;
    private Animator anime;

    private Vector2 moveAmount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput != Vector2.zero)
        {
            anime.SetBool("isRunning", true);
        }
        else
        {
            anime.SetBool("isRunning", false);
        }
        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        moveAmount = moveInput.normalized * speed * Time.deltaTime;


        rb.MovePosition(rb.position + moveAmount);
        // transform.Translate(1, 0, 0);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other);
        // if (other.tag == "Human")
        // {
        //     other.GetComponent<human>().TakeDamage(damage);
        // }
    }

}


