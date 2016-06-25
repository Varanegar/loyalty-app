using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LoyaltyAppLibrary.App;
using System.IO;

namespace LoyaltyAndroid.Clients
{
    public class AndroidFileClient : IFileClient
    {
        public string GetDataLoction()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }

        public string GetInternetCacheLoction()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.InternetCache);
        }

        public bool WriteAllText(string content, string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            try
            {
                System.IO.File.WriteAllText(filePath, content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool WriteAllBytes(byte[] content, string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            try
            {
                System.IO.File.WriteAllBytes(filePath, content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ReadAllText(string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            return System.IO.File.ReadAllText(filePath);
        }

        public byte[] ReadAllBytes(string path, string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            return System.IO.File.ReadAllBytes(filePath);
        }

        public byte[] ReadAllBytes(string filePath)
        {
            return System.IO.File.ReadAllBytes(filePath);
        }

        public void DeleteFile(string path, string fileName)
        {
            System.IO.File.Delete(System.IO.Path.Combine(path, fileName));
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}