using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Arrow = 0,
    Axe = 1,
    Boomerang = 2
}
public class Character : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private Animator anim;
    protected string currentAnim;
    public bool isMoving = false;
    protected bool canShoot = true;
    public bool isMovingDuring = false;
    public bool bulletAvailable = true;

    [SerializeField] public float radius;
    [SerializeField] protected Transform spawnBullet;
    public virtual void OnInit()
    {

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
