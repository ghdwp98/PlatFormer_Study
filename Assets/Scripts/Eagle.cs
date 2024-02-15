using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

/*Vector3 playerPos=playerTransform.position;
         /* 움직이는 palyer를 update에서 추적 
           update 에서 단순반복 행동
           거리에 따라 벡터의 크기가 달라짐 --> 속도가 달라짐 
           방향을 알고싶은 경우 -> 벡터정규화 normalized 사용

         크기를 알고 싶은 경우 magnitude
         float scale = (playerPos - transform.position).magnitude;

         dir*scale == (playerPos - transform.position) 
         transform 자체가  vector3를 이용하기 때문에 vector3 이용 
         2d 에서는 z좌표를 미이용 하면 된다. 

        if ((playerPos - transform.position).magnitude < findRange)
        {
            Vector3 dir = (playerPos - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
        }

        독수리가 player를 쫓아오도록 + 무한하게 쫓아오지 않도록 사정거리를 구해줘야함 
        but--->  update에서 여러가지 기능을 한 번에 구현하기에는 너무 복잡하다!!
        상태를 만들어줘야함 --> 유한상태머신 */


public class Eagle : Monster
{
    public enum State // 열거형 : 상태 
    {
        Idle,
        Trace,
        Return,
        Died
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private Vector3 startPos;
    private State curState; //몬스터의 현재 상태 확인 위한 변수 
    private Transform playerTransform;
    
    void Start()
    {
        curState=State.Idle; 
        playerTransform = GameObject.FindWithTag("Player").transform; //캐싱 기법 공부
        //항상 쓰는 변수는 start에서 계산하고 진입 
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
