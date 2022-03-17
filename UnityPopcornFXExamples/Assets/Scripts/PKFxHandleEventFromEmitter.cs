//--------------------------------------------------------------------------------------------------------
// Copyright Persistant Studios, SARL. All Rights Reserved. https://www.popcornfx.com/terms-and-conditions/
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKFxHandleEventFromEmitter : MonoBehaviour
{	
	public GameObject InstantiateOnEvent;
	public void HandleEvent(string name, List<PopcornFX.SPopcornFXPayloadData> data)
	{
		//Collision Event
		PopcornFX.SPopcornFXPayloadData positionValues = new PopcornFX.SPopcornFXPayloadData();
		PopcornFX.SPopcornFXPayloadData colorValues = new PopcornFX.SPopcornFXPayloadData();

		foreach (PopcornFX.SPopcornFXPayloadData payloadData in data)
		{
			if (payloadData.m_PayloadType == PopcornFX.EAttributeType.Float3 &&
				payloadData.m_PayloadName == "Position")
			{
				positionValues = payloadData;
			}
			if (payloadData.m_PayloadType == PopcornFX.EAttributeType.Float4 &&
				payloadData.m_PayloadName == "Color")
			{
				colorValues = payloadData;
			}
		}

		Vector3 position = new Vector3();
		Vector4 color = new Vector4();
		for (int i = 0; i < positionValues.NumberOfValues; ++i)
		{
			positionValues.GetFloat3(out position, i);
			colorValues.GetFloat4(out color, i);
			GameObject instance = Instantiate(InstantiateOnEvent, position, Quaternion.identity);
			instance.GetComponentInChildren<MeshRenderer>().material.color = new Color(color.x, color.y, color.z);
			GameObject.Destroy(instance, 2.0f);
		}
	}
}
