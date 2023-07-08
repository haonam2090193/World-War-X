
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Health health;
    public Rigidbody rb;
    public void OnHit(RaycastWeapon raycastWeapon, Vector3 direction)
    {
        health.TakeDamage(raycastWeapon.damage, direction, rb);
    }
}
