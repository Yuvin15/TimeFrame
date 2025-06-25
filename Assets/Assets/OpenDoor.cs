using System;
using System.Threading;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public bool openDoor =  false;
    public bool closeDoor =  true;
    public int timerToClose =  2;

    private void OnTriggerEnter(Collider objectThrow)
    {
        if(objectThrow.CompareTag("ThrowObjects")) 
        { 
            if(closeDoor == true) 
            {
                Debug.Log("Open Door");
                transform.position = new Vector3(0, 10, 14);

            }
        }
    }

    public void CloseDoor()
    {
        transform.position = new Vector3(0, 4, 14);
    }
}
