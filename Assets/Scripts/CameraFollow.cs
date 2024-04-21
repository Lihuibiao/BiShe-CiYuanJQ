using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Inst;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    public Vector2 Offset;

    private void Awake()
    {
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update() 
    {
        if (PlayerController.Inst.transform != null)
        {
            Vector3 targetPos = PlayerController.Inst.transform.position;
            targetPos.x = Mathf.Clamp(targetPos.x + Offset.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y + Offset.y, minPosition.y, maxPosition.y);
            targetPos.z = -10;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}