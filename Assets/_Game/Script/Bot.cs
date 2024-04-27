using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private IState<Bot> currentState;
    public Vector3 walkPoint;      // Vi tri di chuyen den
    public bool walkPointSet;
    public float walkPointRange;   // Pham vi di chuyen 
    private WeaponType currentWeapon = WeaponType.Arrow;
    void Start()
    {
        OnInit();
    }


    void Update()
    {
        if(currentState != null)
        { 
            currentState.OnExecute(this);          
        }
        CheckSight();
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new PatrolState());
    }

    private void CheckSight()
    {
        
        
        if (!isMoving)
        {   
            isMovingDuring = false;
            if (CheckEnemy(out Vector3 enemyPosition))
            {
                Vector3 directBot =(enemyPosition - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(directBot);
                ChangeAnim(Constant.ANIM_ATTACK);
                if(canShoot)
                {
                    canShoot = false;
                    StartCoroutine(ShootCoroutine(0.2f));
                }
            }
            
        }
        else
        {
            isMovingDuring = true;
        }
    }

    private bool CheckEnemy(out Vector3 position)
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

    private void Shoot(WeaponType weaponType)
    {
        BulletBase bullet = SimplePool.Spawn<BulletBase>(GetTypeWeapon(weaponType), spawnBullet.position, transform.rotation);
        bullet.DirectToBot = transform.forward;
        bullet.character = this;
        bulletAvailable = false;
        Vector3 initialPosition = bullet.transform.position; // Lấy vị trí ban đầu của đạn
        if (Vector3.Distance(initialPosition, bullet.transform.position) > radius)
        {

            bullet.OnDespawn();
            bulletAvailable = true;
        }
    }

    private IEnumerator ShootCoroutine(float delaytime)
    {
        yield return new WaitForSeconds(delaytime);
        if (bulletAvailable && !isMovingDuring)
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



    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    void OnDrawGizmos()
    {
        // Vẽ hình cầu để hiển thị vùng không gian
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
