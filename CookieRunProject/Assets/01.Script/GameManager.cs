using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int coinAmount=0;
    private float maxhp;
    private float hp;

    [SerializeField] TMP_Text coinTXT;

    [SerializeField] Image hpImage;

    public bool isGameOver;

    [SerializeField] GameObject gameOverUI;

    void Awake()
    {
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isGameOver)
        {
            SceneManager.LoadScene("CookieRun");
        }
    }



    public void CoinGet(int value)
    {
        coinAmount += value;
        coinTXT.text = $"coin: {coinAmount}";
    }

    public void HPSet(float value)
    {
        maxhp = value;
        hp = maxhp;
        if(hpImage != null) hpImage.fillAmount = 1f;
    }

    public void HPChange(float value)
    {
        hp = value;
        hpImage.fillAmount = (float)hp / maxhp; 


    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        isGameOver = true;
    }

}
