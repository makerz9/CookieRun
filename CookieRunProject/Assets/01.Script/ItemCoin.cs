
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    [SerializeField] int price=1;
    [SerializeField] GameObject image;
    [SerializeField] GameObject particle;

    GameManager gameManager;
    PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gameManager = gm.GetComponent<GameManager>();

        GameObject play = GameObject.FindWithTag("Player");
        playerController = play.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        image.SetActive(true);
        particle.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManager.CoinGet(price);
            image.SetActive(false);
            particle.SetActive(true);
            playerController.CurHp += (price * 5);
        }
    }

}
