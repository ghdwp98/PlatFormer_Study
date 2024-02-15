using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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

    public Vector3 startPos;
    public State curState; //몬스터의 현재 상태 확인 위한 변수 
    public Transform playerTransform;

    StateMachine fsm;

    IdleState idleState;
    TraceState traceState;
    ReturnState returnState;



    private void Awake()
    {
        fsm = new StateMachine();
        idleState = new IdleState(this);
        traceState = new TraceState(this);
        returnState=new ReturnState(this);
    }

    void Start()
    {
        fsm.SetInitState(idleState);
        curState=State.Idle; 
        playerTransform = GameObject.FindWithTag("Player").transform; //캐싱 기법 공부
        //항상 쓰는 변수는 start에서 계산하고 진입 
        startPos = transform.position;
        
    }

    void Update()
    {
        fsm.Update();
    }

    public void ChangeState(string stateName)
    {
        switch(stateName)
        {
            case "Idle":
                fsm.ChangeState(idleState);
                    break;
            case "Trace":
                fsm.ChangeState(traceState);
                break;
            case "Return":
                fsm.ChangeState(returnState);
                break;
        }
    }


    public class IdleState:IState
    {
        private Eagle owner;
        private Transform playerTransform;
        private float findRange = 5; //캡슐화 외부 CLASS에서 건들 수 없음 

        public IdleState(Eagle owner)
        {
            this.owner = owner;
            
        }

        public void Enter()
            
        {
            Debug.Log("idle enter");
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        public void Update()
        {
            // 아무것도 안함 
            if (Vector2.Distance(playerTransform.position, owner.transform.position) < findRange)
            {
                owner.ChangeState("Trace");
            }
        }
        public void Exit()
        {

        }
    }

    public class TraceState:IState
    {
        Eagle owner;
        Transform playerTransform;
        float moveSpeed = 2;
        float findRange = 5;

        public TraceState(Eagle owner)
        {
            this.owner = owner;
        }

        public void Enter()
        {
            Debug.Log("trace enter");
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
        public void Update()
        {
            Vector3 dir = (playerTransform.position - owner.transform.position).normalized; 
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime);
            
            if (Vector2.Distance(playerTransform.position, owner.transform.position) > findRange)
            {
                owner.ChangeState("Return");
            }
        }

        public void Exit()
        {

        }
    }
    private class ReturnState :IState
    {
        Eagle owner;
        float moveSpeed = 10;
        public ReturnState(Eagle owner)
        {
            this.owner = owner;
            
        }
        public void Enter()
        {
            Debug.Log("return enter");
        }
        public void Update()
        {
            Vector3 dir = (owner.startPos - owner.transform.position).normalized;
            
            owner.transform.Translate(dir * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(owner.transform.position, owner.startPos) < 0.01f)
            {
                owner.ChangeState("Idle");
            }
        }
        public void Exit()
        {

        }
    }






   /* void IdleUpdate()  // 이글이 idle 상태일 때 해야 할 행동들만 담고 있는 함수 
    {
        // idle 상태에서 하는 것 x 상태를 변경시켜줌 

        if(Vector2.Distance(playerTransform.position,transform.position)<findRange)
        {
            curState = State.Trace; // 가까이 오면 trace 상태로 변경 
        }
            //player의 위치와 자신의 위치를 가지고 거리를 구함 
    }
    void TraceUpdate()
    {
        Vector3 dir = (playerTransform.position - transform.position).normalized; //방향 구하기 정규화
        // 도착지 - 출발지 의 정규화로 방향을 구할 수 있다. 
        transform.Translate(dir*moveSpeed*Time.deltaTime);
        //추적 --> 그 방향으로 움직임 

        //거리가 멀어지면 추적 중지 후 리턴상태로 변경 필요 
        if (Vector2.Distance(playerTransform.position, transform.position) > findRange)
        {
            curState = State.Return;
        }

        *//*if (Vector2.Distance(transform.position, playerTransform.position) < 0.01f)
        {
            curState = State.Idle;
        }*//* // player와 겹쳤을 때 바들바들 떠는 문제 어떻게 해결해야 할지? 
        //player와의 접촉 이벤트로 가려버리기?

    }

    void ReturnUpdate()
    {
        Vector3 dir = (startPos - transform.position).normalized;
        // 현재 지점과 원래지점(start) 의 방향을 구하고 
        transform.Translate (dir*moveSpeed*Time.deltaTime); //정해진 속도로 이동 

        if(Vector2.Distance(transform.position , startPos)<0.01f) //독수리가 바들바들 떠는 문제 해결해야함 
            // ==를 써서 똑같다고 하면 안된다. 
        {
            curState = State.Idle; //원래 위치로 돌아오면 다시 idle 상태로 변경 
        }

        if(Vector2.Distance(transform.position,playerTransform.position)<findRange)
        {
            curState= State.Trace;
        }
    }

    void DiedUpdate()
    {

    }*/

   


}
