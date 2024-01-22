using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Range(2.0f, 20.0f)]
    [SerializeField] private float _distance = 10.0f;

    [Range(0.0f, 10.0f)]
    [SerializeField] private float _height = 2.0f;

    [SerializeField] private float _damping = 10.0f;

    [SerializeField] private float _targetOffset = 2.0f;

    private Transform _trTarget;

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _trTarget = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        var pos = _trTarget.position + (-_trTarget.forward * _distance) + (Vector3.up * _height);

        //transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * damping); // 구면 선형 보간 : Slerp , 선형 보간 : Lerp

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref _velocity, _damping);

        transform.LookAt(_trTarget.position + (_trTarget.up * _targetOffset));
    }
}
