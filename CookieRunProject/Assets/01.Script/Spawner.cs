
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] GameObject spawnPoint;

    [SerializeField] int spawnCount;

    //[SerializeField] float interval; // 생성 시간
    [SerializeField] float spawnTime = 0; // 생성 시간
    [SerializeField] float spawnDelay = 3; // 생성 

    
    void Start()
    {
        Spawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > spawnDelay && spawnCount > 0)
        {
            Spawn();
            spawnTime = 0;
            spawnCount--;
        }
        
        
    }


    private void Spawn()
    {
        int posRan = Random.Range(-3, 0);

        Instantiate(platform, new Vector3(spawnPoint.transform.position.x, posRan, 0), platform.transform.rotation);
    }
}
