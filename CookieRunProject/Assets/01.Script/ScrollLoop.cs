using UnityEngine;

public class ScrollLoop : MonoBehaviour
{
    public float width = 20.48f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -width)
        {
            transform.position += new Vector3(width *2, 0, 0);
        }

    }
}
