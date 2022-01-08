using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

namespace Shinn{

	public class LoadXml : MonoBehaviour {

        public enum path {
            Application_streamingAssetsPath,
            Application_persistentDataPath,
            absolute
        }

        public path pathstate;
		public string filepath = "C:/Users/Shinn/Desktop/";
        public string filename = "EmailSetting.xml";

        [Space, ReadOnly]
		public string temp_client;
        [ReadOnly]
        public string temp_port;
        [ReadOnly]
        public string temp_user;
        [ReadOnly]
        public string temp_pass;

        [Space, ReadOnly]
        public string temp_to;
        [ReadOnly]
        public string temp_subject;
        [ReadOnly]
        public string temp_body;
        [ReadOnly]
        public string temp_fileloc;

        private int PathSelect()
        {
            switch (pathstate)
            {
                case path.Application_streamingAssetsPath:
                    return 0;
                case path.Application_persistentDataPath:
                    return 1;
                case path.absolute:
                    return 2;
                default:
                    return 2;
            }
        }

        void Awake()
        {
            if (PathSelect() == 0)
            {
                filepath = (Application.streamingAssetsPath + "/").ToString();
                LoadFromXml(filepath + filename);
            }
            else if (PathSelect() == 1)
            {
                filepath = (Application.persistentDataPath + "/").ToString();
                LoadFromXml(filepath + filename);
            }
            else
            {
                LoadFromXml(filepath + filename);
            }
        }

        public void LoadFromXml(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            
            if (File.Exists(path))
            {
                print("Load XML Success..");

                xmlDoc.Load(path);

                XmlNodeList _client = xmlDoc.GetElementsByTagName("SMTP_Client");
                XmlNodeList _port = xmlDoc.GetElementsByTagName("SMTP_Port");
                XmlNodeList _user = xmlDoc.GetElementsByTagName("USER");
                XmlNodeList _pass = xmlDoc.GetElementsByTagName("USER_Pass");

                XmlNodeList _to = xmlDoc.GetElementsByTagName("To");
                XmlNodeList _subject = xmlDoc.GetElementsByTagName("Subject");
                XmlNodeList _body = xmlDoc.GetElementsByTagName("Body");
                XmlNodeList _file = xmlDoc.GetElementsByTagName("AttachFile");

                temp_client = _client.Item(0).InnerText;
                temp_port = _port.Item(0).InnerText;
                temp_user = _user.Item(0).InnerText;
                temp_pass = _pass.Item(0).InnerText;

                temp_to = _to.Item(0).InnerText;
                temp_subject = _subject.Item(0).InnerText;
                temp_body = _body.Item(0).InnerText;
                temp_fileloc = _file.Item(0).InnerText;
            }
            else
            {
                Debug.LogWarning("Not find xml");
            }      
        }

	}

}
