using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common
{
    public class ResourceManager
    {
        private volatile static ResourceManager instance = null;
        private static object locker = new Object();
        public static ResourceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ResourceManager();
                        }
                    }
                }
                return instance;
            }
        }

        private string FolderPath = string.Empty;
        public SortedList<String, Resources> LanguageResources = new SortedList<String, Resources>();

        public void Serialize(Resources resources, string filePath)
        {
            ResourcesSerializer.Serialize(filePath, resources);
        }

        public void Init(string filePath)
        {
            FolderPath = filePath;
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            LanguageResources.Clear();
            if (!directoryInfo.Exists)
            {
                return;
            }
            FileInfo[] FileInfo = directoryInfo.GetFiles();
            for (int i = 0; i < FileInfo.Length; i++)
            {
                Resources resources = ResourcesSerializer.DeSerialize(FileInfo[i].FullName);
                resources.createIndex();
                LanguageResources.Add(resources.language, resources);
            }
        }

        public Hashtable GetLanguages()
        {
            Hashtable hashtable = new Hashtable();
            IEnumerator<KeyValuePair<String, Resources>> iEnumerator = LanguageResources.GetEnumerator();
            while (iEnumerator.MoveNext())
            {
                hashtable.Add(iEnumerator.Current.Key, iEnumerator.Current.Value.displayName);
            }
            return hashtable;
        }

        public Hashtable GetLanguages(string path)
        {
            Hashtable hashtable = new Hashtable();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                return hashtable;
            }
            FileInfo[] fileInfo = directoryInfo.GetFiles();
            for (int i = 0; i < fileInfo.Length; i++)
            {
                Resources resources = ResourcesSerializer.DeSerialize(fileInfo[i].FullName);
                hashtable.Add(resources.language, resources.displayName);
            }
            return hashtable;
        }

        public Resources GetResources(string filePath)
        {
            Resources resources = new Resources();
            if (File.Exists(filePath))
            {
                resources = ResourcesSerializer.DeSerialize(filePath);
                resources.createIndex();
            }
            return resources;
        }

        public string Get(string language, string key)
        {
            if (!LanguageResources.ContainsKey(language))
            {
                return string.Empty;
            }
            return LanguageResources[language].Get(key);
        }
    }


    public class ResourceManagerWrapper
    {
        private volatile static ResourceManagerWrapper instance = null;
        private static object locker = new Object();
        private static string CurrentLanguage = "en-us";

        public static ResourceManagerWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ResourceManagerWrapper();
                        }
                    }
                }
                return instance;
            }
        }

        private ResourceManager ResourceManager;

        public ResourceManagerWrapper()
        {
        }

        public void LoadResources(string path)
        {
            ResourceManager = ResourceManager.Instance;
            ResourceManager.Init(path);
        }

        public string Get(string key)
        {
            return ResourceManager.Get(CurrentLanguage, key);
        }

        public string Get(string lanauage, string key)
        {
            return ResourceManager.Get(lanauage, key);
        }

        public Hashtable GetLanguages()
        {
            return ResourceManager.GetLanguages();
        }

        public Hashtable GetLanguages(string path)
        {
            return ResourceManager.GetLanguages(path);
        }

        public void Serialize(string path, string language, string key, string value)
        {
            Resources Resources = this.GetResources(path, language);
            Resources.Set(key, value);
            string filePath = path + "\\" + language + ".xml";
            ResourceManager.Serialize(Resources, filePath);
        }

        public Resources GetResources(string path, string language)
        {
            string filePath = path + "\\" + language + ".xml";
            return ResourceManager.GetResources(filePath);
        }

        public Resources GetResources(string language)
        {
            return ResourceManager.LanguageResources[language];
        }
    }



    [XmlRoot("resources")]
    public class Resources
    {
        private SortedList<String, String> indexs = new SortedList<String, String>();

        [XmlElement("language")]
        public string language = string.Empty;
        [XmlElement("displayName")]
        public string displayName = string.Empty;
        [XmlElement("version")]
        public string version = string.Empty;
        [XmlElement("author")]
        public string author = string.Empty;
        [XmlElement("description")]
        public string description = string.Empty;
        [XmlElement("items", typeof(Items))]
        public Items items;

        public void createIndex()
        {
            indexs.Clear();
            if (items == null)
            {
                return;
            }
            indexs = new SortedList<String, String>(items.items.Length);
            for (int i = 0; i < items.items.Length; i++)
            {
#if DEBUG
                try
                {
                    indexs.Add(items.items[i].key, items.items[i].value);
                }
                catch
                {
                    throw (new Exception(items.items[i].key + items.items[i].value));
                }
#else
                    indexs.Add(items.items[i].key, items.items[i].value);
#endif
            }
        }

        public string Get(string key)
        {
            if (!indexs.ContainsKey(key))
            {
                return string.Empty;
            }
            return indexs[key];
        }

        /// <summary>
        /// JiRiGaLa 2007.05.02
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, string value)
        {
            if (!indexs.ContainsKey(key))
            {
                return false;
            }
            indexs[key] = value;
            for (int i = 0; i < items.items.Length; i++)
            {
                if (items.items[i].key == key)
                {
                    items.items[i].value = value;
                    break;
                }
            }
            return true;
        }
    }

    public class Items
    {
        [XmlElement("item", typeof(Item))]
        public Item[] items;
    }


    public class Item
    {
        [XmlAttribute("key")]
        public string key = string.Empty;
        [XmlText]
        public string value = string.Empty;
    }


    internal class ResourcesSerializer
    {
        public static Resources DeSerialize(string filePath)
        {
            System.Xml.Serialization.XmlSerializer XmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Resources));
            System.IO.FileStream FileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            Resources Resources = XmlSerializer.Deserialize(FileStream) as Resources;
            FileStream.Close();
            return Resources;
        }

        public static void Serialize(string filePath, Resources Resources)
        {
            System.Xml.Serialization.XmlSerializer XmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Resources));
            System.IO.FileStream FileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
            XmlSerializer.Serialize(FileStream, Resources);
            FileStream.Close();
        }
    }
}
