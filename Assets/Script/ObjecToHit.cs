using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecToHit : MonoBehaviour
{
    public float ObjectHealth = 30f;
    public float moveSpeed = 2f;
    public void ObjectHitDamage(float amount)
    {
        ObjectHealth -= amount;
        Debug.Log(gameObject.name + " lost " + amount + " health. Remaining health: " + ObjectHealth);

        if (ObjectHealth <= 0f)
        {
            Die();
        }
    }

    void Update()
    {
        // Di chuyển GameObject theo hướng forward (hướng Z) với tốc độ moveSpeed
       transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
