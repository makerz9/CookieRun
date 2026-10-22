using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlatformTouch : MonoBehaviour
{
    //플레이어 점수 획득용 스크립트
    [SerializeField] private bool isTouch;
    
    GameManager gameManager;
    private void OnEnable()
    {
        //다시 생성될때 점수얻기 기능을위해 초기화
        isTouch = false;
    }

    void Start()
    {
        
        GameObject findgo = GameObject.FindWithTag("GameController");
        gameManager = findgo.GetComponent<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //플레이어와 충돌한 동시에 isTouch가 false일때만 점수 증가
        if (collision.gameObject.CompareTag("Player") && !isTouch)
        {
            //중복 방지 위해 true로 바꿈
            isTouch = true;
            gameManager.ScoreUp();
        }
    }



}
