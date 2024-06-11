using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore=20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    private AudioSource pickupSound;

    private void Awake()
    {
        pickupSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            bool wasHealed =damagable.Heal(healthRestore);
            if (wasHealed)
                if (pickupSound)

                    AudioSource.PlayClipAtPoint(pickupSound.clip,gameObject.transform.position,pickupSound.volume);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;

    }
}
