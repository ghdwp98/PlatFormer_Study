using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //inspector 창에서 debug모드로 private변수들도 제대로 변화되고 있는지 확인이 가능하다. 
    [SerializeField] Rigidbody2D rigid; //private 변수도 serial로 외부에서 할당 가능 (직렬화)
    [SerializeField] float breakPower;
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    //1시 40분 부터 중지 

    Vector2 moveDir;

    private void FixedUpdate() //리지드바디를 통해 이동 -->fixed 이용 해주기
    {
        Move();
    }
    private void Move()
    {
        if(moveDir.x<0&&rigid.velocity.x>-maxXSpeed) //왼쪽 입력을 하고 있는 상황 x<0
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower); //moveDir의 값이 -1 또는 1임 (누르는 키에 따라)
        }
        else if(moveDir.x>0&&rigid.velocity.x<maxXSpeed) //최고속도에 다다르면 더이상 빨라지지 않도록 
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower); 
        }
        else if (moveDir.x==0&&rigid.velocity.x>0) 
            //움직이지 않거나  
        {
            //움직이는 반대의 힘으로 이동 (브레이크 )
            rigid.AddForce(Vector2.left * breakPower);
        }
        else if(moveDir.x==0&&rigid.velocity.x<0)
        {
            rigid.AddForce(Vector2.right*breakPower);
        }
        
        if(rigid.velocity.y<-maxYSpeed)
        {
            Vector2 velocity=rigid.velocity; 
            velocity.y = -maxYSpeed;     //y의 값만 바꾸기 위한 방법 너무 빨리 떨어질 때 통과하는 문제를 해결하기 위해
            rigid.velocity = velocity;
        }
    }

    void OnMove(InputValue value)
    {
        moveDir=value.Get<Vector2>(); //2d 이므로 vector2 이용 (-1,1 ) 
    }
    void OnJump(in InputValue value)
    {

    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
