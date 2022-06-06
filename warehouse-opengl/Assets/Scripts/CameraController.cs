using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _baseSpeed = 20.0f;
    [SerializeField] private float _screenBoundary = 2f;
    [SerializeField] private bool _moveCameraWithMouse = false;

    [SerializeField] private float _minCameraY = 5f;
    [SerializeField] private float _maxCameraY = 20f;
    [SerializeField] private float _heightSpeedMultiplier = 1f;
    [SerializeField] private float _zoomSpeed = 1f;

    [SerializeField, Range( 0f, 0.99f )] private float _moveSmoothiness = 0.5f;
    [SerializeField, Range( 0f, 0.99f )] private float _rotateSmoothiness = 0.5f;
    [SerializeField, Range( 0f, 0.99f )] private float _zoomSmoothiness = 0.5f;
    [SerializeField] private float _rotateSpeed = 100f;

    [SerializeField] public Transform mainCamera = null;
    [SerializeField] private Renderer mapBoundsPlane = null;

    private Bounds _mapBounds;
    private Vector3 _smoothedMoveInput = Vector3.zero;
    private float _smoothedZoomInput = 0f;
    private float _smoothedRotateInput = 0f;

    private void Start()
    {
        AdjustCameraTilt();
        AdjustMapBounds();
    }

    private void Update()
    {
        HandleMoving();
        HandleRotating();
        HandleZooming();
    }
    
    private void AdjustMapBounds()
    {
        _mapBounds = mapBoundsPlane.bounds;
        _mapBounds.size += new Vector3( 0, 100, 0 );
    }

    private void AdjustCameraTilt()
    {
        Vector3 positionAlignedToGround = new Vector3( transform.position.x, 0f, transform.position.z );
        mainCamera.LookAt( positionAlignedToGround );
    }

    private void HandleMoving()
    {
        Vector3 moveInput = GetMoveInput();
        moveInput = GetSpeed( moveInput );

        _smoothedMoveInput = Vector3.Lerp( _smoothedMoveInput, moveInput, 1 - _moveSmoothiness );

        transform.Translate( _smoothedMoveInput );
        if (!_mapBounds.Contains(transform.position))
            transform.position = _mapBounds.ClosestPoint(transform.position);
    }
    
    private void HandleZooming()
    {
        float zoomInput = Input.GetAxis( "Mouse ScrollWheel" );
        _smoothedZoomInput = Mathf.Lerp( _smoothedZoomInput, zoomInput, 1 - _zoomSmoothiness );

        Vector3 translation = mainCamera.forward * _zoomSpeed * Time.unscaledDeltaTime * 100f;
        if ( (mainCamera.position + translation * _smoothedZoomInput ).y > _maxCameraY )
            _smoothedZoomInput = 0f;
        if ( ( mainCamera.position + translation * _smoothedZoomInput ).y < _minCameraY )
            _smoothedZoomInput = 0f;

        translation *= _smoothedZoomInput;

        mainCamera.Translate( translation, Space.World );
    }

    private void HandleRotating()
    {
        float direction = 0f;

        if ( Input.GetKey( KeyCode.Q ) )
            direction = -1f;
        else if ( Input.GetKey( KeyCode.E ) )
            direction = 1f;
        else if ( Input.GetMouseButton( 2 ) )
        {
            //Cursor.lockState = CursorLockMode.Locked;
            direction = Input.GetAxisRaw( "Mouse X" );
        }
        else if ( Cursor.lockState == CursorLockMode.Locked )
            Cursor.lockState = CursorLockMode.None;

        _smoothedRotateInput = Mathf.Lerp( _smoothedRotateInput, -direction, 1 - _rotateSmoothiness );
        transform.Rotate( Vector3.up, _smoothedRotateInput * Time.unscaledDeltaTime * _rotateSpeed, Space.World );
    }

    private Vector3 GetMoveInput()
    {
        Vector3 result = new Vector3();
        bool isMovingWithKeyboard = false;

        result += GetKeyboardInput( ref isMovingWithKeyboard );

        if ( !isMovingWithKeyboard )
            result += GetMouseInput();

        return result;
    }

    private Vector3 GetMouseInput()
    {
        Vector3 result = Vector3.zero;
        if ( _moveCameraWithMouse )
        {
            if ( Input.mousePosition.x > Screen.width - _screenBoundary )
                result += new Vector3( 1, 0, 0 );

            if ( Input.mousePosition.x < 0 + _screenBoundary )
                result += new Vector3( -1, 0, 0 );

            if ( Input.mousePosition.y > Screen.height - _screenBoundary )
                result += new Vector3( 0, 0, 1 );

            if ( Input.mousePosition.y < 1 )
                result += new Vector3( 0, 0, -1 );
        }

        return result;
    }

    private Vector3 GetKeyboardInput( ref bool isMovingWithKeyboard )
    {
        Vector3 result = Vector3.zero;
        if ( Input.GetKey( KeyCode.W ) )
        {
            result += new Vector3( 0, 0, 1 );
            isMovingWithKeyboard = true;
        }
        if ( Input.GetKey( KeyCode.S ) )
        {
            result += new Vector3( 0, 0, -1 );
            isMovingWithKeyboard = true;
        }
        if ( Input.GetKey( KeyCode.A ) )
        {
            result += new Vector3( -1, 0, 0 );
            isMovingWithKeyboard = true;
        }
        if ( Input.GetKey( KeyCode.D ) )
        {
            result += new Vector3( 1, 0, 0 );
            isMovingWithKeyboard = true;
        }

        return result;
    }

    private Vector3 GetSpeed( Vector3 baseInput )
    {
        baseInput *= _baseSpeed;
        baseInput *= ( mainCamera.position.y - _minCameraY ) / ( _maxCameraY - _minCameraY ) * _heightSpeedMultiplier + 1;

        return baseInput * Time.deltaTime;
    }
}