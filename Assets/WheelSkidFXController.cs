using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkidFXController : MonoBehaviour
{
    [SerializeField]WheelCollider[] m_wheelColliders;
    [SerializeField] AudioSource m_wheelFXAudioSource;
    [SerializeField] float m_slipThreshold;

    private void Update()
    {
        //check for slip on each wheel collider
        //increment a slip counter
        //play a skid sound if that counter >O
        //refactor all that into a start skid method
        CheckForSkid();
       
    }


    private void CheckForSkid()
    {
        int slipCount = 0;
        foreach (var wc in m_wheelColliders)
        {
            if (wc.GetGroundHit(out WheelHit wh))
            {
                
                if (Mathf.Abs(wh.forwardSlip) > m_slipThreshold
                    || Mathf.Abs(wh.sidewaysSlip) > m_slipThreshold)
                   
                {
                    slipCount++;
                    if (!m_wheelFXAudioSource.isPlaying)
                    {
                        m_wheelFXAudioSource.Play();
                    }
                }

            }
        }

        if(slipCount == 0 && m_wheelFXAudioSource.isPlaying == true)
        {
            m_wheelFXAudioSource.Stop();
        }
    }
}
