using PopcornFX;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class PKFxShallowWater : PKFx_GridInitializer
{
	private NativeArray<Vector4>	m_RawData;
	private bool					m_IsPlayerIn;
	private GameObject				m_Player;

	public override void InitGrid(SamplerDesc sampler, SGenericNativeArray data)
	{
		m_RawData = data.GetNativeArray<Vector4>();
		base.InitGrid(sampler, data);
	}

	protected override void Update()
	{
		if (m_IsPlayerIn)
			m_Effect.SetAttributeSafe(m_Effect.GetAttributeId("pos", PopcornFX.EAttributeType.Float3), m_Player.transform.position);
		base.Update();
	}

	protected override void SetDataFromID(int pixelID)
	{
		Vector4	dataValue = m_RawData[pixelID];
		m_Pixels[pixelID].r = (byte)(dataValue.x * 255);
		m_Pixels[pixelID].g = (byte)(dataValue.y * 255);
		m_Pixels[pixelID].b = (byte)(dataValue.z * 255);
		m_Pixels[pixelID].a = (byte)(dataValue.w * 255);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_IsPlayerIn = true;
			m_Player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			m_IsPlayerIn = false;
		}
	}
}
