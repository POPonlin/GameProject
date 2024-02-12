using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyPlayableBehaviour : PlayableBehaviour
{
    public ActorManger am;
    public float MyFloat;
    public override void OnPlayableCreate (Playable playable)
    {

    }

    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);
        //pd = (PlayableDirector)playable.GetGraph().GetResolver(); //���������ȥץ����ץ�����ˡ�����Ҫ�ڿ�ʼȥץ
    }

    public override void OnGraphStop(Playable playable)
    {
        //base.OnGraphStop(playable);                
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);
        am.LockALLState(true);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        base.OnBehaviourPause(playable, info);
        am.LockALLState(false);
    }
    
}
