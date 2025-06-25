using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] 
    private int requiredWeight = 7;

    [SerializeField] 
    private bool isActivated = false;

    private Rigidbody pressurePlateObject;
    private Rigidbody currentObject;

    public OpenDoor openDoor;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.mass >= requiredWeight && !isActivated)
        {
            Debug.Log("Plate activated");
            openDoor.CloseDoor();
        }
    }
}
