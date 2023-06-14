using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAimming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDur = 0.3f;
    public Rig aimLayer;

    private Camera mainCam;
    private RaycastWeapon raycastWeapon;
    private void Awake()
    {
        mainCam = Camera.main;
        raycastWeapon = GetComponentInChildren<RaycastWeapon>();
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float yawCamera = mainCam.transform.rotation.eulerAngles.y; //lay goc quay cua truc Y
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
        if (aimLayer != null)
        {
            if (Input.GetMouseButton(1))
            {
                aimLayer.weight += Time.deltaTime / aimDur;
            }
            else
            {
                aimLayer.weight -= Time.deltaTime / aimDur;
            }
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            raycastWeapon.StartFiring();
        }
        if (raycastWeapon.isFiring)
        {
            raycastWeapon.UpdateFiring(Time.deltaTime);
        }
        raycastWeapon.UpdateBullets(Time.deltaTime);
        if (Input.GetButtonUp("Fire1"))
        {
            raycastWeapon.StopFiring();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
