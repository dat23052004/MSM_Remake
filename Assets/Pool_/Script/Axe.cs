using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : GunBase
{
    
    private void Update()
    {
        skin.Rotate(Vector3.forward, Time.deltaTime * 300f);
    }
    public override void Shoot()
    {
        base.Shoot();
              
    }
}
