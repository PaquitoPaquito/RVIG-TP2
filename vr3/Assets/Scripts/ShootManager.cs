using UnityEngine;
using UnityEngine.XR;

public class ShootManager : MonoBehaviour
{
    [SerializeField] private float angleThreshold;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private AudioSource audioSource;

    private InputDevice _rightController;
    private GameObject[] _targets;
    
    private void Start()
    {
        _rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        _targets = GameObject.FindGameObjectsWithTag("Target");
    }

    private void Update()
    {
        if(!_rightController.isValid)
        {
            _rightController=InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            if(!_rightController.isValid) return;
        }
        if (_rightController.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerPressed))
        {
        } 
    }
}
