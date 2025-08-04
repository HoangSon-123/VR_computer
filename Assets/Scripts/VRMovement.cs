using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class VRMovement : MonoBehaviour
{
    [Header("XR Interaction")]
    [SerializeField] private XRBaseInteractor handInteractor;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private Transform customAttachPoint;

    private bool hasExitedMain = false;
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

        // Lưu vị trí/góc ban đầu đúng 1 lần duy nhất
        InitializeResetValuesOnce();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main") && hasExitedMain)
        {
            if (IsBeingGrabbed())
            {
                Debug.Log("Object đang được cầm và vào lại vùng Main → Reset về vị trí gốc");

                // Set lại vị trí/góc về đúng gốc ban đầu
                transform.position = resetPosition;
                transform.rotation = resetRotation;

                hasExitedMain = false;
            }
            else
            {
                Debug.Log("Vào vùng Main nhưng không đang grab → Không reset");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Main"))
        {
            hasExitedMain = true;
            Debug.Log("Object đã rời vùng Main");
        }
    }

    private bool IsBeingGrabbed()
    {
        if (handInteractor == null) return false;

        var grabbed = handInteractor.firstInteractableSelected;
        if (grabbed == null) return false;

        return grabbed.transform == transform;
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
