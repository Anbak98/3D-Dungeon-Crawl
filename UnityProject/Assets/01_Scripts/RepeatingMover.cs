using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Lerp,
    PingPong,
    LerpPingPong,
}

public enum SpaceType
{
    Local,
    World
}

public enum Physic
{
    Transfrom,
    Rigidbody
}

public class RepeatingMover : MonoBehaviour
{
    [Header("Move")]
    public MoveType moveType;
    public float speed = 0.1f;

    [Header("Position")]
    public SpaceType spaceType;
    public Vector3 target;

    private Vector3 pointA;
    private Vector3 pointB;

    private void Start()
    {
        pointA = transform.position;
        pointB = target + transform.position;
    }

    private void Update()
    {
        if (spaceType == SpaceType.Local) 
        {
            if (moveType == MoveType.LerpPingPong)
            {
                float t = Mathf.PingPong(speed * Time.time, 1);
                transform.position = Vector3.Lerp(pointA, pointB, t);
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
