using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    #region PRIVATE VARIABLE
    [SerializeField] private GameObject _playerTarget;
    private Vector3 _offset;
    public float sensitivity = 10f;
    #endregion

    #region BUILTIN METHOD
    void Start()
    {
        _offset = transform.position - _playerTarget.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _playerTarget.transform.position + _offset;
        transform.LookAt(_playerTarget.transform);
    }
    #endregion
}
