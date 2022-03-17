//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PKFxTriggerPillar : MonoBehaviour
{
	public UnityEvent<Collider> OnPublicTriggerEnter = null;
	public Material m_Off;
	public Material m_On;

	[SerializeField]
	public UnityEvent<Collider> OnPublicTriggerExit = null;
	
	[SerializeField]
	public UnityEvent<Collider> OnPublicTriggerStay = null;

	private MeshRenderer m_renderer;
	void Start()
	{

		m_renderer = GetComponentInChildren<MeshRenderer>();

	}

	private void OnTriggerEnter(Collider other)
	{
		if (OnPublicTriggerEnter != null)
			OnPublicTriggerEnter.Invoke(other);
		if (m_renderer)
		{
			Material[] materials = m_renderer.materials;
			materials[2] = m_On;
			m_renderer.materials = materials;
		}
			
	}

	private void OnTriggerStay(Collider other)
	{
		if (OnPublicTriggerStay != null)
			OnPublicTriggerStay.Invoke(other);
	}

	private void OnTriggerExit(Collider other)
	{
		if (OnPublicTriggerExit != null)
			OnPublicTriggerExit.Invoke(other);
		if (m_renderer)
		{
			Material[] materials = m_renderer.materials;
			materials[2] = m_Off;
			m_renderer.materials = materials;
		}

	}
}
