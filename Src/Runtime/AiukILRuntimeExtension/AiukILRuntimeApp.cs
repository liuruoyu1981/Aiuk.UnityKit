using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILRuntime.Runtime.Enviorment;
using System.IO;

public class AiukILRuntimeApp : MonoBehaviour
{
    private AppDomain m_AppDomain;

    [SerializeField]
    private TextAsset m_TextAsset;
    public TextAsset TextAsset { get { return m_TextAsset; } }

    private void Start()
    {
        m_AppDomain = new AppDomain();

        if (TextAsset != null)
        {
            using (var fs = new MemoryStream(TextAsset.bytes))
            {
                m_AppDomain.LoadAssembly(fs);
            }
        }
        else
        {

        }

        OnHotFixLoaded();
    }

    protected void InitializeILRuntime()
    {

    }

    protected void OnHotFixLoaded()
    {
        m_AppDomain.Invoke("ThreeKK.ILRuntimeTest", "StaticFunctionTest", null, null);
    }

}
