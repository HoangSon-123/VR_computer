using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRMovement : MonoBehaviour
{
    [Header("XR Interaction")]
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Transform customAttachPoint;

    private Vector3 resetPosition;
    private Quaternion resetRotation;
    private bool hasInitialized = false;

    private void Start()
    {
        // Gán attach point nếu có
        if (grabInteractable != null && customAttachPoint != null)
        {
            grabInteractable.attachTransform = customAttachPoint;
        }

        // Lưu vị trí/góc ban đầu một lần duy nhất
        InitializeResetValuesOnce();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main"))
        {
            Debug.Log("Object vào vùng Main → Reset vị trí");

            // Reset lại vị trí/góc ban đầu
            transform.position = resetPosition;
            transform.rotation = resetRotation;
        }
    }

    private void InitializeResetValuesOnce()
    {
        if (hasInitialized) return;

        resetPosition = transform.position;
        resetRotation = transform.rotation;
        hasInitialized = true;

        Debug.Log("Vị trí và rotation gốc đã được lưu tại Start().");
    }
}
