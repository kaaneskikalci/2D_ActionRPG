using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   public int attackDamage=10;

   public Vector2 knockback=Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damageable = collision.GetComponent<Damagable>();

        if (damageable!=null)
        {
            Vector2 delieveredKnockback =
                transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage,delieveredKnockback);
           
            if (gotHit)
            
                
            Debug.Log(collision.name+"hit"+attackDamage);
        }
    }
    
}
