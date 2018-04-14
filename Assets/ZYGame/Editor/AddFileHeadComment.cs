using UnityEditor;
using UnityEngine;
using System.IO;

public class AddFileHeadComment : UnityEditor.AssetModificationProcessor
{
    /// <summary>  
    /// 此函数在asset被创建完，文件已经生成到磁盘上，但是没有生成.meta文件和import之前被调用  
    /// </summary>  
    /// <param name="newFileMeta">newfilemeta 是由创建文件的path加上.meta组成的</param>  
    //public static void OnWillCreateAsset(string newFileMeta)
    //{
    //    string newFilePath = newFileMeta.Replace(".meta", "");
    //    string fileExt = Path.GetExtension(newFilePath);
    //    if (fileExt != ".cs")
    //    {
    //        return;
    //    }
    //    //注意，Application.datapath会根据使用平台不同而不同  
    //    string realPath = Application.dataPath.Replace("Assets", "") + newFilePath;
    //    string scriptContent = File.ReadAllText(realPath);

    //    //这里实现自定义的一些规则  
    //    scriptContent = scriptContent.Replace("#PROJECTSNAME#", PlayerSettings.productName);
    //    scriptContent = scriptContent.Replace("#YEAR#",System.DateTime.Now.Year.ToString());
    //    scriptContent = scriptContent.Replace("#COMPANY#", "xxx公司");
    //    scriptContent = scriptContent.Replace("#SCRIPTFULLNAME#", Path.GetFileName(newFilePath));
    //    scriptContent = scriptContent.Replace("#AUTHOR#", "作者");
    //    scriptContent = scriptContent.Replace("#VERSION#", Application.version);
    //    scriptContent = scriptContent.Replace("#UNITYVERSION#", Application.unityVersion);
    //    scriptContent = scriptContent.Replace("#DATE#", System.DateTime.Now.ToString("yyyy-MM-dd"));
    //    scriptContent = scriptContent.Replace("#DESCRIPTION#", "项目名称");
    //    scriptContent = scriptContent.Replace("#HISTORY#", "自定义，可以不写内容，可以删除该行");
    //    File.WriteAllText(realPath, scriptContent);
    //}

}
