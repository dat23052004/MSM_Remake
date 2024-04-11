using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : GunBase
{

    private void Update()
    {
        skin.Rotate(Vector3.forward, Time.deltaTime * 600f);
    }
    public override void Shoot()
    {
        base.Shoot();
        
    }

}
