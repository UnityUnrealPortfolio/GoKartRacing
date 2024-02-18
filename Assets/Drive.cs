using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/// <summary>
/// Drives the car using keyboard input (Hardwired)
/// will refactor to receive inputs from seperate Inputs class
/// to allow Gamepad and mobile joystick input
/// </summary>
public class Drive : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] WheelCollider m_frontRightWC;
    [SerializeField] WheelCollider m_frontLeftWC;
    [SerializeField] WheelCollider m_backRightWC;
    [SerializeField] WheelCollider m_backLeftWC;

    [Header("Wheel Meshes")]
    [SerializeField] GameObject m_frontRightWM;
    [SerializeField] GameObject m_frontLeftWM;
    [SerializeField] GameObject m_backRightWM;
    [SerializeField] GameObject m_backLeftWM;

    [Header("Drive Parameters")]
    [SerializeField] float m_maxTorque;
    [SerializeField] float m_maxSteerAngle;
    [SerializeField] float m_turnRate;

    private WheelCollider[] allWheelColliders;
    private Dictionary<WheelCollider,GameObject> wheels = new Dictionary<WheelCollider,GameObject>();
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        //receive inputs
        float forwardBackInput = Input.GetAxis("Vertical");
        forwardBackInput = Mathf.Clamp(forwardBackInput, -1, 1);//ToDo:do we need to do this?
        float turnInput = Input.GetAxis("Horizontal") * m_turnRate;
        turnInput = Mathf.Clamp(turnInput,-m_maxSteerAngle,m_maxSteerAngle);
        //pass forward back input to wheel colliders
        DriveCar(forwardBackInput,turnInput);
    }

    private void DriveCar(float accel,float steer)
    {
        //calculate torque
        var driveForce = accel * m_maxTorque;
        //add torque to wheel colliders
        foreach (var wc in allWheelColliders)
        {
            wc.motorTorque = driveForce;
        }

        //steer front wheel colliders
        m_frontLeftWC.steerAngle = steer;
        m_frontRightWC.steerAngle = steer;
        //snap wheel mesh position and rotation to colliders
        foreach (KeyValuePair<WheelCollider,GameObject> wc in wheels)
        {
            SetWheelMeshPosAndRot(wc.Key, wc.Value);
        }
        
        
    }

    private void Init()
    {
        allWheelColliders = new WheelCollider[] { m_frontRightWC, m_frontLeftWC, m_backRightWC, m_backLeftWC };
        wheels.Add(m_frontLeftWC, m_frontLeftWM);
        wheels.Add(m_frontRightWC, m_frontRightWM);
        wheels.Add(m_backRightWC, m_backRightWM);
        wheels.Add(m_backLeftWC, m_backLeftWM);
    }

    private void SetWheelMeshPosAndRot(WheelCollider wc, GameObject wm)
    {
        wc.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wm.transform.position = pos;
        wm.transform.rotation = rot;    
    }
}
