using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follower : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;
    [SerializeField] Vector3 offset;

    private void FixedUpdate()
    {
        TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.fixedDeltaTime * 5f);
    }
}
