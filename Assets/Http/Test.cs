using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System.Text;
using UnityEngine.UI;
using System.Data;

public class Test : MonoBehaviour
{
    /// <summary>
    /// 用来显示返回数据
    /// </summary>
    public Text m_Text;

    public static Test Instance;

    public List<BllTreeNodeInfo> bllTreeNodes;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        bllTreeNodes = new List<BllTreeNodeInfo>();
        // List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("set=foo&get=foo"));
        // formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("", "");

        //C_UnityWebRequest.Instance.Get("http://172.26.1.109:8080/dsa5200/tree/findOrgTree", aaa);
        C_UnityWebRequest.Instance.Post("http://172.26.1.109:8080/dsa5200/tree/bill_list", wWForm, aaa);
    }
    public void aaa(UnityWebRequest a)
    {
        JsonData jd = JsonMapper.ToObject(a.downloadHandler.text);
        JsonData jsons = jd["data"];

        bllTreeNodes = GetAllTreeInfo(jsons);
        Debug.Log("进入回调函数：" + a.downloadHandler.text);
    }

    private List<BllTreeNodeInfo> GetAllTreeInfo(JsonData ddd)
    {
        List<BllTreeNodeInfo> lstNodes = new List<BllTreeNodeInfo>();
        if (ddd != null && ddd.Count > 0)
        {
            for (int i = 0; i < ddd.Count; i++)
            {
                BllTreeNodeInfo node = new BllTreeNodeInfo()
                {
                    NodeName = ddd[i]["name"].ToString(),
                    TreeID = ddd[i]["id"].ToString(),
                    TreeParentID = ddd[i]["pid"].ToString(),
                };
                lstNodes.Add(node);
            }
        }
        return lstNodes;
    }
}