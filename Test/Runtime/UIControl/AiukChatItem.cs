using AiukUnityRuntime.View;
using UnityEngine;
using UnityEngine.UI;

public class AiukChatItem : IListViewItem<string>
{
    private Text m_Text;


    public void Init(RectTransform rect)
    {
        m_Text = rect.Find("Text").GetComponent<Text>();
    }

    public void UpdateDraw(string data)
    {
        m_Text.text = data;
    }
}
