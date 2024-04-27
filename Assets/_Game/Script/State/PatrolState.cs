using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    private float patrolDuration = 3f;
    private float patrolTime;
    public void OnEnter(Bot bot)
    {
        bot.walkPointSet = false;
        patrolTime = 0;
        Patrol(bot);
    }

    public void OnExecute(Bot bot)
    {
       Patrol(bot);
       patrolTime += Time.deltaTime;
        if(patrolTime >= patrolDuration)
        {
            bot.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

    private void Patrol(Bot bot)
    {
        if(!bot.walkPointSet)
        {
            SearchWalkPoint(bot);
        }
        else
        {
            bot.agent.SetDestination(bot.walkPoint);           
            bot.isMoving = true;
            bot.ChangeAnim(Constant.ANIM_RUN);
            if (Vector3.Distance(bot.transform.position, bot.walkPoint) < .1f )
            {
                bot.walkPointSet = false; 
            }

            //if(bot.agent.velocity.magnitude > 0.1f)
            //{
            //    bot.isMoving = true;
            //    bot.ChangeAnim(Constant.ANIM_RUN);
            //}       
            //else
            //{
            //    bot.isMoving = false;
            //    bot.ChangeAnim(Constant.ANIM_IDLE);
            //}
        }
    }

    private void SearchWalkPoint(Bot bot)
    {        
        float randomX = Random.Range(-bot.walkPointRange, bot.walkPointRange);
        float randomZ = Random.Range(-bot.walkPointRange, bot.walkPointRange);

        bot.walkPoint  = new Vector3(bot.transform.position.x + randomX,bot.transform.position.y, bot.transform.position.z + randomZ);

        bot.agent.SetDestination(bot.walkPoint);
        bot.walkPointSet =true;
    }
}
