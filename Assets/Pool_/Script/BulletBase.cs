using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : GameUnit
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed = 5;
    public Vector3 DirectToBot;
    public Character character;
    private void Update()
    {
        //Invoke(nameof(OnDespawn), 0.5f);        
        rb.velocity = DirectToBot * speed;
    }
    public void OnInit()
    {
        
       
    }

    public void OnDespawn()
    {
        
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.CharacterTag) /*&& other.gameObject != character.gameObject*/)
        {
            OnDespawn();
            character.bulletAvailable = true;
        }
    }

}
