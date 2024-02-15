using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
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

    public Vector3 startPos;
    public State curState; //������ ���� ���� Ȯ�� ���� ���� 
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
        playerTransform = GameObject.FindWithTag("Player").transform; //ĳ�� ��� ����
        //�׻� ���� ������ start���� ����ϰ� ���� 
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
        private float findRange = 5; //ĸ��ȭ �ܺ� CLASS���� �ǵ� �� ���� 

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
            // �ƹ��͵� ���� 
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






   /* void IdleUpdate()  // �̱��� idle ������ �� �ؾ� �� �ൿ�鸸 ��� �ִ� �Լ� 
    {
        // idle ���¿��� �ϴ� �� x ���¸� ��������� 

        if(Vector2.Distance(playerTransform.position,transform.position)<findRange)
        {
            curState = State.Trace; // ������ ���� trace ���·� ���� 
        }
            //player�� ��ġ�� �ڽ��� ��ġ�� ������ �Ÿ��� ���� 
    }
    void TraceUpdate()
    {
        Vector3 dir = (playerTransform.position - transform.position).normalized; //���� ���ϱ� ����ȭ
        // ������ - ����� �� ����ȭ�� ������ ���� �� �ִ�. 
        transform.Translate(dir*moveSpeed*Time.deltaTime);
        //���� --> �� �������� ������ 

        //�Ÿ��� �־����� ���� ���� �� ���ϻ��·� ���� �ʿ� 
        if (Vector2.Distance(playerTransform.position, transform.position) > findRange)
        {
            curState = State.Return;
        }

        *//*if (Vector2.Distance(transform.position, playerTransform.position) < 0.01f)
        {
            curState = State.Idle;
        }*//* // player�� ������ �� �ٵ�ٵ� ���� ���� ��� �ذ��ؾ� ����? 
        //player���� ���� �̺�Ʈ�� ����������?

    }

    void ReturnUpdate()
    {
        Vector3 dir = (startPos - transform.position).normalized;
        // ���� ������ ��������(start) �� ������ ���ϰ� 
        transform.Translate (dir*moveSpeed*Time.deltaTime); //������ �ӵ��� �̵� 

        if(Vector2.Distance(transform.position , startPos)<0.01f) //�������� �ٵ�ٵ� ���� ���� �ذ��ؾ��� 
            // ==�� �Ἥ �Ȱ��ٰ� �ϸ� �ȵȴ�. 
        {
            curState = State.Idle; //���� ��ġ�� ���ƿ��� �ٽ� idle ���·� ���� 
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
