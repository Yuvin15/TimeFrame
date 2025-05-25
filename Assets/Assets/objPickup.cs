using UnityEngine;

public class ObjPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform cameraTrans;
    public float throwAmount = 10f;
    public float pickupRange = 3f;

    private Transform currentObject;
    private Rigidbody currentRigidbody;
    private bool pickedUp = false;
    private bool interactable = false;

    void Update()
    {
        if (!pickedUp)
        {
            Ray ray = new Ray(cameraTrans.position, cameraTrans.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                if (hit.transform.CompareTag("ThrowObjects"))
                {
                    crosshair1.SetActive(false);
                    crosshair2.SetActive(true);
                    interactable = true;

                    if (Input.GetMouseButtonDown(0))
                    {
                        currentObject = hit.transform;
                        currentRigidbody = currentObject.GetComponent<Rigidbody>();
                        PickUpObject();
                    }
                }
                else
                {
                    ResetCrosshair();
                }
            }
            else
            {
                ResetCrosshair();
            }
        }

        if (pickedUp)
        {
            if (Input.GetMouseButtonUp(0))
            {
                DropObject();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ThrowObject();
            }
        }
    }

    void PickUpObject()
    {
        currentObject.parent = cameraTrans;
        currentObject.localPosition = new Vector3(0f, 0f, 2f);
        currentRigidbody.useGravity = false;
        currentRigidbody.isKinematic = true;
        currentRigidbody.linearVelocity = Vector3.zero;
        currentRigidbody.angularVelocity = Vector3.zero;
        pickedUp = true;
    }

    void DropObject()
    {
        currentObject.parent = null;
        currentRigidbody.useGravity = true;
        currentRigidbody.isKinematic = false;
        pickedUp = false;
        ResetCrosshair();
    }

    void ThrowObject()
    {
        currentObject.parent = null;
        currentRigidbody.useGravity = true;
        currentRigidbody.isKinematic = false;
        currentRigidbody.linearVelocity = cameraTrans.forward * throwAmount;
        pickedUp = false;
        ResetCrosshair();
    }

    void ResetCrosshair()
    {
        crosshair1.SetActive(true);
        crosshair2.SetActive(false);
        interactable = false;
    }
}
