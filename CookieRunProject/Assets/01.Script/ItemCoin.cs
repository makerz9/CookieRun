
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    [SerializeField] int price=1;
    [SerializeField] GameObject image;
    [SerializeField] GameObject particle;

    GameManager gameManager;
    SoundManager soundManager;
    PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gameManager = gm.GetComponent<GameManager>();

        GameObject sm = GameObject.FindWithTag("SoundController");
        soundManager = sm.GetComponent<SoundManager>();

        GameObject play = GameObject.FindWithTag("Player");
        playerController = play.GetComponent<PlayerController>();
    }

    //먹으면 이미지가 사라지기 때문에
    //다시 생성되면 이미지와 파티클 초기화
    private void OnEnable()
    {
        //초기화 스크립트
        image.SetActive(true);
        particle.SetActive(false);
    }

    

    //아이템 효과는 아이템에서 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.CoinGet(price);
            image.SetActive(false);
            particle.SetActive(true);
            playerController.CurHp += (price * 5);
            soundManager.SoundPlay("SECoin");
        }
    }

}
