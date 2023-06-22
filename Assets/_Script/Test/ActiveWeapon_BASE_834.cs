using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public Transform weaponParent;

    private RaycastWeapon raycastWeapon;
    public Animator rigController;


    private void Awake()
    {

        RaycastWeapon existWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existWeapon)
        {
            Equip(existWeapon);
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (raycastWeapon)
        {
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
            if(Input.GetKeyDown(KeyCode.X))
            {
                bool isHoldstered = rigController.GetBool("holdster_weapon");
                rigController.SetBool("holdster_weapon", !isHoldstered);
            }
        }       
    }
    public void Equip(RaycastWeapon newWeapon)
    {
        if(raycastWeapon)
        {
            Destroy(raycastWeapon.gameObject);
        }
        raycastWeapon = newWeapon;
        raycastWeapon.RaycastDes = crossHairTarget;
        raycastWeapon.transform.parent = weaponParent;
        raycastWeapon.transform.localPosition = Vector3.zero;
        raycastWeapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + raycastWeapon.weaponName);
    }
}
