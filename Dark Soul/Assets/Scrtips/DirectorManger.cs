using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManger : IActorMangerInterface
{

    public PlayableDirector pd;

    [Header("剧本")]
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
    /// 突刺执行函数，包含timeline内参数设置
    /// </summary>
    /// <param name="_name">剧本名称</param>
    /// <param name="attacer">攻击者</param>
    /// <param name="victim">受击者</param>
    public void FrontStab(string _name, ActorManger attacer, ActorManger victim)
    {
        if(pd.state == PlayState.Playing)
        {
            return;
        }
        if(_name == "StabFront")
        {        
            pd.playableAsset = Instantiate(frontStab);  //克隆一个剧本

            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;   //获取TimeLine资源

            foreach (var trackAssent in timeline.GetOutputTracks()) //GetOutputTracks()方法获取其中的轨道
            {
                if(trackAssent.name == "My Playable Track")
                {
                    pd.SetGenericBinding(trackAssent, attacer); //给轨道上的参数赋值
                    foreach (var clip in trackAssent.GetClips())    //获取Track上的Clip，GetClips()方法获取其中的块
                    {
                        MyPlayableClip playableClip = (MyPlayableClip)clip.asset;   //引入了插件脚本MyPlayableClip，所以是此类型
                        MyPlayableBehaviour playableBehaviour = playableClip.template;
 
                        //playableBehaviour.MyFloat = 666;
                        pd.SetReferenceValue(playableClip._myGameObject.exposedName, GameObject.Find("Player-Joker").GetComponent<ActorManger>());  //此处获取可能有问题，时间轴资源在Asset而物体在场景，所以获取用SetReferenceValue（）函数
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
            #region//此段注释中的代码只能设置TimeLine中的Track而不是Clip
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
    /// 开箱子函数，包含timeline内参数设置
    /// </summary>
    /// <param name="_name">剧本名称</param>
    /// <param name="attacer">攻击者</param>
    /// <param name="victim">受击者</param>
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
                    pd.SetGenericBinding(trackAssent, attacer); //给轨道上的参数赋值
                    foreach (var clip in trackAssent.GetClips())    //获取Track上的Clip，GetClips()方法获取其中的块
                    {
                        MyPlayableClip playableClip = (MyPlayableClip)clip.asset;   //引入了插件脚本MyPlayableClip，所以是此类型
                        MyPlayableBehaviour playableBehaviour = playableClip.template;

                        //playableBehaviour.MyFloat = 666;
                        pd.SetReferenceValue(playableClip._myGameObject.exposedName, GameObject.Find("Player-Joker").GetComponent<ActorManger>());  //此处获取可能有问题，时间轴资源在Asset而物体在场景，所以获取用SetReferenceValue（）函数
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
