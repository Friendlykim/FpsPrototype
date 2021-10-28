using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("Walk,RunSpeed")]
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
}
