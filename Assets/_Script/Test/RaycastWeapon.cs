using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPoisition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
    }
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0.0f;

    public ParticleSystem[] muzzleFlash;
    public Transform raycastOrigin;
    public Transform RaycastDes;
    public ParticleSystem hitEffect;
    public TrailRenderer tracerEffect;

    public Ray ray;
    private RaycastHit hitInfo;
    private float accumulatedTime = 0.0f;
    float maxLifeTime = 3.0f;
    List<Bullet> bullets = new List<Bullet>();

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initialPoisition) + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    Bullet CreateBullet(Vector3 poisiton, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPoisition = poisiton;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, poisiton, Quaternion.identity);
        bullet.tracer.AddPosition(poisiton);
        return bullet;
    }

    public void StartFiring()
    {
        accumulatedTime = 0.0f;
        isFiring = true;
        FireBullet();
    }
    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;
        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }
    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullet();
    }
    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }
    void DestroyBullet()
    {
        bullets.RemoveAll(bullets => bullets.time >= maxLifeTime);
    }

    void RaycastSegment(Vector3 start,Vector3 end,Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo,distance))
        {
           hitEffect.transform.position = hitInfo.point;
           hitEffect.transform.forward = hitInfo.normal;
           hitEffect.Emit(1);               

           bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;        
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }
    private void FireBullet()
    {
        foreach (var p in muzzleFlash)
        {
            p.Emit(1);
        }
        Vector3 velocity = (RaycastDes.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position,velocity);
        bullets.Add(bullet);      
    }
    public void StopFiring()
    {
        isFiring = false;
    }
}
