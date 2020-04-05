using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform player;
    public float X_Rotation;
    public float Y_Rotation;
    public float transformHeight;
    public float distanceFromPlayer;
    public Vector3 offsetPosition;

    private void Start()
    {
        //camera = Camera.main.gameObject;

        transformHeight = 10f;
        offsetPosition = new Vector3(10f, 10f, 10f);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) Look();

        if(player != null && player.gameObject.activeSelf != false)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position + offsetPosition, 5f);
            transform.LookAt(player);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transformHeight, transform.position.z), 0.01f);
        }

        if(Input.GetKeyDown("space"))
        {
            player = null;
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transformHeight, transform.position.z), 0.01f);
        }

        if(Input.GetMouseButton(0))
        {
            ShootRayCast();
        }
    }

    void ShootRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            if(hit.transform != null)
            {
                player = hit.transform;
            }
        }
    }

    void Look()
    {
        float Mouse_X = Input.GetAxis("Mouse X") * 100f / Time.timeScale * Time.deltaTime;
        float Mouse_Y = Input.GetAxis("Mouse Y") * 100f / Time.timeScale * Time.deltaTime;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(inputX, 0, inputY);

        transform.Translate(move * Time.deltaTime * 10f);

        X_Rotation -= Mouse_Y;
        X_Rotation = Mathf.Clamp(X_Rotation, -90f, 90f);

        Y_Rotation += Mouse_X;

        transform.localRotation = Quaternion.Euler(X_Rotation, Y_Rotation, 0f);
    }
}
