using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuanSong : MonoBehaviour
{
    public Vector3 Move2Pos;

    // Update is called once per frame
    void Update()
    {
        if (PlayerIn && Input.GetKeyDown(KeyCode.W))
        {
            PlayerController.Inst.transform.position = Move2Pos;
        }   
    }

    public bool PlayerIn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerIn = false;
        }
    }
}
