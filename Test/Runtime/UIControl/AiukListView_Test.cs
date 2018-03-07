using System.Collections.Generic;
using AiukUnityRuntime.View;
using UnityEngine;

public class AiukListView_Test : MonoBehaviour
{
    private readonly List<string> m_TestStrs = new List<string>()
    {
        "0_贪玩蓝月好游戏！",
        "1_大家好，我是渣渣辉！",
        "2_大家好，我是古天乐！",
        "3_社会主义好！",
        "4_万恶的资本主义！",
        "5_大海啊，全是水！",
        "6_少壮不努力老大写程序！",
        "7_腾讯阿里巴巴都是爸爸！",
        "8_能投身铸造中国梦的伟业，！",
        "9_四个现代化！",
        "10_双击666！",
        "11_涛声依旧！",
        "12_I ll be back！",
        "13_社会我雷哥，人狠话不多！",
        "14_社会我雷哥，小米没有货！",
        "15_抗拒从严，回家过年！",
        "16_坦白从宽，牢底坐穿！",
        "17_没有什么是一包辣条解决不了的！",
        "18_如果有那就两包！",
        "19_天王盖地虎！",
        "20_宝塔镇河妖！",
        "21_哟哟，切克闹！",
    };

    //private IAiukListViewItemSet<string, AiukListView_TestMono> m_LvtSet;
    //private IAiukListViewBootstrap _mListViewBootstrap;

    private void Start()
    {
        NewListViewTest();
    }

    private AiukChatListView<string, AiukChatItem> chatListView;

    private void NewListViewTest()
    {
        var lvRect = GameObject.Find("example_listview_singleline").GetComponent<RectTransform>();
        chatListView = AiukAbsListView<string, AiukChatItem>.
            CreateListView<AiukChatListView<string, AiukChatItem>>(m_TestStrs);
        chatListView
            .SetPollDownUpdate(ls =>
            {
                if (newStrs.Count > 0)
                {
                    foreach (var newStr in newStrs)
                    {
                        chatListView.AddNetData(newStr);
                    }
                    newStrs.Clear();
                }
            })
            .Start(lvRect);
    }

    private List<string> newStrs = new List<string>()
    {
        "新数据1",
        "新数据2",
        "新数据3",
    };

    private readonly List<string> m_BackSts = new List<string>();
    private float m_ButtonWidth = 150f;
    private float m_ButtonHeight = 25f;
    private float m_ButtonLetf = 10f;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(m_ButtonLetf, 10, m_ButtonWidth, m_ButtonHeight), "添加数据到头部"))
        {
            chatListView.AddLast("新数据");
        }
        if (GUI.Button(new Rect(m_ButtonLetf + m_ButtonWidth + 5, 10, m_ButtonWidth, m_ButtonHeight), "删除数据"))
        {
        }
        if (GUI.Button(new Rect(m_ButtonLetf + m_ButtonWidth * 2 + 10, 10, m_ButtonWidth, m_ButtonHeight), "切换垂直滑动条显示"))
        {
        }
        if (GUI.Button(new Rect(m_ButtonLetf, 40, m_ButtonWidth, m_ButtonHeight), "切换水平滑动条显示"))
        {
        }
        if (GUI.Button(new Rect(m_ButtonLetf + m_ButtonWidth + 5, 40, m_ButtonWidth, m_ButtonHeight), "随机子项居中"))
        {

        }
    }
}