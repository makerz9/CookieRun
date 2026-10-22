using UnityEngine;

public class ItemManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    GameManager gameManager;
    PlayerController playerController;

    Platforms platforms;
    void Start()
    {
        GameObject obj = GameObject.FindWithTag("GameController");
        gameManager = obj.GetComponent<GameManager>();

        GameObject play = GameObject.FindWithTag("Player");
        playerController = play.GetComponent<PlayerController>();

        // GameObject[] plat = GameObject.FindGameObjectsWithTag("PlatformBase");
        // plarforms = plat.GetComponent<Plarforms>();
    }

    public void RushItemEffect()
    {
        //무적아이템
        // = 3초동안 맵 속도 두배
        // = 플레이어 무적상태

    }

}
