using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBg : MonoBehaviour
{
    public GameObject InitBg;

    public GameObject SecBg;

    private void Awake()
    {
        InitBg.gameObject.SetActive(true);
        SecBg.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InitBg.gameObject.SetActive(!InitBg.gameObject.activeSelf);
            SecBg.gameObject.SetActive(!SecBg.gameObject.activeSelf);
        }    
    }
}
