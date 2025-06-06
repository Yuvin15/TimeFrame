using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    [Header("Height for object")]
    public Vector3 respawnPosition = new Vector3(0, 1, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10f) 
        {
            
            Debug.Log("Fell to -10");
            //transform.position = respawnPosition;

            Vector3 pos = transform.position;
            pos.y = 1;
            transform.position = pos;
        }
        else 
        {
            Debug.Log("No problem");
        }
    }
}
