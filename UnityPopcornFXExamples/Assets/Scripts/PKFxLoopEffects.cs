//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxLoopEffects : MonoBehaviour
{
	public List<PopcornFX.PKFxEmitter>		Emitters = new List<PopcornFX.PKFxEmitter>();
	public float							LoopTime = 0.0f;

	private float m_Timer;
	void Start()
	{
		m_Timer = 0.0f;
	}

	void Update()
	{
		if (LoopTime >= 0.01f)
		{
			m_Timer += Time.deltaTime;
			if (m_Timer >= LoopTime)
			{
				m_Timer = 0.0f;
				foreach (PopcornFX.PKFxEmitter emitter in Emitters)
				{
					if (emitter != null)
					{
						if (emitter.Alive)
							emitter.KillEffect();
						emitter.StartEffect();
					}
				}
			}
		}
	}
}
