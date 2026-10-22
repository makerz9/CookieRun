using System;

using Unity.Mathematics;

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
    //[SerializeField] private bool sliding = false;

    [SerializeField] private bool damaged = false;

    [SerializeField] private float curHp;

    [SerializeField] SpriteRenderer spriteRenderer;
    private float maxHp=100;

    GameManager gameManager;
    SoundManager soundManager;

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

        GameObject sm = GameObject.FindWithTag("SoundController");
        soundManager = sm.GetComponent<SoundManager>();

        spriteRenderer.color = new Color(1f,1f,1f,1f);
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
        //꾹 누를때 자연스럽게 실행되게하기 위해 GetKeyDown -> GetKey
        if(Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            OnSlide();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && grounded)
        {
            OffSlide();
        }

        CurHp -= Time.deltaTime * 2;

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

        //점프할때 기본 콜라이더 강제 변경
        NormalCollider();
        soundManager.SoundPlay("SEJump");

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

        //쿠키런과 어울리지 않는 기능
        // if(collision.gameObject.CompareTag("Upper"))
        // {
               //추가 점프
        //     jumpCount++;
            
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Dead"))
        {
            //구덩이 빠짐
            TakeDamage(50f); //큰 데미지
            Vector3 newPosition = transform.position;
            newPosition.y += 20f;
            //구출시스템 = 빠지면 화면 위에서 다시 내려옴
            transform.position = newPosition;
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            //매개변수로 확장성 설계
            TakeDamage(20f);
            
        }

        // if(collision.gameObject.CompareTag("Upper"))
        // {
        //     jumpCount++;
        // }
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

            //미세한 플랫폼 간 틈에서 생기는 체공 문제 해결하기 위해
            //살짝만 플레이어를 위로 들어서 땅에 착지시켜 상태 초기화
            transform.position = new Vector2(transform.position.x,transform.position.y+0.01f);
        }

        // if(collision.gameObject.CompareTag("Upper"))
        // {
        //     jumpCount = Math.Max(jumpCount - 1, 0);
            
        // }
    }






#endregion

//슬라이드 기능
    #region SlideFunction

    //슬라이드중
    private void OnSlide()
    {

        capsule.offset = new Vector2(0, -0.5f);
        capsule.size = new Vector2(1.25f, 0.6f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        
        animator.SetBool("isSlide", true);
    }

    //슬라이더 종료
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
        if(!damaged)
        {
            Debug.Log(damage);
            CurHp -= damage;
            damaged = true;
        spriteRenderer.color = new Color(1f,1f,1f,0.5f);
            Invoke("OffInvincibility", 1);//1초동안 무적 활성화
        }
    }

    private void OffInvincibility()
    {
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        damaged = false;
    }

    #endregion


//사망기능
    #region DeadFunction

    private void Dead()
    {
        rigid.simulated = false;
        OffInvincibility();
        animator.SetTrigger("isDie");
        gameManager.isGameOver = true;
        soundManager.SoundPlay("SEDie");
    }

    #endregion



}
