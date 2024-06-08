using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   public int attackDamage=10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damageable = collision.GetComponent<Damagable>();

        if (damageable!=null)
        {
            bool gotHit = damageable.Hit(attackDamage);
           
            if (gotHit)
            
                
            Debug.Log(collision.name+"hit"+attackDamage);
        }
    }
    
}
