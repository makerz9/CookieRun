using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlatformTouch : MonoBehaviour
{
    [SerializeField] private bool isTouch;
    
    GameManager gameManager;
    private void OnEnable()
    {
        isTouch = false;
    }

    void Start()
    {
        
        GameObject findgo = GameObject.FindWithTag("GameController");
        gameManager = findgo.GetComponent<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTouch)
        {
            isTouch = true;
            gameManager.ScoreUp();
        }
    }



}
