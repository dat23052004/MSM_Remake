﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private Animator anim;
    protected string currentAnim;
    public bool isMoving = false;
    protected bool canShoot = true;
    public bool isMovingDuring = false;
    public bool bulletAvailable = true;

    public int levelScaleAndRadius =0;
    public Vector3 scaleFactor = new Vector3(1.01f, 1.01f, 1.01f);
    public float radius;
    [SerializeField] protected Transform spawnBullet;

    [SerializeField] protected Transform weaponHoldingPos;
    [SerializeField] protected WeaponInfo weaponData;
    [SerializeField] protected PantDataSO pantData;
    [SerializeField] protected HairDataSO hairData;
    protected Weapon weaponInstance;

    protected virtual void Update()
    {
        UpdateLevelCharacter();
    }
    public virtual void OnInit()
    {
        if (weaponData != null)
        {
            ChangeWeapon();
        }
        
    }
    public void UpdateLevelCharacter()
    {
                  Vector3 newScale = new Vector3(
    Mathf.Pow(scaleFactor.x, levelScaleAndRadius),
    Mathf.Pow(scaleFactor.y, levelScaleAndRadius),
    Mathf.Pow(scaleFactor.z, levelScaleAndRadius)
    );
       
        transform.localScale = newScale;
        
        radius = transform.localScale.x * 4f;
    }

    public void IncreaseSizeAndRadius()
    {
        levelScaleAndRadius++;
    }

    protected PoolType GetTypeWeapon(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Arrow:
                return PoolType.Arrow;
            case WeaponType.Axe:
                return PoolType.Axe;
            case WeaponType.Boomerang:
                return PoolType.Boomerang;
            default:
                return 0;
        }
    }

    public void ChangeWeapon()
    {
        weaponData = DataManager.Ins.GetWeaponData(GameManager.Ins.UserData.EquippedWeapon);
        if (weaponInstance != null)
        {
            Destroy(weaponInstance.gameObject);
        }
        weaponInstance = Instantiate(weaponData.weapon, weaponHoldingPos.position, weaponHoldingPos.rotation, weaponHoldingPos);
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null && currentAnim.Length > 0)
            {
                anim.ResetTrigger(currentAnim);
            }
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
