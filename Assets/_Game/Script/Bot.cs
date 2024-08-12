using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    protected override void Update()
    {
        base.Update();
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

        // Sử dụng coroutine để kiểm tra vị trí của đạn mỗi frame
        StartCoroutine(CheckBulletDistance(bullet));
    }

    private IEnumerator CheckBulletDistance(BulletBase bullet)
    {

        while (Vector3.Distance(spawnBullet.position, bullet.transform.position) < (radius - 1f))
        {
            yield return null;
        }
        bullet.OnDespawn();

        bulletAvailable = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.WeaponTag))
        {
            ChangeAnim(Constant.ANIM_DIE);           
            Invoke(nameof(DespawnWithDelay), 1f);
        }
    }

    private void DespawnWithDelay()
    {
        LevelManager.Ins.DespawnBot(this);
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
