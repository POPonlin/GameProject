using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogManger : MonoBehaviour
{
    public TextAsset dialogData;
    public Image leftSprite;   
    public Text nameText;
    public Text dialogText;
    public GameObject continueUI;
    public GameObject OptionPrefab;
    public Transform OptionGroup;
    public GameObject UIManger;
    [Header("单个字体显示速度")]public float textSpeed=0.1f;
    private int index;
    private string[] dialogRows;

    private enum state
    {
        idle,
        underway,
        waitOption,
        end,
    }
    private state dialogState;
    
    void Start()
    {
        ReadText(dialogData);
        index=0;
        dialogState=state.idle;
        ShowDialogRow();
    }

    // Update is called once per frame
    void Update()
    {       
        ContinueUI();
        NextDialog();
        if(UIManger.activeSelf==true&&gameObject.activeSelf==true)
        {
            UIManger.SetActive(false);
        }
    }   

    /// <summary>
    /// 向名字和文本赋值
    /// </summary>
    /// <param name="name">人物名称</param>
    /// <param name="text">对话内容</param>
    private void UpDateText(string name,string text)
    {
        nameText.text=name;
        dialogText.text=text;
    }

    /// <summary>
    /// 读取文本文件
    /// </summary>
    /// <param name="textAsset">对话文件</param>
    private void ReadText(TextAsset textAsset)
    {
        dialogRows=textAsset.text.Split('\n');
    }

    /// <summary>
    /// 显示对话内容
    /// </summary>
    private void ShowDialogRow()
    {
        //foreach(var row in dialogRows)
        for(int i=0;i<dialogRows.Length;i++)
        {
            string[] cells = dialogRows[i].Split(',');
          
            if (cells[0]=="#"&&int.Parse(cells[1])==index)
            {
                //UpDateText(cells[2], cells[4]);
                dialogState=state.underway;

                StartCoroutine(text(cells[2],cells[4]));


                index=int.Parse(cells[5]);
                break;
            }
            else if(cells[0]=="&"&&int.Parse(cells[1])==index)
            {
                dialogState=state.waitOption;
                CreatOption(index);
            }
            else if(cells[0]=="END"&&int.Parse(cells[1])==index)
            {
                dialogState=state.end;
                gameObject.SetActive(false);
                UIManger.SetActive(true);
                Cursor.visible = false;
            }
        }
    }
   
    IEnumerator text(string name,string row)
    {
        dialogState=state.waitOption;
        var tempText = string.Empty;
        for(int i=0;i<row.Length+1;i++)
        {                       
            yield return new WaitForSeconds(textSpeed);

            tempText=row.Substring(0, i);

            UpDateText(name, tempText);                        
        }
        dialogState=state.underway;
    }

    /// <summary>
    /// 控制继续提示是否显示
    /// </summary>
    private void ContinueUI()
    {
        if(dialogState==state.underway)
        {
            continueUI.SetActive(true);
        }
        else
        {
            continueUI.SetActive(false);
        }
    }
    /// <summary>
    /// 对话过程按下E下一句
    /// </summary>
    private void NextDialog()
    {
        if(dialogState==state.underway)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ShowDialogRow();
            }
        }
    }
    /// <summary>
    /// 生成对话选项
    /// </summary>
    /// <param name="_index">起始选项id</param>
    private void CreatOption(int _index)
    {
        string[] cells = dialogRows[_index+1].Split(',');
        if (cells[0]=="&")
        {
            GameObject optionObject = Instantiate(OptionPrefab, OptionGroup);

            optionObject.GetComponentInChildren<Text>().text=cells[4];
            optionObject.GetComponent<Button>().onClick.AddListener(delegate { OptionClick(int.Parse(cells[5])); });
            CreatOption(_index+1);
        }
    }
    /// <summary>
    /// 点击选项后，销毁选项列表，并执行下一段对话
    /// </summary>
    /// <param name="_id">选择后要跳转的id</param>
    private void OptionClick(int _id)
    {
        index=_id;
        ShowDialogRow();
        for(int i=0;i<OptionGroup.childCount;i++)
        {
            Destroy(OptionGroup.GetChild(i).gameObject);
        }
    }
}
