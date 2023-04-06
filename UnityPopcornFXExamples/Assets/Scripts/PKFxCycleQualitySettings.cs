using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxCycleQualitySettings : MonoBehaviour
{
	private bool m_UpateQualitySetting = false;

	private int m_QualityLevel = 0;
	private int m_QualityLevelCount = 0;
	public void ChangeQualitySettings()
	{
		m_QualityLevel = QualitySettings.GetQualityLevel();
		m_QualityLevelCount = QualitySettings.names.Length;
		m_UpateQualitySetting = true;
		
	}

	void Update()
	{
		if (m_UpateQualitySetting)
		{
			QualitySettings.SetQualityLevel((m_QualityLevel + 1) % (m_QualityLevelCount));
			PopcornFX.PKFxManager.UpdateQualityLevels();
			m_UpateQualitySetting = false;
		}
	}
}
