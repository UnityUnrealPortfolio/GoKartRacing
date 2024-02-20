using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkidFXController : MonoBehaviour
{
    [SerializeField]WheelCollider[] m_wheelColliders;
    [SerializeField] AudioSource m_wheelFXAudioSource;
    [SerializeField] float m_slipThreshold;
    [SerializeField] GameObject m_trailObject;
    [SerializeField] ParticleSystem m_smokeTrailParticleFx;

    private GameObject[] skidTrails;//ToDo:serialized for testing
    private ParticleSystem[] smokeTrailParticles;
    private void Start()
    {
        skidTrails = new GameObject[4];
        smokeTrailParticles = new ParticleSystem[4];
    }
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

                    //generate skid trail fx
                    
                }

            }
        }

        if(slipCount > 0)
        {
            for(int i = 0;i< m_wheelColliders.Length;i++)
            {

                StartSkidTrail(i);
            }
        }
        else
        {
            for (int i = 0; i < m_wheelColliders.Length; i++)
            {

                EndSkidTrail(i);
            }
        }
        

        if(slipCount == 0 && m_wheelFXAudioSource.isPlaying == true)
        {
            m_wheelFXAudioSource.Stop();
        }

       
    }

    public void StartSkidTrail(int i)
    {
        if (skidTrails[i] == null)
        {
            skidTrails[i] = Instantiate(m_trailObject);
            smokeTrailParticles[i] = Instantiate(m_smokeTrailParticleFx);
            smokeTrailParticles[i].Stop();
        }

        //ToDo:refactor to one sub routine that accepts a smoke trail, a tire track trail and spawns and emits them
        //spawn tire mark fx
        skidTrails[i].transform.parent = m_wheelColliders[i].transform;
        skidTrails[i].transform.localPosition = -Vector3.up * m_wheelColliders[i].radius;
        skidTrails[i].transform.rotation = Quaternion.Euler(90, 0, 0);

        //spawn smoke trailfx
        smokeTrailParticles[i].transform.parent = m_wheelColliders[i].transform;
        smokeTrailParticles[i].transform.localPosition = -Vector3.up * m_wheelColliders[i].radius;
        smokeTrailParticles[i].Emit(1);
    }
    public void EndSkidTrail(int i)
    {
        if (skidTrails[i] == null) return;
        Transform trailHolder = skidTrails[i].transform;
        skidTrails[i] = null;
        trailHolder.parent = null;
        Destroy(trailHolder.gameObject, 30);
    }
}
