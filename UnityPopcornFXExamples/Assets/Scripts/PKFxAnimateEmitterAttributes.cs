//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxAnimateEmitterAttributes : MonoBehaviour
{
	public PopcornFX.PKFxEmitter	m_Target;


	int m_ColorID;
	int m_RotationSpeedID;
	int m_SpawnCountScaleID;

	Vector4 m_ColorStartValue;
	public Vector4 m_ColorTargetValue;

	float m_RotationStartValue;
	public float m_RotationTargetValue;

	float m_SpawnCountStartValue;
	public float m_SpawnCountTargetValue;

	float m_Timer = 2.0f;
	float m_TimeElapse;
	float m_LerpScale = 1.0f;

	void Start()
	{
		m_TimeElapse = 0.0f;
		if (m_Target != null)
		{
			m_RotationSpeedID = m_Target.GetAttributeId("RotationSpeed", PopcornFX.EAttributeType.Float);
			m_SpawnCountScaleID = m_Target.GetAttributeId("SpawnCountScale", PopcornFX.EAttributeType.Float);
			m_ColorID = m_Target.GetAttributeId("Color", PopcornFX.EAttributeType.Float4);

			m_RotationStartValue = m_Target.GetAttributeFloat(m_RotationSpeedID);
			m_SpawnCountStartValue = m_Target.GetAttributeFloat(m_SpawnCountScaleID);
			m_ColorStartValue = m_Target.GetAttributeFloat4(m_ColorID);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (m_TimeElapse > m_Timer)
			m_LerpScale = -1.0f;
		else if (m_TimeElapse < 0.0f)
			m_LerpScale = 1.0f;
		m_TimeElapse += Time.deltaTime * m_LerpScale;

		m_Target.SetAttributeSafe(m_RotationSpeedID, Mathf.Lerp(m_RotationStartValue, m_RotationTargetValue, m_TimeElapse / m_Timer));
		m_Target.SetAttributeSafe(m_SpawnCountScaleID, Mathf.Lerp(m_SpawnCountStartValue, m_RotationTargetValue, m_TimeElapse / m_Timer));
		m_Target.SetAttributeSafe(m_ColorID, Vector4.Lerp(m_ColorStartValue, m_ColorTargetValue, m_TimeElapse / m_Timer));
	}
}
