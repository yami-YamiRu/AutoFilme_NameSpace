using UnityEngine;
using UnityEditor;
using System.IO;

public class NamespaceBatchAdder : MonoBehaviour
{
    [MenuItem("Tool/Add NameSpace to Selected Folder")]
    private static void AddNameSpaceToSelectedFolder()
    {
        //projectビューで選択されているフォルダーを取得
        var selected = Selection.activeObject;
        string folderPath = AssetDatabase.GetAssetPath(selected);

        if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogWarning("フォルダーを選択して下ださい/Please select a folder.");
            return;
        }

        //ファイルパスシステムの絶対パスに変換
        //Convert to absolute path in the file path system
        string fullPath = Path.Combine(Application.dataPath, folderPath.Substring("Assets/".Length));

        string[] csFiles = Directory.GetFiles(fullPath, "*.cs", SearchOption.AllDirectories);

        foreach (var file in csFiles)
        {
            string scriptText = File.ReadAllText(file);

            //すでに"namespace"があればスキップ
            //If a namespace already exists, skip it.
            if (scriptText.Contains("namespace")) continue;

            //選択したフォルダーを基点に"namespace"を作成
            //Create a namespace based on the selected folder
            string relativePath = file.Replace(Application.dataPath + Path.DirectorySeparatorChar, "");

            string nsPath = Path.GetDirectoryName(relativePath);

            string namespaceName = nsPath.Replace(Path.DirectorySeparatorChar, '.');

            //IndentCodeは53_65に記載
            //IndentCode is listed in 53_65<==
            string wrappedScript 
                = $"namespace {namespaceName} \n{{\n{IndentCode(scriptText)}\n}}";

            File.WriteAllText(file, wrappedScript);
            
            //Debug_Log
            Debug.Log("$Added namespace '{namespaceName}'to {relativePath}");
            Debug.Log($"Target file: {file}");
            Debug.Log($"Namespace: {namespaceName}");
        }

        AssetDatabase.Refresh();

    }


    ///<summary>
    ///インデント整形(1段下げる)
    ///Indent formatting (Indent one level)
    ///</summary>
    private static string IndentCode(string code)
    {
        string[] lines = code.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(lines[i]))
                lines[i] = "    " + lines[i]; //4スペース分インデント/Indent by 4 spaces
        }
        return string.Join("\n", lines);
    }
}
