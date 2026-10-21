using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSource;    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    //인덱스와 이름으로 찾기 나중에는 헷갈릴 위험 존재
    //한계가 명확함
    //딕셔너리등 확실한 방법으로 개선 필요
    public void SoundPlay(string name)
    {
        if(name == "SEJump")
        {
            audioSource[0].Play();
        }
        else if (name == "SEDie")
        {
            audioSource[1].Play();
        }
        else if (name == "SECoin")
        {
            audioSource[2].Play();
        }

    }
}
