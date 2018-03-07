using UnityEngine;
using UnityEngine.UI;

public class AiukListView_TestMono : MonoBehaviour
{
    public Text Text;

    public void Init()
    {
        Text = transform.Find("Text").GetComponent<Text>();
    }
}