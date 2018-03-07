using NUnit.Framework;
using Aiuk.Unity.Common;
using AiukUnityEditor;
using Assert = UnityEngine.Assertions.Assert;

public class AiukEditorStringPrefs_Test
{
    private const string TEST_VALUE = "AIUK_PREFS_STRING_VALUE";
    private const string TEST_KEY = "AIUK_PREFS_STRING_KEY";

    [Test]
    public void AiukStringPrefs_TestSimplePasses()
    {
        var stringPrefs = AiukEditorPrefsFactory.GetPrefs<AiukEditorStringPrefs, string>(
             TEST_KEY, TEST_VALUE);
        var fv = stringPrefs.GetValue();
        Assert.AreEqual(fv, TEST_VALUE);
    }
}
