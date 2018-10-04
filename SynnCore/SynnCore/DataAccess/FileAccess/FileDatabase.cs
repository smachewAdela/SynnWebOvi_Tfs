﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess
{
    public class FileDatabase
    {
        private FileStream fs = null;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        private string data_path = "";
        /// <summary>
        /// Get or Set the path of the text file!
        /// </summary>
        public string Path
        {
            get { return data_path; }
            set { data_path = value; }
        }
        public int Entries()
        {
            int count = 0;
            CreateConfigFile();
            try
            {
                fs = new FileStream(data_path, FileMode.Open);
                if (fs.Length > 0)
                {
                    sr = new StreamReader(fs);
                    while (sr.ReadLine() != null)
                    {
                        count++;
                    }
                    sr.Close();
                }
                fs.Close();
            }
            catch
            {
                count = 0;
            }
            return count;
        }
        public string Select(string key, int id)
        {
            int count = 0;
            CreateConfigFile();
            string result = "";
            try
            {
                fs = new FileStream(data_path, FileMode.Open);
                sr = new StreamReader(fs);
                string temp = "";
                bool cond = true;
                while (cond == true)
                {

                    if ((temp = sr.ReadLine()) == null)
                    {
                        sr.Close();
                        fs.Close();
                        cond = false;
                    }
                    else
                    {

                        string[] stringSplit = temp.Split(';');
                        int _maxIndex = stringSplit.Length;
                        string column = stringSplit[id];
                        if (column.Contains(key))
                        {
                            result += count.ToString() + ";";
                        }
                        count++;
                    }


                }

                sr.Close();
                fs.Close();
                return result;
            }
            catch
            {
                return "ERROR";
            }

        }
        public bool ReadEntrie(int id, ref string name, ref string lastname, ref string phone, ref string mail, ref string website)
        {
            int count = 0;
            CreateConfigFile();
            try
            {
                fs = new FileStream(data_path, FileMode.Open);
                sr = new StreamReader(fs);
                string temp = "";
                bool cond = true;
                while (cond == true)
                {

                    if ((temp = sr.ReadLine()) == null)
                    {
                        sr.Close();
                        fs.Close();
                        cond = false;
                        if (count == 0)
                            return false;
                    }


                    if (count == id)
                    {
                        string[] stringSplit = temp.Split(';');
                        int _maxIndex = stringSplit.Length;
                        name = stringSplit[0];
                        lastname = stringSplit[1];
                        phone = stringSplit[2];
                        mail = stringSplit[3];
                        website = stringSplit[4];

                    }
                    count++;

                }

                sr.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool ReadEntrie(int id, ref string name)
        {
            int count = 0;
            CreateConfigFile();
            try
            {
                fs = new FileStream(data_path, FileMode.Open);
                sr = new StreamReader(fs);
                bool cond = true;
                string temp = "";
                while (cond == true)
                {

                    if ((temp = sr.ReadLine()) == null)
                    {
                        sr.Close();
                        fs.Close();
                        cond = false;
                        if (count == 0)
                            return false;
                    }

                    if (count == id)
                    {
                        string[] stringSplit = temp.Split(';');
                        int _maxIndex = stringSplit.Length;
                        name = stringSplit[0];
                    }
                    count++;

                }

                sr.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool InsertEntrie(string name, string lastname, string phone, string mail, string website)
        {
            CreateConfigFile();
            try
            {
                fs = new FileStream(data_path, FileMode.Append);
                sw = new StreamWriter(fs);
                sw.WriteLine(name + ";" + lastname + ";" + phone + ";" + mail + ";" + website);
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception ee)
            {
                string temp = ee.Message;
                return false;
            }
        }
        public bool UpdateEntrie(int id, string name, string lastname, string phone, string mail, string website)
        {
            int count = 0;
            CreateConfigFile();
            try
            {
                fs = new FileStream(data_path, FileMode.Open);
                sr = new StreamReader(fs);

                string temp = "";
                string temp2 = "";
                while ((temp = sr.ReadLine()) != null)
                {

                    if (count == id)
                    {
                        temp2 += name + ";" + lastname + ";" + phone + ";" + mail + ";" + website + "\r\n";
                    }
                    else
                    {
                        temp2 += (temp + "\r\n");
                    }
                    count++;

                }
                sr.Close();
                fs.Close();

                fs = new FileStream(data_path, FileMode.Create);
                sw = new StreamWriter(fs);
                sw.Write(temp2);
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteEntrie(int id)
        {
            int count = 0;
            CreateConfigFile();
            try
            {
                fs = new FileStream(data_path, FileMode.Open);
                sr = new StreamReader(fs);

                string temp = "";
                string temp2 = "";
                while ((temp = sr.ReadLine()) != null)
                {

                    if (count != id)
                    {
                        temp2 += (temp + "\r\n");
                    }
                    count++;

                }
                sr.Close();
                fs.Close();

                fs = new FileStream(data_path, FileMode.Create);
                sw = new StreamWriter(fs);
                sw.Write(temp2);
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool CreateConfigFile()
        {
            try
            {
                data_path = data_path.Replace((char)92, '/');
                data_path = data_path.Replace((char)9, '/');
                if (!File.Exists(data_path))
                {

                    string temp = System.IO.Path.GetDirectoryName(data_path);

                    if (!Directory.Exists(temp))
                    {
                        Directory.CreateDirectory(temp);
                    }
                    fs = new FileStream(data_path, FileMode.CreateNew);
                    fs.Close();
                }
                return true;
            }
            catch //(Exception ee)
            {
                return false;
            }
        }


    }
}
