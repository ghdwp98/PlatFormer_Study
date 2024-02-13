using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //inspector â���� debug���� private�����鵵 ����� ��ȭ�ǰ� �ִ��� Ȯ���� �����ϴ�. 
    [SerializeField] Rigidbody2D rigid; //private ������ serial�� �ܺο��� �Ҵ� ���� (����ȭ)
    [SerializeField] float breakPower;
    [SerializeField] float movePower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;
    //1�� 40�� ���� ���� 

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
        else if (moveDir.x==0&&rigid.velocity.x>0) 
            //�������� �ʰų�  
        {
            //�����̴� �ݴ��� ������ �̵� (�극��ũ )
            rigid.AddForce(Vector2.left * breakPower);
        }
        else if(moveDir.x==0&&rigid.velocity.x<0)
        {
            rigid.AddForce(Vector2.right*breakPower);
        }
        
        if(rigid.velocity.y<-maxYSpeed)
        {
            Vector2 velocity=rigid.velocity; 
            velocity.y = -maxYSpeed;     //y�� ���� �ٲٱ� ���� ��� �ʹ� ���� ������ �� ����ϴ� ������ �ذ��ϱ� ����
            rigid.velocity = velocity;
        }
    }

    void OnMove(InputValue value)
    {
        moveDir=value.Get<Vector2>(); //2d �̹Ƿ� vector2 �̿� (-1,1 ) 
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
