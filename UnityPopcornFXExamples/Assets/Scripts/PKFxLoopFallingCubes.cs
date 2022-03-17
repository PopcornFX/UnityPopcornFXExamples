//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxLoopFallingCubes : MonoBehaviour
{
	public List<PopcornFX.PKFxEmitter> Emitters = new List<PopcornFX.PKFxEmitter>();
	public float LoopTime = 0.0f;

	private List<Vector3> m_StartPositions = new List<Vector3>();
	private List<Quaternion> m_StartRotations = new List<Quaternion>();
	private float m_Timer;
	void Start()
	{
		m_Timer = 0.0f;
		foreach (PopcornFX.PKFxEmitter emitter in Emitters)
		{
			m_StartPositions.Add(emitter.transform.position);
			m_StartRotations.Add(emitter.transform.rotation);
		}
	}

	void Update()
	{
		if (LoopTime >= 0.01f)
		{
			m_Timer += Time.deltaTime;
			if (m_Timer >= LoopTime)
			{
				m_Timer = 0.0f;

				int idx = 0;
				foreach (PopcornFX.PKFxEmitter emitter in Emitters)
				{
					if (emitter != null)
					{
						if (emitter.Alive)
							emitter.KillEffect();
						emitter.StartEffect();
						emitter.transform.SetPositionAndRotation(m_StartPositions[idx], m_StartRotations[idx]);
					}
					++idx;
				}
			}
		}
	}
}
