using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleTarget : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, 0.001f);
    }
}
