//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxLoopThroughSamplerShape : MonoBehaviour
{
	public float TimeForSamplers = 2.0f;
	public bool Loop = false;

	private float m_Timer = 0.0f;
	PopcornFX.PKFxEmitter		m_Emitter;
	private PopcornFX.Sampler	m_Sampler;
	void Start()
	{
		m_Emitter = GetComponent<PopcornFX.PKFxEmitter>();

		if (m_Emitter && m_Emitter.m_FxSamplersList.Count > 0)
		{
			foreach (var sampler in m_Emitter.m_FxSamplersList)
			{
				if (sampler.m_Descriptor.m_Type == PopcornFX.ESamplerType.SamplerShape)
					m_Sampler = sampler;
			}

		}
	}

	void Update()
	{
		if (!Loop)
			return;
		m_Timer += Time.deltaTime;

		if (m_Timer >= TimeForSamplers)
		{
			m_Timer = 0;
			IncrementSamplerShapeAndRestartFX();
		}
	}

	public void IncrementSamplerShapeAndRestartFX()
	{
		int typeValue = ((int)m_Sampler.m_ShapeType + 1) % ((int)PopcornFX.Sampler.EShapeType.CapsuleShape + 1);
		m_Sampler.m_ShapeType = (PopcornFX.Sampler.EShapeType)typeValue;
		m_Emitter.KillEffect();
		m_Emitter.StartEffect();
	}
}
