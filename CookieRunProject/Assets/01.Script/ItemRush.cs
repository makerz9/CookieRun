using UnityEngine;

public class ItemRush : MonoBehaviour
{
    ItemManager itemManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject play = GameObject.FindWithTag("Player");
        itemManager = play.GetComponent<ItemManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            itemManager.RushItemEffect();
        }
    }
}
