using CodeBase;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMove : MonoBehaviour, ISavedProgress
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private CharacterController _characterController;

    private IInputService _inputService;
    private Camera _camera;

    private void Awake()
    {
        _inputService = ServiceLocator.Contener.Single<IInputService>();
    }

    private void Start()
    {
        _camera = Camera.main;
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

    public void UpdateProgress(PlayerProgress progress)
    {
        progress.WorldData.PositionOnLevel =
            new PositionOnLevel(CurrentLevelName(), this.transform.position.AsVectorData());
    }

    public void LoadProgress(PlayerProgress progress)
    {
        if (CurrentLevelName() == progress.WorldData.PositionOnLevel.LevelName)
        {
            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

            if (savedPosition != null) Warp(savedPosition);
        }
    }

    private void Warp(Vector3Data savedPosition)
    {
        _characterController.enabled = false;
        transform.position = savedPosition.AsUnityVector().AddY(_characterController.height);
        _characterController.enabled = true;
    }

    private string CurrentLevelName()
        => SceneManager.GetActiveScene().name;
}