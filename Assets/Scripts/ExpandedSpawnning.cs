using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandedSpawnning : MonoBehaviour
{
    public GameObject cubeModel;
    public float spawnSpacingX;
    public float spawnSpacingY;
    public float xPower;
    public float yPower;

    private void Start()
    {
        xPower = staticScript.xPower;
        yPower = staticScript.yPower;

        for (float i = 0f; i < xPower; i += 1f)
        {
            for (float j = 0f; j < yPower; j += 1f)
            {
                Vector3 spawnPosition = new Vector3(i * spawnSpacingX, transform.position.y, j * spawnSpacingY);
                GameObject mainPlayer = Instantiate(cubeModel, spawnPosition, Quaternion.identity);
            }
        }
    }
}
