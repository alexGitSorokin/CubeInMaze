using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _height = 2f;
    [SerializeField] private float _minZoom = 5f;
    [SerializeField] private float _speedZoom = 2f;
    [SerializeField] private float _maxZoom = 15f;
    [SerializeField] private float _rotateSpeed = 120f;
    private float _currentRotate = 0f;
    private float _currentZoom = 10f;
    #endregion

    #region Methods
    private void Update()
    {
        transform.position = _target.position + _offset;
        _currentRotate -= UnityEngine.Input.GetAxis("Horizontal") * _rotateSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = _target.position - _offset * _currentZoom;
        transform.LookAt(_target.position + Vector3.up * _height);
    }
    #endregion
}
