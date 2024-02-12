using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManger : IActorMangerInterface
{

    public PlayableDirector pd;

    [Header("�籾")]
    public TimelineAsset frontStab;
    public TimelineAsset openBox;

    [Space]
    public ActorManger attacer;
    public ActorManger victim;

    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ͻ��ִ�к���������timeline�ڲ�������
    /// </summary>
    /// <param name="_name">�籾����</param>
    /// <param name="attacer">������</param>
    /// <param name="victim">�ܻ���</param>
    public void FrontStab(string _name, ActorManger attacer, ActorManger victim)
    {
        if(pd.state == PlayState.Playing)
        {
            return;
        }
        if(_name == "StabFront")
        {        
            pd.playableAsset = Instantiate(frontStab);  //��¡һ���籾

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;   //��ȡTimeLine��Դ

            foreach (var trackAssent in timeline.GetOutputTracks()) //GetOutputTracks()������ȡ���еĹ��
            {
                if(trackAssent.name == "My Playable Track")
                {
                    pd.SetGenericBinding(trackAssent, attacer); //������ϵĲ�����ֵ
                    foreach (var clip in trackAssent.GetClips())    //��ȡTrack�ϵ�Clip��GetClips()������ȡ���еĿ�
                    {
                        MyPlayableClip playableClip = (MyPlayableClip)clip.asset;   //�����˲���ű�MyPlayableClip�������Ǵ�����
                        MyPlayableBehaviour playableBehaviour = playableClip.template;
 
                        //playableBehaviour.MyFloat = 666;
                        pd.SetReferenceValue(playableClip._myGameObject.exposedName, GameObject.Find("Player-Joker").GetComponent<ActorManger>());  //�˴���ȡ���������⣬ʱ������Դ��Asset�������ڳ��������Ի�ȡ��SetReferenceValue��������
                    }
                }
                else if(trackAssent.name == "My Playable Track (1)")
                {
                    pd.SetGenericBinding(trackAssent, victim);
                    foreach (var clip in trackAssent.GetClips())
                    {
                        MyPlayableClip playableClip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour playableBehaviour = playableClip.template;
                  
                        //playableBehaviour.MyFloat = 777;
                        pd.SetReferenceValue(playableClip._myGameObject.exposedName, GameObject.Find("Enemy-Cyclon").GetComponent<ActorManger>());
                        //Debug.Log(playableClip._myGameObject.exposedName);
                    }
                }
                else if(trackAssent.name == "Animation Track")
                {
                    pd.SetGenericBinding(trackAssent, attacer.transform.GetComponentInChildren<Animator>());                  
                }
                else if(trackAssent.name == "Animation Track (1)")
                {
                    pd.SetGenericBinding(trackAssent, victim.gameObject.transform.GetComponentInChildren<Animator>());
                }   
            }
            #region//�˶�ע���еĴ���ֻ������TimeLine�е�Track������Clip
            //foreach (var playableBinding in pd.playableAsset.outputs)
            //{
            //    if (playableBinding.streamName == "Animation Track")
            //    {
            //        pd.SetGenericBinding(playableBinding.sourceObject, attacer.transform.GetComponentInChildren<Animator>());
            //    }
            //    else if (playableBinding.streamName == "Animation Track (1)")
            //    {
            //        pd.SetGenericBinding(playableBinding.sourceObject, victim.gameObject.transform.GetComponentInChildren<Animator>());
            //    }
            //    else if (playableBinding.streamName == "My Playable Track")
            //    {
            //        pd.SetGenericBinding(playableBinding.sourceObject, attacer);
            //    }
            //    else if (playableBinding.streamName == "My Playable Track (1)")
            //    {
            //        pd.SetGenericBinding(playableBinding.sourceObject, victim);
            //    }
            //}
            #endregion
            pd.Evaluate();

            pd.Play();
        }
    }

    /// <summary>
    /// �����Ӻ���������timeline�ڲ�������
    /// </summary>
    /// <param name="_name">�籾����</param>
    /// <param name="attacer">������</param>
    /// <param name="victim">�ܻ���</param>
    public void BoxOpen(string _name, ActorManger attacer, ActorManger victim)
    {
        if(pd.state == PlayState.Playing)
        {
            return;
        }

        if(_name == "OpenBox")
        {
            pd.playableAsset = Instantiate(openBox);
            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;
            foreach (var trackAssent in timelineAsset.GetOutputTracks())
            {
                if(trackAssent.name == "My Playable Track")
                {
                    pd.SetGenericBinding(trackAssent, attacer); //������ϵĲ�����ֵ
                    foreach (var clip in trackAssent.GetClips())    //��ȡTrack�ϵ�Clip��GetClips()������ȡ���еĿ�
                    {
                        MyPlayableClip playableClip = (MyPlayableClip)clip.asset;   //�����˲���ű�MyPlayableClip�������Ǵ�����
                        MyPlayableBehaviour playableBehaviour = playableClip.template;

                        //playableBehaviour.MyFloat = 666;
                        pd.SetReferenceValue(playableClip._myGameObject.exposedName, GameObject.Find("Player-Joker").GetComponent<ActorManger>());  //�˴���ȡ���������⣬ʱ������Դ��Asset�������ڳ��������Ի�ȡ��SetReferenceValue��������
                    }
                }
                else if (trackAssent.name == "My Playable Track (1)")
                {
                    pd.SetGenericBinding(trackAssent, victim);
                    foreach (var clip in trackAssent.GetClips())
                    {
                        MyPlayableClip playableClip = (MyPlayableClip)clip.asset;
                        MyPlayableBehaviour playableBehaviour = playableClip.template;                
                        //playableBehaviour.MyFloat = 777;
                        pd.SetReferenceValue(playableClip._myGameObject.exposedName, GameObject.Find("Box").GetComponent<ActorManger>());
                        //Debug.Log(playableClip._myGameObject.exposedName);
                    }
                }
                else if (trackAssent.name == "Animation Track")
                {
                    pd.SetGenericBinding(trackAssent, attacer.transform.GetComponentInChildren<Animator>());
                }
                else if (trackAssent.name == "Animation Track (1)")
                {
                    pd.SetGenericBinding(trackAssent, victim.gameObject.transform.GetComponentInChildren<Animator>());
                }
            }
            pd.Evaluate();

            pd.Play();       
        }
    }
}
