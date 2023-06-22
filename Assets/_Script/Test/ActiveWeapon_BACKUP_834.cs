using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
<<<<<<< HEAD
    }
    public Transform crossHairTarget;
    public Transform[] weaponSlots;
=======
    };
    public Transform crossHairTarget;
    public Transform[] weaponSlots;

    private RaycastWeapon[] equippedWeapon = new RaycastWeapon[2];
    private int activeWeaponIdx;
>>>>>>> origin/20230607_Characters
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
<<<<<<< HEAD
        var weapon = GetWeapon(activeWeaponIndex);
        if (weapon)
=======
        var raycastWeapon = GetWeapon(activeWeaponIdx);
        if (raycastWeapon)
>>>>>>> origin/20230607_Characters
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
<<<<<<< HEAD
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }
        if (!Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
=======
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActivateWeapon(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActivateWeapon(WeaponSlot.Secondary);
>>>>>>> origin/20230607_Characters
        }
    }
    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
<<<<<<< HEAD
        var weapon = GetWeapon(weaponSlotIndex);
        if(weapon)
=======
        var raycastWeapon = GetWeapon(weaponSlotIndex);
        if(raycastWeapon)
>>>>>>> origin/20230607_Characters
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
<<<<<<< HEAD
=======
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
>>>>>>> origin/20230607_Characters
    }
}
