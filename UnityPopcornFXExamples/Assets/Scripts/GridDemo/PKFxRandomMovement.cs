using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PKFxRandomMovement : MonoBehaviour
{
	[SerializeField] private GameObject		m_PlayerDummyPrefab;
	[SerializeField] private GameObject		m_LeftPoint;
	[SerializeField] private GameObject		m_RightPoint;
	[SerializeField] private float			m_Z_MovementAmount;
					 private AnimationCurve	m_Curve;
					 private GameObject		m_PlayerDummy;
					 private int			m_KeysNumber;
					 private bool			m_IsLeft;
					 private bool			m_IsMoving;

	private void Start()
	{
		m_PlayerDummy = Instantiate(m_PlayerDummyPrefab, m_LeftPoint.transform.position, Quaternion.identity);
		m_PlayerDummy.transform.parent = transform;
		m_IsLeft = true;
		m_IsMoving = false;
		m_KeysNumber = 5;
	}

	public void StartRandomMovement()
	{
		if (m_IsMoving)
			return;
		m_Curve = GetRandomAnimatedCurve(m_KeysNumber);
		if (m_IsLeft)
			StartCoroutine(Move(m_LeftPoint.transform.position, m_RightPoint.transform.position, 1));
		else
			StartCoroutine(Move(m_RightPoint.transform.position, m_LeftPoint.transform.position, 1));
	}

	AnimationCurve GetRandomAnimatedCurve(int keysNumber)
	{
		AnimationCurve	curve = new();
		for (int i = 1; i <= keysNumber; i++)
		{
			float rand = Random.Range(-1f, 1f);
			curve.AddKey(1f/(keysNumber+1)*i, rand);
		}
		curve.AddKey(0f, 0f);
		curve.AddKey(1f, 0f);
		return curve;
	}

	IEnumerator Move(Vector3 beginPos, Vector3 endPos, float duration)
	{
		m_IsMoving = true;
		for (float t = 0; t < 1; t += Time.deltaTime / duration)
		{
			m_PlayerDummy.transform.position = Vector3.Lerp(beginPos, endPos, t);
			m_PlayerDummy.transform.position += new Vector3(0,0, m_Curve.Evaluate(t/duration) * m_Z_MovementAmount);
			yield return null;
		}
		m_IsMoving = false;
		m_IsLeft = !m_IsLeft;
	}
}
