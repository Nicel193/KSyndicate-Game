using CodeBase;
using CodeBase.Infrastructure;
using CodeBase.Logic.Camera;
using CodeBase.Services.Input;
using UnityEngine;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private CharacterController _characterController;
    
    private IInputService _inputService;
    private Camera _camera;

    private void Awake()
    {
        _inputService = Game.InputService;
    }
    
    private void Start()
    {
        _camera = Camera.main;
        CameraFollow();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0f;
            movementVector.Normalize();

            transform.forward = movementVector;
        }

        movementVector += Physics.gravity;

        _characterController.Move(movementVector * (_movementSpeed * Time.deltaTime));
    }

    private void CameraFollow()
    {
        if (_camera is { } && _camera.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.Follow(this.gameObject);
        }
    }
}
