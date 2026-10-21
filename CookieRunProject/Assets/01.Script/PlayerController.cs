using System;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //점프 횟수
    //점프력
    //체력
    //애니메이터
    //리지드바디
    //콜라이더

    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJumpCount;
    [SerializeField] private int jumpPower=8;

    Animator animator;
    Rigidbody2D rigid;

    CapsuleCollider2D capsule;

    [SerializeField] private bool grounded = true;
    [SerializeField] private bool sliding = false;



    [SerializeField] private float curHp;
    private float maxHp=100;

    GameManager gameManager;

    public float CurHp
    {
        get
        {
            return curHp;
        }

        set
        {
             if (curHp <= 0 && value <= 0) return; 
            curHp = math.clamp(value, 0f, maxHp);
            
            Debug.Log(curHp);
            
            if(curHp <=0)
            {
                //사망 구현
                Dead();
                gameManager.GameOver();
                //return;
            }
            gameManager.HPChange(curHp);
        }
    }

    void Awake()
    {   
        GameObject gm = GameObject.FindWithTag("GameController");
        gameManager = gm.GetComponent<GameManager>();
        //기본 설정 구현
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        CurHp = maxHp;

        gameManager.HPSet(maxHp);
    }

    void Start()
    {
        maxJumpCount = 2;
        //초기화 로직
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        
        //점프
        if(Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            Jump();
        }

        //슬라이드
        if(Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            OnSlide();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && grounded)
        {
            OffSlide();
        }

        CurHp -= Time.deltaTime;

    }

//점프 기능
    #region JumpFunction
    private void Jump()
    {
        grounded = false;
        rigid.linearVelocity = new Vector2(rigid.linearVelocityX, jumpPower);
        jumpCount--;
        animator.SetBool("isGrounded", false);
        animator.SetBool("isSlide", false);
        NormalCollider();

    }


    //땅 충돌
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
            jumpCount = maxJumpCount;
            animator.SetBool("isGrounded", true);
        }

        if(collision.gameObject.CompareTag("Upper"))
        {
            jumpCount++;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Dead"))
        {
            TakeDamage(50f);
            Vector3 newPosition = transform.position;
            newPosition.y += 20f;
            transform.position = newPosition;
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(20f);
            
        }

        if(collision.gameObject.CompareTag("Upper"))
        {
            jumpCount++;
        }
    }

    //떨어짐
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
            animator.SetBool("isGrounded", false);
            animator.SetBool("isSlide", false);
            NormalCollider();
        }

        if(collision.gameObject.CompareTag("Upper"))
        {
            jumpCount = Math.Max(jumpCount - 1, 0);
            
        }
    }

#endregion

//슬라이드 기능
    #region SlideFunction

    private void OnSlide()
    {

        capsule.offset = new Vector2(0, -0.5f);
        capsule.size = new Vector2(1.25f, 0.6f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        
        animator.SetBool("isSlide", true);
    }
    private void OffSlide()
    {
        //원래 콜라이더로 돌아올일 쓸떼 많아 함수로 분리;
        NormalCollider();

        animator.SetBool("isSlide", false);
    }

    private void NormalCollider()
    {
        capsule.offset = new Vector2(0, -0.1f);
        capsule.size = new Vector2(0.6f, 1.25f);
        capsule.direction = CapsuleDirection2D.Vertical;
    }

#endregion

//데미지 기능
    #region DamageFunction

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
        CurHp -= damage;  
    }

    #endregion


//사망기능
    #region DeadFunction

    private void Dead()
    {
        rigid.simulated = false;
        
        animator.SetTrigger("isDie");
        gameManager.isGameOver = true;
    }

    #endregion



}
