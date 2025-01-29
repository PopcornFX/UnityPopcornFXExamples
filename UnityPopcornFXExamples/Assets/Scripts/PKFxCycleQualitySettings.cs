using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PKFxCycleQualitySettings : MonoBehaviour
{
	private bool m_UpdateQualitySetting = false;

	private int m_QualityLevel = 0;
	private int m_QualityLevelCount = 0;
	private RenderPipelineAsset m_CurrentRenderPipelineAsset;

	private void Start()
	{
		m_CurrentRenderPipelineAsset = QualitySettings.renderPipeline;
    }

	public void ChangeQualitySettings()
	{
		m_QualityLevel = QualitySettings.GetQualityLevel();
		m_QualityLevelCount = QualitySettings.names.Length;
		m_UpdateQualitySetting = true;
	}

	void Update()
	{
		if (m_UpdateQualitySetting)
		{
			QualitySettings.SetQualityLevel((m_QualityLevel + 1) % (m_QualityLevelCount));
			if(m_CurrentRenderPipelineAsset != null)
				GraphicsSettings.renderPipelineAsset = m_CurrentRenderPipelineAsset;
            PopcornFX.PKFxManager.UpdateQualityLevels();
			m_UpdateQualitySetting = false;
        }
	}
}
