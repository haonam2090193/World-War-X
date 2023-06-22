using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    };
    public Transform crossHairTarget;
    public Transform[] weaponSlots;

    private RaycastWeapon[] equippedWeapon = new RaycastWeapon[2];
    private int activeWeaponIdx;
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
        var raycastWeapon = GetWeapon(activeWeaponIdx);
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
                ToggleActiveWeapon();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActivateWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActivateWeapon(WeaponSlot.Secondary);
        }
    }
    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var raycastWeapon = GetWeapon(weaponSlotIndex);
        if(raycastWeapon)
        {
            Destroy(raycastWeapon.gameObject);
        }
        raycastWeapon = newWeapon;
        raycastWeapon.RaycastDes = crossHairTarget;
        raycastWeapon.transform.SetParent(weaponSlots[weaponSlotIndex],false);
        SetActivateWeapon(newWeapon.weaponSlot);
    }
    private void SetActivateWeapon(WeaponSlot weaponSlot)
    {
        int hosterIndex = activeWeaponIdx;
        int activeIndex = (int)weaponSlot;
        StartCoroutine(SwitchWeapon(hosterIndex, activeIndex));
    }
    private void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("hoster_weapon");
        if(isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIdx));
        }
        else
        {
            StartCoroutine(HosterWeapon(activeWeaponIdx));
        }
    }
    private RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equippedWeapon.Length)
        {
            return null;
        }
        return equippedWeapon[index];
    }
    private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HosterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIdx = activateIndex;
    }
    private IEnumerator HosterWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holdster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

        }
    }
    private IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holdster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
}
