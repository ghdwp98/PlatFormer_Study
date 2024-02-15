using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

/*Vector3 playerPos=playerTransform.position;
         /* �����̴� palyer�� update���� ���� 
           update ���� �ܼ��ݺ� �ൿ
           �Ÿ��� ���� ������ ũ�Ⱑ �޶��� --> �ӵ��� �޶��� 
           ������ �˰���� ��� -> ��������ȭ normalized ���

         ũ�⸦ �˰� ���� ��� magnitude
         float scale = (playerPos - transform.position).magnitude;

         dir*scale == (playerPos - transform.position) 
         transform ��ü��  vector3�� �̿��ϱ� ������ vector3 �̿� 
         2d ������ z��ǥ�� ���̿� �ϸ� �ȴ�. 

        if ((playerPos - transform.position).magnitude < findRange)
        {
            Vector3 dir = (playerPos - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }

        �������� player�� �Ѿƿ����� + �����ϰ� �Ѿƿ��� �ʵ��� �����Ÿ��� ��������� 
        but--->  update���� �������� ����� �� ���� �����ϱ⿡�� �ʹ� �����ϴ�!!
        ���¸� ���������� --> ���ѻ��¸ӽ� */


public class Eagle : Monster
{
    public enum State // ������ : ���� 
    {
        Idle,
        Trace,
        Return,
        Died
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private Vector3 startPos;
    private State curState; //������ ���� ���� Ȯ�� ���� ���� 
    private Transform playerTransform;
    
    void Start()
    {
        curState=State.Idle; 
        playerTransform = GameObject.FindWithTag("Player").transform; //ĳ�� ��� ����
        //�׻� ���� ������ start���� ����ϰ� ���� 
        startPos = transform.position;
        
    }

    void Update()
    {
        switch(curState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Trace:
                TraceUpdate(); 
                break;
            case State.Return:
                ReturnUpdate();
                break;
            case State.Died:
                DiedUpdate();
                break;
        }






    }

    void IdleUpdate()
    {

    }
    void TraceUpdate()
    {

    }
    void ReturnUpdate()
    {

    }
    void DiedUpdate()
    {

    }

   


}
