using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Monster
{
    private Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform; //ĳ�� ��� ����
    }

    void Update()
    {
        
    }
}
