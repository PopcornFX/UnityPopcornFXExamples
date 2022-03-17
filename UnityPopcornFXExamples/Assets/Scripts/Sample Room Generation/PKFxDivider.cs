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
public class PKFxDivider : MonoBehaviour
{
	public Material BaseImageMaterial;
	public Font		Font;
	public float	m_XOffset = 0.0f;


	public void Clean()
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}
	}

	public void SetIsStart(bool start)
	{
		if (start)
		{
			m_XOffset = 0.0f;
			transform.rotation = Quaternion.Euler(0, -90, 0);
		}
		else
		{
			m_XOffset = 18.0f;
			transform.rotation = Quaternion.Euler(0, 90, 0);
		}
	}
}
