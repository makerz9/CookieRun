using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float backSpeed = 8;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    GameManager gameManager;
    void Start()
    {
        GameObject gm = GameObject.FindWithTag("GameController");
        gameManager = gm.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameManager.isGameOver == true)
        {
            return;
        }
        transform.Translate(Vector3.left * backSpeed * Time.deltaTime, Space.World);
    }
}
