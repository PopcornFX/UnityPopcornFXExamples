using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PKFx_GridInitializer : MonoBehaviour
{
	[SerializeField] protected Texture2D				m_Texture;
					 protected PopcornFX.SamplerDesc	m_Sampler;
					 protected Color32[]				m_Pixels;
					 protected PopcornFX.PKFxEmitter	m_Effect;
					 protected int						m_SamplerDimX;
					 protected int						m_SamplerDimY;
					 protected int						m_SamplerDimZ;

	protected virtual void Start()
	{
		m_Pixels = m_Texture.GetPixels32();
	}

	public virtual void InitGrid(PopcornFX.SamplerDesc sampler, PopcornFX.SGenericNativeArray data)
	{
		if (m_Texture == null)
			return;

		m_Sampler = sampler;
		m_SamplerDimX = m_Sampler.m_GridDimensionsXY.x;
		m_SamplerDimY = m_Sampler.m_GridDimensionsXY.y;
		m_SamplerDimZ = m_Sampler.m_GridDimensionsZW.x;

		m_Effect = GetComponent<PopcornFX.PKFxEmitter>();
	}

	protected virtual void Update()
	{
		for (int z = 0; z < m_SamplerDimZ; ++z)
		{
			for (int y = 0; y < m_SamplerDimY; ++y)
			{
				for (int x = 0; x < m_SamplerDimX; ++x)
				{
					var pixelID = (x * m_SamplerDimY) + y + z * m_SamplerDimX * m_SamplerDimY;
					SetDataFromID(pixelID);
				}
			}
		}
		m_Texture.SetPixels32(m_Pixels, 0);
		m_Texture.Apply();
	}

	protected virtual void SetDataFromID(int pixelID)
	{
	}
}
