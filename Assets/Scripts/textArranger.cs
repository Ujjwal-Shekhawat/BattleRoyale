using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textArranger : MonoBehaviour
{
    public GameObject[] children;
    public float yDecrement = 45f;

    private void Start()
    {
        foreach(GameObject child in children)
        {
            child.transform.localPosition = new Vector3(0, 0 - yDecrement, 0);
            yDecrement += 20;
        }
    }
}
