//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class PKFxStand : MonoBehaviour
{
	public List<PKFxRoomPartData>		m_Variations = new List<PKFxRoomPartData>();
	public List<string>					m_StringVariations = new List<string>();
	public PKFxRoomPartData				m_TriggerButtonPrefab;
	public PKFxRoomPartData				m_NamePlatePrefab;
	public List<PopcornFX.PKFxEmitter>	m_Effects = new List<PopcornFX.PKFxEmitter>();
	public Material						BaseImageMaterial;
	public Font Font;

	public bool HasTriggerButton
	{
		get { return m_HasTriggerButton; }
		set {
			if (m_HasTriggerButton != value)
			{
				m_HasTriggerButton = value;
				UpdateButton();
			}
		}
	}
	private bool m_HasTriggerButton = false;

	public bool HasNamePlate
	{
		get { return m_HasNamePlate; }
		set
		{
			if (m_HasNamePlate != value)
			{
				m_HasNamePlate = value;
				UpdateNamePlate();
			}
		}
	}
	private bool m_HasNamePlate = false;

	public int Current
	{
		get { return m_Current; }
		set
		{
			if (m_Current != value)
			{
				m_Current = value;
				UpdateStand();
			}
		}
	}
	private int						m_Current = 0;


	private GameObject		m_StandInstance = null;
	public GameObject		m_TriggerButtonInstance = null;
	private GameObject		m_NameplateInstance = null;

	public void Awake()
	{
		Transform trigger = transform.Find("TriggerButton");
		if (trigger != null)
		{
			m_TriggerButtonInstance = trigger.gameObject;
			m_HasTriggerButton = true;
		}

		Transform nameplate = transform.Find("NamePlate");
		if (nameplate != null)
		{
			m_NameplateInstance = nameplate.gameObject;
			m_HasNamePlate = true;
		}

		Transform standInstance = transform.Find("StandInstance");
		if (standInstance != null)
			m_StandInstance = standInstance.gameObject;
#if false
		if (EditorApplication.isPlaying) return;
		Clean();
		foreach (PKFxRoomPartData go in m_Variations)
		{
			if (go != null && go.Part)
				m_StringVariations.Add(go.Part.name);
			else
				m_StringVariations.Add("Empty");
		}
		UpdateStand();
#endif
	}

	public void Clean()
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

	public void UpdateButton()
	{
		if (m_TriggerButtonInstance != null)
			DestroyImmediate(m_TriggerButtonInstance);
		if (m_HasTriggerButton)
		{
			m_TriggerButtonInstance = Instantiate(m_TriggerButtonPrefab.Part, transform);
			m_TriggerButtonInstance.name = "TriggerButton";
			m_TriggerButtonInstance.transform.position += m_TriggerButtonPrefab.Offset;
		}
	}

	public void UpdateNamePlate()
	{
		if (m_NameplateInstance != null)
			DestroyImmediate(m_NameplateInstance);
		if (m_HasNamePlate)
		{
			m_NameplateInstance = Instantiate(m_NamePlatePrefab.Part, transform);
			m_NameplateInstance.name = "NamePlate";
			m_NameplateInstance.transform.position += m_NamePlatePrefab.Offset;
		}
	}
	

	public void UpdateStand()
	{
		if (m_StandInstance != null)
			DestroyImmediate(m_StandInstance);
		if (m_Current < m_Variations.Count && m_Variations[m_Current].Part != null)
		{
			m_StandInstance = Instantiate(m_Variations[m_Current].Part, transform);
			m_StandInstance.name = "StandInstance";
			m_StandInstance.transform.parent = transform;
			m_StandInstance.transform.position += m_Variations[m_Current].Offset;
		}
	}
}
