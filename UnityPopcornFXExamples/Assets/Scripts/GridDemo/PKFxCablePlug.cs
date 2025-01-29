using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxCablePlug : MonoBehaviour
{
    [SerializeField] private Transform      m_Target;
    [SerializeField] private Light          m_Light;
    [SerializeField] private MeshRenderer   m_Base;
    [SerializeField] private MeshRenderer   m_Reactor;
                     private Material       m_MaterialBase;
                     private Material       m_MaterialReactor;
                     private Color          m_Color;
                     public Transform       Target => m_Target;
                     public Color           Color => m_Color;


    private void Start()
	{
        m_MaterialBase = new Material(m_Base.material);
        m_Base.material = m_MaterialBase;
        m_MaterialReactor = new Material(m_Reactor.material);
        m_Reactor.material = m_MaterialReactor;
    }

	public void SetColor(Color color)
    {
        m_MaterialBase.SetColor("_EmissionColor", color);
        m_MaterialReactor.SetColor("_EmissionColor", color);
        m_Color = color;
    }
}
