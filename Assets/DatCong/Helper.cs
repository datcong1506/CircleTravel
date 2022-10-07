using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace DatCong
{
    public static class Helper
    {
        public static T LoadData<T>(string path) where T : class
        {
            T result=null;
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
            }
            return result;
        }

        public static void WriteData<T>(T data, string path)where T : class
        {
            File.WriteAllText(path,JsonUtility.ToJson(data));
        }

        public static void DeleteData(string path)
        {
            File.Delete(path);
        }




        public static Dictionary<TKey, TValue> ConvertListToDic<TKey, TValue>(this  List<ListToDictionNary<TKey,TValue>> origin)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            for (int i = 0; i < origin.Count; i++)
            {
                result.Add(origin[i].Key,origin[i].Value);
            }

            return result;
        }
        
    }
    
    
    // T is Key type
    [Serializable]
    public class ListToDictionNary<TKey,TValue> where TValue : notnull
    {
        public TKey Key;
        public TValue Value;
    } 
    
    
}



