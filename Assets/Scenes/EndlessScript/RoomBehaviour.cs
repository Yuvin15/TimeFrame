using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up, 1 - Down, 2 - Right, 3 - Left
    public GameObject[] doors;
        
    public void UpdateRoom(bool[] roomStatus)
    {
        for (int i = 0; i < roomStatus.Length; i++)
        {
            doors[i].SetActive(roomStatus[i]);
            walls[i].SetActive(!roomStatus[i]);
        }
    }
}
