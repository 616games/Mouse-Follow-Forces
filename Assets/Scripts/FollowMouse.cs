using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    #region --Fields / Properties--
    
    /// <summary>
    /// How close to follow the mouse cursor.
    /// </summary>
    [SerializeField]
    private float _followSpeed = 1;

    /// <summary>
    /// The distance in front of the camera the game object should be positioned.
    /// </summary>
    private float _forwardDistance = 15f;
    
    /// <summary>
    /// Camera to be used to capture the screen position of the mouse.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// Cached Transform component.
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Speed and direction of the game object.
    /// </summary>
    private Vector3 _velocity;

    /// <summary>
    /// How fast the velocity is changing.
    /// </summary>
    private Vector3 _acceleration;
    
    #endregion
    
    #region --Unity Specific Methods--
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        Follow();
    }
    #endregion
    
    #region --Custom Methods--

    /// <summary>
    /// Initializes variables and caches components.
    /// </summary>
    private void Init()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    /// <summary>
    /// Moves the game object to follow the mouse cursor.
    /// </summary>
    private void Follow()
    {
        Vector3 _direction = GetMouseWorldPosition() - transform.position;
        
        ApplyForce(_direction.normalized);
        
        _velocity += _acceleration;
        _transform.position += _velocity * (_followSpeed * Time.deltaTime);
        
        _velocity *= .99f;
        _acceleration = Vector3.zero;
    }

    private void ApplyForce(Vector3 _force)
    {
        _acceleration += _force;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 _mousePosition = Input.mousePosition;
        _mousePosition.z = _forwardDistance;
        
        return _camera.ScreenToWorldPoint(_mousePosition);
    }
    
    #endregion
    
}
