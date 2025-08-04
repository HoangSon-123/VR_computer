using UnityEngine;
using UnityEngine.XR;


public class VRObject : MonoBehaviour
{


    [SerializeField] private GameObject objectInMain;
    bool wasGripPressed = false;
    bool wasTriggerPressed = false;


    #region UNITY
    private void Start()
    {
        objectInMain.SetActive(false);
    }

    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool gripPressed = false;
        bool triggerPressed = false;

        device.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);

        // Detect grip release
        if (wasGripPressed && !gripPressed)
        {
            Debug.Log("Grip button released");
        }

        // Detect trigger release
        if (wasTriggerPressed && !triggerPressed)
        {
            Debug.Log("Trigger button released");
        }

        // Update previous states
        wasGripPressed = gripPressed;
        wasTriggerPressed = triggerPressed;
    }
    #endregion



    public void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Main"))
        {
            gameObject.SetActive(false);
            objectInMain.SetActive(true);
        }
    }


}
