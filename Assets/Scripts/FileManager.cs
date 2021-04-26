using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager
{
    /// <summary>
    /// ファイルを作る
    /// </summary>
    /// <param name="textName"></param>
    public static void CreateFile(string textName)
    {
        Debug.Log($"ファイル{GetFilePath(textName)}を作ります。");
        File.Create(GetFilePath(textName));
    }

    /// <summary>
    /// ファイルの上書き保存(ファイルが見つからない場合は作成し、上書き保存する)
    /// </summary>
    /// <param name="textName">ファイル名</param>
    /// <param name="text">保存するテキストデータ</param>
    public static void TextSave(string textName ,string text)
    {
        using (var writer = new StreamWriter(GetFilePath(textName), append : false) )
        {
            Debug.Log($"ファイル{GetFilePath(textName)}");
            writer.Write(text);
        }
    }

    /// <summary>
    /// ファイルを読み取る
    /// </summary>
    /// <param name="textName"></param>
    /// <returns></returns>
    public static string TextLoad(string textName)
    {
        string text = "";
        try
        {
            using (var reader = new StreamReader(GetFilePath(textName)))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Debug.Log(line);
                    text += line;
                }
            }
        }
        catch (FileNotFoundException ex)
        {

            Debug.Log($"{ex}のファイルが見つかりませんでした。");
        }
        return text;
    }

    /// <summary>
    /// ファイルのPathを返す
    /// </summary>
    /// <param name="textName"></param>u
    /// <returns></returns>
    public static string GetFilePath(string textName)
    {
        string filePath = Application.persistentDataPath + "/" + (textName == "" ? Application.productName : textName) + ".json";
        return filePath;
    }
}
