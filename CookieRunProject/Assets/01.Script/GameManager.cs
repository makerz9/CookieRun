using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int coinAmount=0;
    private int score=0;
    private float maxhp;
    private float hp;

    [Header("UI")]
    [SerializeField] TMP_Text coinTXT;
    [SerializeField] TMP_Text scoreTXT;

    [SerializeField] Image hpImage;

    [Header("GameOver")]
    public bool isGameOver;

    [SerializeField] GameObject gameOverUI;

    void Awake()
    {
        gameOverUI.SetActive(false);
    }

    void Update()
    {
        //게임오버 재시작
        if(Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene("CookieRun");
        }
    }


    //코인 획득 원본값 수정 방지를 위해
    //private 변수값과 public 함수로 처리
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

    public void ScoreUp()
    {
        score++;
        scoreTXT.text = $"score: {score}";
    }

}
