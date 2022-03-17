//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PKFxRoomPartData
{
	public GameObject	Part;
	public Vector3		Offset;
	public Vector3		Scale;

	public PKFxRoomPartData()
	{
		Part = null;
		Offset = Vector3.zero;
		Scale = Vector3.one;
	}
}

[ExecuteAlways]
public class PKFxRoomGenerator : MonoBehaviour
{
	public GameObject		m_TargetObject;
	public GameObject		m_PersitentTargetObject;

	public List<PKFxRoomPartData> m_Dividers = new List<PKFxRoomPartData>();	// 2: small, double width
	public List<PKFxRoomPartData> m_BackWalls = new List<PKFxRoomPartData>();	// 2: small, double height
	public List<PKFxRoomPartData> m_Clamps = new List<PKFxRoomPartData>();		// 2: small, double height
	public List<PKFxRoomPartData> m_Ls = new List<PKFxRoomPartData>();			// 2: floor, floor missing 
	public List<PKFxRoomPartData> m_Trim = new List<PKFxRoomPartData>();		// 3: small, double height, double width (front clamp)
	public List<PKFxRoomPartData> m_Us = new List<PKFxRoomPartData>();			// 3: small, double height, small with hole (floor + roof)
	public List<PKFxRoomPartData> m_Walls = new List<PKFxRoomPartData>();       // 2: small, double height

	public PKFxRoomPartData m_Stand = new PKFxRoomPartData();
	public PKFxRoomPartData m_Divider = new PKFxRoomPartData();
	public PKFxRoomPartData m_CeilLight = new PKFxRoomPartData();

	public int Rooms;
	public int Segments;

	public bool Terrasse;
	public bool DoubleHeight;

	private const float KSEGMENT_OFFSET = -10.0f;
	private const float KTRIM_OFFSET = -3.0f;

	void Awake()
	{
	}

	void Update()
	{

	}

	public void OnDestroy()
	{


	}
	
	private GameObject InstantiateScaledAndParent(PKFxRoomPartData partData, Vector3 positionOff, Vector3 scaleFactor, string name = null, bool persistency = false)
	{
		var obj = InstantiateAndParent(partData, positionOff, name, persistency);
		if (obj == null)
			return null;
		Vector3 localS = new Vector3(scaleFactor.x / partData.Scale.x, scaleFactor.y / partData.Scale.y, scaleFactor.z / partData.Scale.z);

		obj.transform.localScale = localS;
		return obj;
	}
	private GameObject InstantiateAndParent(PKFxRoomPartData partData, Vector3 positionOff, string name = null, bool persistency = false)
	{
		Transform	parentTrans;
		GameObject	currentObj = null;

		if (persistency)
		{
			if (name != null)
			{
				Transform current = m_PersitentTargetObject.transform.Find(name);
				if (current)
					currentObj = current.gameObject;
			}
			else
				return null;
		}
		if (persistency && m_PersitentTargetObject != null)
			parentTrans = m_PersitentTargetObject.transform;
		else
			parentTrans = m_TargetObject.transform;

		if (currentObj == null)
			currentObj = Instantiate(partData.Part, parentTrans);
		currentObj.transform.position = Vector3.zero;
		currentObj.transform.position += positionOff;
		currentObj.transform.position += partData.Offset;
		if (name != null)
			currentObj.name = name;
		return currentObj;
	}

	public void GenerateRoom(ref Vector3 positionOff, int roomIdx)
	{
		int idx = 0;
		if (DoubleHeight)
			idx = 1;

		InstantiateAndParent(m_Trim[idx], positionOff);
		InstantiateAndParent(m_Clamps[idx], positionOff);
		positionOff.x += KTRIM_OFFSET / 2.0f;
		if (roomIdx == 0)
		{
			if (!Terrasse)
				InstantiateAndParent(m_Walls[idx], positionOff);
			else
			{
				InstantiateAndParent(m_Dividers[0], positionOff);
			}
		}
		else
		{
			InstantiateAndParent(m_Dividers[0], positionOff);
		}
		GameObject divider = InstantiateAndParent(m_Divider, positionOff, "Divider " + roomIdx + " Start", true);
		divider.GetComponent<PKFxDivider>().SetIsStart(true);

		positionOff.x += KTRIM_OFFSET / 2.0f;

		InstantiateScaledAndParent(m_BackWalls[idx], positionOff, new Vector3((float)Segments, 1, 1));
		for (int i = 0; i < Segments; ++i)
		{
			InstantiateAndParent(m_Us[idx], positionOff);

			InstantiateAndParent(m_CeilLight, positionOff);
			
			GameObject stand = InstantiateAndParent(m_Stand, positionOff, "Stand " + roomIdx + " " + i, true);
			if (stand)
			{
				stand.GetComponent<PKFxStand>().UpdateStand();
			}
			positionOff.x += KSEGMENT_OFFSET;
		}

		if (roomIdx == Rooms - 1)
		{
			InstantiateAndParent(m_Walls[idx], positionOff);
		}
		Vector3 endDividerOff = positionOff + new Vector3(KTRIM_OFFSET / 2.0f, 0, 0);
		divider = InstantiateAndParent(m_Divider, endDividerOff, "Divider " + roomIdx + " End", true);
		divider.GetComponent<PKFxDivider>().SetIsStart(false);
	}

	public void GenerateRooms()
	{
		if (m_TargetObject == null)
			return;
		Vector3 positionOff = new Vector3();
		for (int i = 0; i < Rooms; ++i)
		{
			GenerateRoom(ref positionOff, i );
		}
	}

	public void CleanRooms()
	{
		if (m_TargetObject == null)
			return;
		for (int i = m_TargetObject.transform.childCount -1; i >= 0; --i)
		{
			DestroyImmediate(m_TargetObject.transform.GetChild(i).gameObject);
		}
	}
}
