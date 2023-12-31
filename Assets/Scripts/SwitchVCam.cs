using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAcion;

    [SerializeField] private Canvas normalCanves;
    [SerializeField] private Canvas aimCanves;


    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAcion = playerInput.actions["Aim"];
    }
    private void OnEnable()
    {
        aimAcion.performed += _ => StartAim();
        aimAcion.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAcion.performed -= _ => StartAim();
        aimAcion.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        normalCanves.enabled = false;
        aimCanves.enabled = true;
        virtualCamera.Priority += 10;
    }

    private void CancelAim()
    {
        normalCanves.enabled = true;
        aimCanves.enabled = false;
        virtualCamera.Priority -= 10;
    }
}
