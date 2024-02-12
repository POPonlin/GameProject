using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyPlayableClip : PlayableAsset, ITimelineClipAsset
{
    public MyPlayableBehaviour template = new MyPlayableBehaviour ();
    public ExposedReference<ActorManger> _myGameObject;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MyPlayableBehaviour>.Create (graph, template);
        MyPlayableBehaviour clone = playable.GetBehaviour ();
        _myGameObject.exposedName = GetInstanceID().ToString(); //�����¼ӣ���ʼ��exposedName��GetInstanceID()��Ҫת���ַ�������
        clone.am = _myGameObject.Resolve (graph.GetResolver ());
        return playable;
    }
}
