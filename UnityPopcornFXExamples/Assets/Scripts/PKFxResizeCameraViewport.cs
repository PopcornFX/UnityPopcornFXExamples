//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxResizeCameraViewport : MonoBehaviour
{
	public Camera Main;
	public Camera Secondary;

	public int GrowthFactor;

	public float m_TimeElapse;
	public float m_Timer = 2.0f;

	public Vector4 m_MainStart;
	public Vector4 m_MainEnd;

	public Vector4 m_SecondaryStart;
	public Vector4 m_SecondaryEnd;
	void Start()
	{
		GrowthFactor = 0;
		m_TimeElapse = 0.0f;
	}


	void Update()
	{
		if (GrowthFactor > 0)
		{
			m_TimeElapse += Time.deltaTime;
			Vector4 rect = Vector4.Lerp(m_SecondaryStart, m_SecondaryEnd, m_TimeElapse / m_Timer);
			Secondary.rect = new Rect(rect.x, rect.y, rect.z, rect.w);

			rect = Vector4.Lerp(m_MainStart, m_MainEnd, m_TimeElapse / m_Timer);
			Main.rect = new Rect(rect.x, rect.y, rect.z, rect.w);

		}
		else if (GrowthFactor < 0)
		{
			m_TimeElapse += Time.deltaTime;
			Vector4 rect = Vector4.Lerp(m_SecondaryEnd, m_SecondaryStart, m_TimeElapse / m_Timer);
			Secondary.rect = new Rect(rect.x, rect.y, rect.z, rect.w);

			rect = Vector4.Lerp(m_MainEnd, m_MainStart, m_TimeElapse / m_Timer);
			Main.rect = new Rect(rect.x, rect.y, rect.z, rect.w);

		}
		if (m_TimeElapse > m_Timer)
		{
			GrowthFactor = 0;
			m_TimeElapse = 0.0f;
		}
	}

	public void SetGrow()
	{
		GrowthFactor = 1;
		if (m_TimeElapse != 0.0f)
			m_TimeElapse = m_Timer - m_TimeElapse;
	}

	public void SetShrink()
	{
		GrowthFactor = -1;
		if (m_TimeElapse != 0.0f)
			m_TimeElapse = m_Timer - m_TimeElapse;
	}
}
