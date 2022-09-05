using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField] private float clampAngle = 80f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    
    private PlayerInputActions m_playerInputActions;
    private Vector3 startingRotation;
    
    protected override void Awake()
    {
        m_playerInputActions = new PlayerInputActions();
        base.Awake();
    }

    protected override void OnEnable()
    {
        m_playerInputActions.PlayerMovement.Enable();
        base.OnEnable();
    }

    private void OnDisable()
    {
        m_playerInputActions.PlayerMovement.Disable();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!vcam.Follow) return;

        if (stage != CinemachineCore.Stage.Aim) return;

        if (startingRotation == Vector3.zero) startingRotation = transform.localRotation.eulerAngles;
        var deltaInput = m_playerInputActions.PlayerMovement.Look.ReadValue<Vector2>();
        startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
        startingRotation.y -= deltaInput.y * horizontalSpeed * Time.deltaTime;
        startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
        state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
    }
}
