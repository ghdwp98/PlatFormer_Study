using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

    //타일맵 땅 바닥에 collider 추가 composite collider --> rigid 추가 후 xyz 제약 걸어주기 
{
    //inspector 창에서 debug모드로 private변수들도 제대로 변화되고 있는지 확인이 가능하다. 
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid; //private 변수도 serial로 외부에서 할당 가능 (직렬화)
    [SerializeField] SpriteRenderer render; //스프라이트 렌더러 
    [SerializeField] Animator animator; //애니메이션 

    [Header("Property")]
    
    [SerializeField] float breakPower;
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    [SerializeField] float jumpSpeed;

    [SerializeField] LayerMask groundCheckLayer;
    private bool isGround; //땅 체크 --> 연속 점프 방지 


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
        else if (moveDir.x==0&&rigid.velocity.x>0.1f) 
            //움직이지 않거나  
        {
            //움직이는 반대의 힘으로 이동 (브레이크 )
            rigid.AddForce(Vector2.left * breakPower);
        }
        else if(moveDir.x==0&&rigid.velocity.x<-0.1f)
        {
            rigid.AddForce(Vector2.right*breakPower);
        }
        
        if(rigid.velocity.y<-maxYSpeed)
        {
            Vector2 velocity=rigid.velocity; 
            velocity.y = -maxYSpeed;     //y의 값만 바꾸기 위한 방법 너무 빨리 떨어질 때 통과하는 문제를 해결하기 위해
            rigid.velocity = velocity;
        }

        animator.SetFloat("YSpeed", rigid.velocity.y); //값으로도 애니메이터의 파라메터를 설정해 줄 수 있음 
    }

    private void Jump()
    {
        //Addforce와 veloctiy 방식의 차이 
        Vector2 velocity=rigid.velocity;
        velocity.y = jumpSpeed;
        rigid.velocity = velocity;
    }


    void OnMove(InputValue value)
    {
        moveDir=value.Get<Vector2>(); //2d 이므로 vector2 이용 (-1,1 ) 
        //스프라이트 렌더러의 Flip 변경으로 좌/ 우 바라보는 방향 변경 

        if(moveDir.x<0) //왼쪽 보는 상황
        {
            render.flipX = true;
            animator.SetBool("Run", true);
        }
        else if(moveDir.x>0) //0일 때는 (가만있는 상태) 방향전환 필요없기 때문에 >0 
        {
            render.flipX=false;
            animator.SetBool("Run", true);
        }
        else //움직임이 0 인 상황 
        {
            animator.SetBool("Run", false);
        }
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed&&isGround)
        {
            Jump(); //이단 점프 방지 땅과 닿을 때만 점프
        }
    }
    private int groundCount; //몇 개의 충돌체와 충돌 중 인지 파악 
    //유니티 레이어, 레이어 마스크 충돌 및 둘의 차이  공부 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (((1 << collision.gameObject.layer) & groundCheckLayer) != 0) //밟은 상태 체크 
        {
            Debug.Log("땅 밟음");
        }
*/
        if(groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount++;
            isGround = groundCount>0;
            animator.SetBool("IsGround", isGround);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (((1 << collision.gameObject.layer) & groundCheckLayer) == 0) //밟은 상태 체크 
        {
            Debug.Log("땅 에서 떨어짐");
        }*/
        //유니티 레이어의 레이어마스크 와 비트연산 

        if (groundCheckLayer.Contain(collision.gameObject.layer))
        {
            groundCount--;
            isGround = groundCount>0; //true false 
            animator.SetBool("IsGround", isGround);
        }
        

    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
