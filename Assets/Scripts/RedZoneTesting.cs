using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZoneTesting : MonoBehaviour
{
    public float collapsingSpeed;
    public float perodicically;
    public float linearInterpolating;

    private void Start()
    {
        if(perodicically == 1)
        {
            StartCoroutine(perodicCollapse(10f));
        }
    }

    private void Update()
    {
        if(transform.localScale.x > 10 && perodicically != 1f)
        transform.localScale = new Vector3(transform.localScale.x - collapsingSpeed, transform.localScale.y, transform.localScale.z - collapsingSpeed);

        if(linearInterpolating == 1)
        {
            Vector3 initialScale = transform.localScale;
            Vector3 newScale = new Vector3(transform.localScale.x - 10f, transform.localScale.y, transform.localScale.z - 10f);
            transform.localScale = Vector3.Lerp(initialScale, newScale, 0.0001f);
        }
    }

    IEnumerator perodicCollapse(float perodicCollapseSpeed)
    {
        if(transform.localScale.x > 10)
        {
            //Vector3 initialScale = transform.localScale;
            //Vector3 newScale = new Vector3(transform.localScale.x - perodicCollapseSpeed, transform.localScale.y, transform.localScale.z - perodicCollapseSpeed);
            //transform.localScale = Vector3.Lerp(initialScale, newScale, 0.1f);

            linearInterpolating = 1f;
        }

        yield return new WaitForSeconds(perodicCollapseSpeed);
        linearInterpolating = 0f;
        yield return new WaitForSeconds(perodicCollapseSpeed);
        StartCoroutine(perodicCollapse(perodicCollapseSpeed));
    }
}
