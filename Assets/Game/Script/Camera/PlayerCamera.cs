using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner _cinemachineConfiner;
    private void Start()
    {
        var levelLimitCollider = FindObjectOfType<LevelConfig>().levelLimitCollider;
        _cinemachineConfiner.m_BoundingShape2D = levelLimitCollider;
    }
}
