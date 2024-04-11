using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private LayerMask groundLayer;    
    private WeaponType currentWeapon = (WeaponType)2;
    
    void Start()
    {
        OnInit();
    }
   
    void Update()
    {
        Moving();
        CheckSight();
    }

    public override void OnInit()
    {
        
    }
    public void Moving()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 velocity = movement * moveSpeed;
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + transform.forward * 0.7f;  
        if(Physics.Raycast(raycastOrigin,Vector3.down, out hit, 5f, groundLayer))
        {
            rb.velocity = new Vector3(velocity.x, velocity.y, velocity.z);
            isMoving = movement.magnitude > 0;   
            
        }
        else
        {
            rb.velocity = Vector3.zero;
            isMoving = false;
            ChangeAnim(Constant.ANIM_IDLE);
        }
        if (movement.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            ChangeAnim(Constant.ANIM_RUN);
        }
        else
        {
            ChangeAnim(Constant.ANIM_IDLE);
            isMoving = false;           
        }
    }
    
    public void CheckSight()
    {
        if (!isMoving)
        {
            isMovingDuring = false;
            if (CheckAnemy(out Vector3 enemyposition))
            {
                Vector3 directToBot = (enemyposition - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(directToBot);
                ChangeAnim(Constant.ANIM_ATTACK);
                if (canShoot)
                {
                    canShoot = false;                    
                    StartCoroutine(ShootCoroutine(.2f));
                }
            }
           
        }
        else
        {
            isMovingDuring = true;
        }
    }

    public bool CheckAnemy(out Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        position = Vector3.zero;        
        foreach(Collider collider in colliders)
        {           
            if(collider.gameObject != gameObject && collider.CompareTag(Constant.CharacterTag))
            {
                
                position = collider.transform.position;
                return true;
            }
        }
        return false;
    }

    public void Shoot(WeaponType weaponType)
    {
        BulletBase bullet = SimplePool.Spawn<BulletBase>(GetTypeWeapon(weaponType),spawnBullet.position, transform.rotation);
        bullet.DirectToBot = transform.forward;
        bullet.character = this;
        bulletAvailable = false;
        StartCoroutine(DestroyBullet(bullet, radius));
      
    }

    public IEnumerator ShootCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(bulletAvailable && !isMovingDuring)
        {                        
            Shoot(currentWeapon);
            yield return new WaitForSeconds(1.5f);
            canShoot = true;
        }
        else
        {
            canShoot = true;
        }
    }

    public IEnumerator DestroyBullet(BulletBase bullet, float maxDistance)
    {
        Vector3 initialPosition = bullet.transform.position;
        while(Vector3.Distance(initialPosition, bullet.transform.position) < maxDistance)
        {
            yield return null;
        }
        bullet.OnDespawn();
        bulletAvailable = true;
    }
    
    
    void OnDrawGizmos()
    {
        // Vẽ hình cầu để hiển thị vùng không gian
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    
}
