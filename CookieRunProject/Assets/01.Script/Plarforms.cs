using UnityEngine;

public class Plarforms : MonoBehaviour
{    [SerializeField] GameObject[] platform;
    public float width = 20.48f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        New();
    }

    void Update()
    {
        if(transform.position.x <= -width)
        {
            foreach(var plat in platform)
            {
                plat.SetActive(false);
            }
            New();
        }

    }

    private void New()
    {

        Debug.Log("초기화");

        int platRan = Random.Range(0, platform.Length);
        int posRan = Random.Range(-3, 0);

        //Instantiate(platform[platRan], new Vector3(transform.position.x, posRan, 0), platform[platRan].transform.rotation);
        platform[platRan].SetActive(true);
    }
}
