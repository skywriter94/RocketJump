﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float moveDamping = 15.0f;
    public float rotateDamping = 10.0f;
    public float distance = 5.0f;
    public float height = 4.0f;
    public float targetOffset = 2.0f;

    private Transform tr;


    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height);
        tr.position = Vector3.Slerp(tr.position, camPos, Time.deltaTime * moveDamping);
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);
        tr.LookAt(target.position + (target.up * targetOffset));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);
        Gizmos.DrawLine(target.position + (target.up * targetOffset), transform.position);
    }
}
