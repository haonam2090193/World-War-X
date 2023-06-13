using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    public bool isFiring = false;
    public ParticleSystem[] muzzleFlash;
    public Transform raycastOrigin;
    public Transform RaycastDes;
    public ParticleSystem hitEffect;

    public Ray ray;
    private RaycastHit hitInfo;
    public TrailRenderer tracerEffect;
   public void StartFiring()
    {
        isFiring = true;
        foreach (var p in muzzleFlash)
        {
            p.Emit(1);
        }

        ray.origin = raycastOrigin.position;
        ray.direction = RaycastDes.position - raycastOrigin.position;

        var bulletTracer = Instantiate(tracerEffect,ray.origin, Quaternion.identity);
        bulletTracer.AddPosition(ray.origin);

        if(Physics.Raycast(ray,out  hitInfo))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);
            tracerEffect.transform.position = hitInfo.point;
        }
    }
    public void StopFiring()
    {
        isFiring = false;
    }
}
