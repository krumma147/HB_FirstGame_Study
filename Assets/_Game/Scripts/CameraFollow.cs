using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    //Vtri tuong doi cua target va camera
    public Vector3 offset;
    public float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        target = FindAnyObjectByType<Player>().transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);

    }
}
