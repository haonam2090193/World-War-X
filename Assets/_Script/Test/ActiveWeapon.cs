using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }
    public Transform crossHairTarget;
    public Transform[] weaponSlots;
    public Animator rigController;
    private RaycastWeapon[] equipped_Weapon = new RaycastWeapon[2];
    int activeWeaponIndex;
    private void Awake()
    {

        RaycastWeapon existWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existWeapon)
        {
            Equip(existWeapon);
        }
    }
    RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equipped_Weapon.Length)
        {
            return null;
        }
        return equipped_Weapon[index];
    }
    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        if (weapon)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }
            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
            weapon.UpdateBullets(Time.deltaTime);
            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                ToggleActiveWeapon();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (!Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }
    }
    public void Equip (RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.RaycastDes = crossHairTarget;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex],false);     
        equipped_Weapon[weaponSlotIndex] = weapon;
        activeWeaponIndex = weaponSlotIndex;

        SetActiveWeapon(newWeapon.weaponSlot);
    }

    void ToggleActiveWeapon()
    {

        bool isHoldstered = rigController.GetBool("holdster_weapon");
        if(isHoldstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HosterWeapon(activeWeaponIndex));
        }
    }
    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int hosterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;
        StartCoroutine(SwitchWeapon(hosterIndex, activateIndex));
    }
    IEnumerator SwitchWeapon(int hosterIndex, int activateIndex)
    {
        yield return StartCoroutine(HosterWeapon(hosterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }
    IEnumerator HosterWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if(weapon)
        {
            rigController.SetBool("holdster_weapon", true);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }
    IEnumerator ActivateWeapon(int index)
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
