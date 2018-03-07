using UnityEngine;
using UnityEditor;
using System.IO;
using AuikEditor;
using AuikUnityCommon;

public class AiukVhtmlParser_Test
{
    [MenuItem("Aiuk/Vhtml_Test")]
    public static void AuikVhtmlParser_TestSimplePasses()
    {
        var auikLocation = EditorPrefs.GetString(AuikEditorPrefsKeys.AUIK_LOCATION);
        var vhtmlExampleFilePath = AiukUnityPath.GetUnityFullPath(auikLocation) + "/Src/AiukUnity/Runtime/View/VxmlExample.html";
        var vhtmlExampleCode = File.ReadAllText(vhtmlExampleFilePath);
        Debug.Log(vhtmlExampleCode);

        var parser = new AiukVhtmlParser();
        var vhtmlData = new AiukVhtmlParserData(vhtmlExampleCode);
        parser.ParseVxmlDataCode(vhtmlData);
    }
}
