using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public int damageAmount;
    public string OwnerType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(OwnerType))
        {
            Debug.Log("compare tag pass");
            if (other.gameObject.tag == "Human")
            {
                Debug.Log("Huamn func trigger");
                other.GetComponent<human>().TakeDamage(damageAmount);
            }

        }
    }
}
