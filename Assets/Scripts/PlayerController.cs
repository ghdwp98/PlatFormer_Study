using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

    //Ÿ�ϸ� �� �ٴڿ� collider �߰� composite collider --> rigid �߰� �� xyz ���� �ɾ��ֱ� 
{
    //inspector â���� debug���� private�����鵵 ����� ��ȭ�ǰ� �ִ��� Ȯ���� �����ϴ�. 
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid; //private ������ serial�� �ܺο��� �Ҵ� ���� (����ȭ)
    [SerializeField] SpriteRenderer render; //��������Ʈ ������ 
    [SerializeField] Animator animator; //�ִϸ��̼� 

    [Header("Property")]
    
    [SerializeField] float breakPower;
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    [SerializeField] float jumpSpeed;

    [SerializeField] LayerMask groundCheckLayer;
    private bool isGround; //�� üũ --> ���� ���� ���� 


    Vector2 moveDir;

    private void FixedUpdate() //������ٵ� ���� �̵� -->fixed �̿� ���ֱ�
    {
        Move();
    }
    private void Move()
    {
        if(moveDir.x<0&&rigid.velocity.x>-maxXSpeed) //���� �Է��� �ϰ� �ִ� ��Ȳ x<0
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower); //moveDir�� ���� -1 �Ǵ� 1�� (������ Ű�� ����)
        }
        else if(moveDir.x>0&&rigid.velocity.x<maxXSpeed) //�ְ�ӵ��� �ٴٸ��� ���̻� �������� �ʵ��� 
        {
            rigid.AddForce(Vector2.right * moveDir.x * movePower); 
        }
        else if (moveDir.x==0&&rigid.velocity.x>0.1f) 
            //�������� �ʰų�  
        {
            //�����̴� �ݴ��� ������ �̵� (�극��ũ )
            rigid.AddForce(Vector2.left * breakPower);
        }
        else if(moveDir.x==0&&rigid.velocity.x<-0.1f)
        {
            rigid.AddForce(Vector2.right*breakPower);
        }
        
        if(rigid.velocity.y<-maxYSpeed)
        {
            Vector2 velocity=rigid.velocity; 
            velocity.y = -maxYSpeed;     //y�� ���� �ٲٱ� ���� ��� �ʹ� ���� ������ �� ����ϴ� ������ �ذ��ϱ� ����
            rigid.velocity = velocity;
        }

        animator.SetFloat("YSpeed", rigid.velocity.y); //�����ε� �ִϸ������� �Ķ���͸� ������ �� �� ���� 
    }

    private void Jump()
    {
        //Addforce�� veloctiy ����� ���� 
        Vector2 velocity=rigid.velocity;
        velocity.y = jumpSpeed;
        rigid.velocity = velocity;
    }


    void OnMove(InputValue value)
    {
        moveDir=value.Get<Vector2>(); //2d �̹Ƿ� vector2 �̿� (-1,1 ) 
        //��������Ʈ �������� Flip �������� ��/ �� �ٶ󺸴� ���� ���� 

        if(moveDir.x<0) //���� ���� ��Ȳ
        {
            render.flipX = true;
            animator.SetBool("Run", true);
        }
        else if(moveDir.x>0) //0�� ���� (�����ִ� ����) ������ȯ �ʿ���� ������ >0 
        {
            render.flipX=false;
            animator.SetBool("Run", true);
        }
        else //�������� 0 �� ��Ȳ 
        {
            animator.SetBool("Run", false);
        }
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed&&isGround)
        {
            Jump(); //�̴� ���� ���� ���� ���� ���� ����
        }
    }
    private int groundCount; //�� ���� �浹ü�� �浹 �� ���� �ľ� 
    //����Ƽ ���̾�, ���̾� ����ũ �浹 �� ���� ����  ���� 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (((1 << collision.gameObject.layer) & groundCheckLayer) != 0) //���� ���� üũ 
        {
            Debug.Log("�� ����");
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
        /*if (((1 << collision.gameObject.layer) & groundCheckLayer) == 0) //���� ���� üũ 
        {
            Debug.Log("�� ���� ������");
        }*/
        //����Ƽ ���̾��� ���̾��ũ �� ��Ʈ���� 

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
