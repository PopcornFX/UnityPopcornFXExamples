//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxChangeSkinnedMeshTarget : MonoBehaviour
{
	public PopcornFX.PKFxEmitter m_Emitter;

	public SkinnedMeshRenderer Target_0;
	public SkinnedMeshRenderer Target_1;


	private PopcornFX.Sampler m_Sampler;
	void Start()
	{
		if (m_Emitter && m_Emitter.m_FxSamplersList.Count > 0)
		{
			foreach (var sampler in m_Emitter.m_FxSamplersList)
			{
				if (sampler.m_Descriptor.m_Type == PopcornFX.ESamplerType.SamplerShape &&
					sampler.m_ShapeType == PopcornFX.Sampler.EShapeType.BakedMeshShape)
				{
					m_Sampler = sampler;
					m_Sampler.m_SkinnedMeshRenderer = Target_0;
					m_Sampler.m_ShapeTransformReference = Target_0.transform;
				}
			}
		}
	}

	public void ChangeSkinnedMeshTarget()
	{
		SkinnedMeshRenderer swap = Target_0;
		Target_0 = Target_1;
		Target_1 = swap;

		if (m_Sampler != null)
		{
			m_Sampler.m_SkinnedMeshRenderer = Target_0;
			m_Sampler.m_ShapeTransformReference = Target_0.transform;
		}
		
	}
}
