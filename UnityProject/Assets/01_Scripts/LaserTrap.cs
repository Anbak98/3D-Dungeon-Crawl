using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public LayerMask target;

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, 1f, target))
        {
            Debug.Log("Hello there");
        }
    }
}
