using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public static class FileHelper
{
    /// <summary>
    /// 动态创建文件夹.
    /// </summary>
    /// <returns>The folder.</returns>
    /// <param name="path">文件创建目录.</param>
    /// <param name="FolderName">文件夹名(不带符号).</param>
    public static string CreateFolder(string path, string FolderName)
    {
        string FolderPath = path + FolderName;
        if (!Directory.Exists(FolderPath))
        {
            Directory.CreateDirectory(FolderPath);
        }
        return FolderPath;
    }

    /// <summary>
    /// 动态创建文件夹.
    /// </summary>
    /// <returns>The folder.</returns>
    /// <param name="path">文件夹路径</param>
    /// <param name="FolderName">文件夹名(不带符号).</param>
    public static string CreateFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }

    /// <summary>
    /// 创建文件.
    /// </summary>
    /// <param name="path">文件全局路径</param>
    /// <param name="bytes">写入的内容.</param>
    /// <param name="length">写入长度.</param>
    public static void CreateFile(string path, byte[] bytes, int length)
    {
        try
        {
            File.WriteAllBytes(path, bytes);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("FileHelper.CreateFile(" + path + ", ...) is Falid.\n" + ex.Message);
        }
    }

    public static void CreateFile(string path, string data)
    {
        try
        {
            File.WriteAllText(path,data);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("FileHelper.CreateFile(" + path + ", ...) is Falid.\n" + ex.Message);
        }
    }

    public static string ReadFile(string path)
    {
        string data = "";
        try
        {
            data = File.ReadAllText(path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("FileHelper.CreateFile(" + path + ", ...) is Falid.\n" + ex.Message);
        }
        return data;
    }

    public static byte[] ReadFileBytes(string path)
    {
        byte[] bytes;
        try
        {
            bytes = File.ReadAllBytes(path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("FileHelper.CreateFile(" + path + ", ...) is Falid.\n" + ex.Message);
            bytes = new byte[0];
        }
        return bytes;
    }
    /// <summary>
    /// 获取文件夹下所有文件的信息
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<FileInfo> GetFilesInfo(string path,string suffix="")
    {
        if (string.IsNullOrEmpty(suffix))
            suffix = "*";
        string[] files = Directory.GetFiles(path, suffix);

        List<FileInfo> list = new List<FileInfo>();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = new FileInfo(files[i]);
            list.Add(file);
        }
        return list;
    }

    /// <summary>
    /// 获取文件夹下文件的数量
    /// </summary>
    /// <param name="path"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static int GetFilesCount(string path, string suffix = "")
    {
        if (string.IsNullOrEmpty(suffix))
            suffix = "*";
        string[] files = Directory.GetFiles(path, suffix);
        return files.Length;
    }

    /// <summary>
    ///   拷贝文件
    /// </summary>
    public static void CopyFile(string src, string dest, bool overwrite = false)
    {
        //不存在则返回
        if (!File.Exists(src))
            return;

        //保证路径存在
        string directory = FileHelper.GetDirectory(dest);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        File.Copy(src, dest, overwrite);
    }

    /// <summary>
    /// 读取文件.
    /// </summary>
    /// <returns>The file.</returns>
    /// <param name="path">完整文件夹路径.</param>
    /// <param name="name">读取文件的名称.</param>
    public static ArrayList LoadFile(string path, string name)
    {
        //使用流的形式读取
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + name);
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }

        string line;
        ArrayList arrlist = new ArrayList();
        while ((line = sr.ReadLine()) != null)
        {
            //一行一行的读取
            //将每一行的内容存入数组链表容器中
            arrlist.Add(line);
        }
        //关闭流
        sr.Close();
        //销毁流
        sr.Dispose();
        //将数组链表容器返回
        return arrlist;
    }

    /// <summary>
    /// 获取文件下所有文件大小
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static int GetAllFileSize(string filePath)
    {
        int sum = 0;
        if (!Directory.Exists(filePath))
        {
            return 0;
        }

        DirectoryInfo dti = new DirectoryInfo(filePath);

        FileInfo[] fi = dti.GetFiles();

        for (int i = 0; i < fi.Length; ++i)
        {
            sum += Convert.ToInt32(fi[i].Length / 1024);
        }

        DirectoryInfo[] di = dti.GetDirectories();

        if (di.Length > 0)
        {
            for (int i = 0; i < di.Length; i++)
            {
                sum += GetAllFileSize(di[i].FullName);
            }
        }
        return sum;
    }

    /// <summary>
    /// 获取指定文件大小
    /// </summary>
    /// <param name="FilePath"></param>
    /// <param name="FileName"></param>
    /// <returns></returns>
    public static long GetFileSize(string FilePath, string FileName)
    {
        long sum = 0;
        if (!Directory.Exists(FilePath))
        {
            return 0;
        }
        else
        {
            FileInfo Files = new FileInfo(@FilePath + FileName);
            sum += Files.Length;
        }
        return sum;
    }

    /// <summary>
    ///   创建本地AssetBundle文件
    /// </summary>
    /// <param name="path">文件全局路径</param>
    /// <param name="bytes">写入的内容.</param>
    /// <param name="length">写入长度.</param>
    static void CreateassetbundleFile(string path, byte[] bytes, int length)
    {
        try
        {
            //文件流信息
            Stream sw;
            FileInfo t = new FileInfo(path);
            //打开文件
            sw = t.Open(FileMode.Create, FileAccess.ReadWrite);
            if (bytes != null && length > 0)
            {
                //以行的形式写入信息
                sw.Write(bytes, 0, length);
            }

            //关闭流
            sw.Close();
            //销毁流
            sw.Dispose();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("FileHelper.CreateFile(" + path + ", ...) is Falid.\n" + ex.Message);
        }
    }

    /// <summary>
    ///   读取本地AssetBundle文件
    /// </summary>
    static IEnumerator LoadAssetbundleFromLocal(string path, string name)
    {
        WWW w = new WWW("file:///" + path + "/" + name);

        yield return w;

        if (w.isDone)
        {
            GameObject.Instantiate(w.assetBundle.mainAsset);
        }
    }

    /// <summary>
    ///   
    /// </summary>
    public static IEnumerator CopyStreamingAssetsToFile(string src, string dest, Action action = null)
    {
        using (WWW w = new WWW(src))
        {
            yield return w;

            if (string.IsNullOrEmpty(w.error))
            {
                while (w.isDone == false)
                    yield return null;

                //保证路径存在
                string directory = FileHelper.GetDirectory(dest);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                FileHelper.CreateFile(dest, w.bytes, w.bytes.Length);
                if (action != null)
                    action();
            }
            else
            {
                Debug.LogWarning(w.error);
            }
        }
    }

    /// <summary>
    /// 删除文件.
    /// </summary>
    /// <param name="path">删除完整文件夹路径.</param>
    /// <param name="name">删除文件的名称.</param>
    public static void DeleteFile(string path, string name)
    {
        File.Delete(path + name);
    }

    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="filesName"></param>
    /// <returns></returns>
    public static bool DeleteFiles(string path, string filesName)
    {
        bool isDelete = false;
        try
        {
            if (Directory.Exists(path))
            {
                if (File.Exists(path + "\\" + filesName))
                {
                    File.Delete(path + "\\" + filesName);
                    isDelete = true;
                }
            }
        }
        catch
        {
            return isDelete;
        }
        return isDelete;
    }

    /// <summary>
    ///   删除文件夹下所有子文件夹与文件
    /// </summary>
    public static void DeleteAllChild(string path, FileAttributes filter)
    {
        if (!Directory.Exists(path))
            return;

        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] files = dir.GetFiles("*");
        for (int i = 0; i < files.Length; ++i)
        {
            if ((files[i].Attributes & filter) > 0)
                continue;
            if (File.Exists(files[i].FullName))
                File.Delete(files[i].FullName);
        }
        DirectoryInfo[] dirs = dir.GetDirectories("*");
        for (int i = 0; i < dirs.Length; ++i)
        {
            if ((dirs[i].Attributes & filter) > 0)
                continue;

            if (Directory.Exists(dirs[i].FullName))
                Directory.Delete(dirs[i].FullName, true);
        }
    }

    /// <summary>
    ///   获得路径的最后文件夹路径
    /// </summary>
    public static string GetDirectory(string file_full_name)
    {
        int last_idx = file_full_name.LastIndexOfAny("/\\".ToCharArray());
        if (last_idx < 0)
            return "";

        return file_full_name.Substring(0, last_idx);
    }

    /// <summary>
    ///   绝对路径转相对路径
    /// </summary>
    public static string AbsoluteToRelativePath(string root_path, string absolute_path)
    {
        absolute_path = absolute_path.Replace('\\', '/');
        int last_idx = absolute_path.LastIndexOf(root_path);
        if (last_idx < 0)
            last_idx = absolute_path.ToLower().LastIndexOf(root_path.ToLower());
        if (last_idx < 0)
            return absolute_path;

        int start = last_idx + root_path.Length;
        int length = absolute_path.Length - start;
        return absolute_path.Substring(start, length);
    }

    /// <summary>
    ///   获得取除路径扩展名的路径
    /// </summary>
    public static string GetPathWithoutExtension(string full_name)
    {
        int last_idx = full_name.LastIndexOfAny(".".ToCharArray());
        if (last_idx < 0)
            return full_name;

        return full_name.Substring(0, last_idx);
    }
}


