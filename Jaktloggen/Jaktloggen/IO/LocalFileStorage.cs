﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Jaktloggen.Helpers;
using Jaktloggen.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Jaktloggen.IO
{
    public static class LocalFileStorage
    {
        public static bool Exists(string filename)
        {
            return DependencyService.Get<IFileUtility>().Exists(filename);
        }

        public static DateTime GetLastWriteTime(string filename)
        {
            return DependencyService.Get<IFileUtility>().GetLastWriteTime(filename);
        }

        public static T LoadFromLocalStorage<T>(string filename, bool loadFromserver = false)
        {
            var localObj = (T)Activator.CreateInstance(typeof(T));
            // 1 read json
            if (filename.EndsWith(".json") && Exists(filename))
            {
                var jsonString = DependencyService.Get<IFileUtility>().Load(filename);

                try
                {
                    localObj = JsonConvert.DeserializeObject<T>(jsonString);
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex);
                }
            }
            else
            {
                //try to read from legacy xml
                var xmlFilename = filename.Replace(".json", ".xml");
                var localFileExists = Exists(xmlFilename);

                if (localFileExists)
                {
                    var xmlString = DependencyService.Get<IFileUtility>().Load(xmlFilename);
                    try
                    {
                        using (var reader = new StringReader(xmlString))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(T));
                            localObj = (T)serializer.Deserialize(reader);
                            localObj.SaveToLocalStorage(filename); //Save to json format
                        }
                    }
                    catch (Exception ex)
                    {
                        Utils.LogError(ex);
                    }
                }
            }
            return localObj;
        }

        


        public static async void SaveToLocalStorage<T>(this T objToSerialize, string filename)
        {
            if (filename.ToLower().EndsWith(".json"))
            {
                var jsonString = JsonConvert.SerializeObject(objToSerialize);
                DependencyService.Get<IFileUtility>().Save(filename, jsonString);
                return;
            }
            else
            {
                XmlSerializer xmlSerializer = new XmlSerializer(objToSerialize.GetType());

                using (StringWriter textWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(textWriter, objToSerialize);
                    DependencyService.Get<IFileUtility>().Save(filename, textWriter.ToString());
                }
            }
        }
        
        public static void CopyToAppFolder(string file)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".Xml." + file);
            
            using (var reader = new StreamReader(stream))
            {
                DependencyService.Get<IFileUtility>().Save(file, reader.ReadToEnd());
            }
        }
    }
}
