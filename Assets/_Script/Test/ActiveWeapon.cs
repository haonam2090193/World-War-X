using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }

    public Animator rigController;
    public Transform crosshairTarget;
    public Transform[] weaponSlots;
    public CinemachineFreeLook playerCamera;
    public AmmoWidget ammoWidget;

    private RaycastWeapon[] equippedWeapons = new RaycastWeapon[2];
    private int activeWeaponIdx;
    private bool isHolstered = false;

    private void Awake()
    {
        RaycastWeapon existWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existWeapon)
        {
            Equip(existWeapon);
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIdx);
    }

    void Update()
    {
        var raycastWeapon = GetWeapon(activeWeaponIdx);
        if (raycastWeapon && !isHolstered)
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



            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetActiveWeapon(WeaponSlot.Primary);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetActiveWeapon(WeaponSlot.Secondary);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveWeapon();
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var raycastWeapon = GetWeapon(weaponSlotIndex);
        if (raycastWeapon)
        {
            Destroy(raycastWeapon.gameObject);
        }
        raycastWeapon = newWeapon;
        raycastWeapon.raycastDestination = crosshairTarget;
        raycastWeapon.weaponRecoil.playerCamera = playerCamera;
        raycastWeapon.weaponRecoil.rigController = rigController;
        raycastWeapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        equippedWeapons[weaponSlotIndex] = raycastWeapon;
        SetActiveWeapon(newWeapon.weaponSlot);

        ammoWidget.Refresh(raycastWeapon.ammoCount);
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIdx;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    private void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holdster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIdx));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIdx));
        }
    }

    private RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= equippedWeapons.Length)
        {
            return null;
        }
        return equippedWeapons[index];
    }

    private IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIdx = activateIndex;
    }


    private IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holdster_weapon", true);
            yield return new WaitForSeconds(0.1f);
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
            isHolstered = false;
        }
    }

}
