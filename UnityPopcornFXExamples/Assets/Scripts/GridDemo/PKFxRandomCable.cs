using PopcornFX;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class PKFxRandomCable : MonoBehaviour
{
    [SerializeField] private GameObject             m_PlugsParent;
    [SerializeField] private PKFxEffectAsset        m_CableFxSource;
    [SerializeField] private List<Color>            m_CableColors;
                     private List<PKFxCablePlug>    m_CablePlugs = new();
                     private List<PlugPair>         m_PlugPairs = new();
                     private List<PlugPair>         m_CablePairs = new();
                     private List<PKFxEmitter>      m_Emitters = new();

    private struct PlugPair
    {
		public PlugPair(PKFxCablePlug plug1, PKFxCablePlug plug2)
		{
            _plug1 = plug1;
            _plug2 = plug2;
		}

        public PKFxCablePlug Plug1 => _plug1;
        public PKFxCablePlug Plug2 => _plug2;

        private PKFxCablePlug _plug1;
		private PKFxCablePlug _plug2;
    }

	private void Start()
	{
        m_CablePlugs = m_PlugsParent.GetComponentsInChildren<PKFxCablePlug>().ToList();
        SetRandomCables();
    }

    public void SetRandomCables()
    {
        ClearAllEmitters();

        //Create random pairs of colors
        m_PlugPairs = GetRandomPlugPairs(m_CablePlugs, true);

        //Make sure at least 1 plug pair is linked
        CreateCable(m_PlugPairs[0]);
        List<PKFxCablePlug> newCablePlugs = new(m_CablePlugs);
        newCablePlugs.Remove(m_PlugPairs[0].Plug1);
        newCablePlugs.Remove(m_PlugPairs[0].Plug2);

        //Link randomly the rest of plugs
        m_CablePairs = GetRandomPlugPairs(newCablePlugs);
        foreach (PlugPair pair in m_CablePairs)
        {
            CreateCable(pair);
        }
    }

    private List<PlugPair> GetRandomPlugPairs(List<PKFxCablePlug> cablePlugs, bool setPlugColor = false)
    {
        if (cablePlugs == null || cablePlugs.Count < 2) 
            return null;
        System.Random rnd = new System.Random();
        cablePlugs = cablePlugs.OrderBy(_ => rnd.Next()).ToList();
        m_CableColors = m_CableColors.OrderBy(_ => rnd.Next()).ToList();

        List<PlugPair> plugPairs = new List<PlugPair>();
        for (int i = 1; i < cablePlugs.Count(); i += 2)
        {
            PlugPair newPlugPair = new PlugPair(cablePlugs[i - 1], cablePlugs[i]);
            if (setPlugColor)
            {
                Color cableColor;
                if (i < m_CableColors.Count())
                    cableColor = m_CableColors[i];
                else
                    cableColor = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                newPlugPair.Plug1.SetColor(cableColor);
                newPlugPair.Plug2.SetColor(cableColor);
            }
            plugPairs.Add(newPlugPair);
        }
        return plugPairs;
    }

    private void CreateCable(PlugPair pair)
    {
        var emitterObject = new GameObject("CableEffect-" + (m_Emitters.Count+1));
        emitterObject.transform.parent = transform;
        PKFxEmitter emitter = emitterObject.AddComponent<PKFxEmitter>();
        m_Emitters.Add(emitter);
        emitter.UpdateEffectAsset(m_CableFxSource, true);
        emitter.StartEffect();

        //Set Effect Properties
        emitter.SetAttributeSafe(emitter.GetAttributeId("ExtremityA", PopcornFX.EAttributeType.Float3), pair.Plug1.Target.position);
        emitter.SetAttributeSafe(emitter.GetAttributeId("ExtremityB", PopcornFX.EAttributeType.Float3), pair.Plug2.Target.position);
        emitter.SetAttributeSafe(emitter.GetAttributeId("Color1", PopcornFX.EAttributeType.Float4), pair.Plug1.Color);
        emitter.SetAttributeSafe(emitter.GetAttributeId("Color2", PopcornFX.EAttributeType.Float4), pair.Plug2.Color);
        emitter.SetAttributeSafe(emitter.GetAttributeId("Width", PopcornFX.EAttributeType.Float), .05f);
        emitter.SetAttributeSafe(emitter.GetAttributeId("RopeLength", PopcornFX.EAttributeType.Float), 3f);
        if(pair.Plug1.Color != pair.Plug2.Color)
            StartCoroutine(KillEmitter(emitter, 1f));
    }

    private void ClearAllEmitters()
    {
        if(m_Emitters == null || m_Emitters.Count == 0)
            return;
        foreach(PKFxEmitter emitter in m_Emitters)
        {
            StartCoroutine(KillEmitter(emitter, 0f));
        }
        m_Emitters.Clear();
    }

    IEnumerator KillEmitter(PKFxEmitter emitter, float duration)
    {
        emitter.SetAttributeSafe(emitter.GetAttributeId("Lifetime", PopcornFX.EAttributeType.Float), duration);
        yield return new WaitForSeconds(duration);
        m_Emitters.Remove(emitter);
        emitter.StopEffect();
        Destroy(emitter);
        Destroy(emitter.gameObject);
    }
}
