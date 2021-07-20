using UnityEngine;
using System;
using System.IO;

namespace Helper
{
    public class FileUtils 
    {
        public static string GetCachedPath(string category)
        {
            return Application.temporaryCachePath+"/"+category;
        }

        public static void CreateDirectory(string folderPath)
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }

        public static bool ExistDirectory(string folderPath)
        {
            return System.IO.Directory.Exists(folderPath);
        }
        
        public static void DeleteFolder(string folderPath)
        {
            if (System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.Delete(folderPath, true);
            }
        }

        public static void CopyFile(string srcPath, string destPath)
        {
            File.Copy(srcPath, destPath);   
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public static bool ExistFile(string path)
        {
            return System.IO.File.Exists(path);
        }

        public static void ClearFile(string path)
        {
            File.WriteAllText(path, string.Empty);
        }

        public static void Write(string path, byte[] rawData, bool force = false)
        {
            try
            {
                ClearFile(path);
                
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    fs.Write(rawData, 0, rawData.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(string.Format("Exception caught in process: {0}", ex));
            }
        }


        public static void Write(string path, string data, bool force = false)
        {
            Write(path, System.Text.Encoding.UTF8.GetBytes(data), force);
        }

        public static byte[] Read(string path)
        {
            byte []rawData = null;

            try 
            {
                rawData = System.IO.File.ReadAllBytes(path);
            } catch(Exception e)
            {
                Debug.Log(e.ToString());
            }
            return rawData;
        }

        public static string ReadText(string path)
        {
            return  System.Text.Encoding.UTF8.GetString(Read(path));
        }

        // OR
        public static bool ExistFile(string[] paths)
        {
            for (int i = 0; i < paths.Length; ++i)
                if (ExistFile(paths[i])) return true;
            return false;
        }
    }
}
