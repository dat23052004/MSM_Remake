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

    public int levelScaleAndRadius =0;
    public Vector3 scaleFactor = new Vector3(1.01f, 1.01f, 1.01f);
    public float radius;
    [SerializeField] protected Transform spawnBullet;

    public virtual void Update()
    {
        
        UpdateLevelCharacter(); 
    }
    public virtual void OnInit()
    {
        
    }
    public void UpdateLevelCharacter()
    {
        Vector3 newScale = new Vector3(
    Mathf.Pow(scaleFactor.x, levelScaleAndRadius),
    Mathf.Pow(scaleFactor.y, levelScaleAndRadius),
    Mathf.Pow(scaleFactor.z, levelScaleAndRadius)
    );
        Debug.Log(newScale);
        transform.localScale = newScale;

        radius = transform.localScale.x * 5f;
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
