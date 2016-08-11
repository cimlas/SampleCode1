using System;
using System.IO;
using Newtonsoft.Json;

namespace AllianceProject.Types
{
    public abstract class PersistenceBase <T>
    {
        public Identifier Id { get; set; }

        public static string GetFilePath(Identifier pId)
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\{pId.Hash}.json";
        }

        public void Save()
        {
            Id.FillIdentifier(this.ToString());
            
            var json = JsonConvert.SerializeObject(this);

            File.WriteAllText(GetFilePath(Id), json);
        }

        public void Delete()
        {
            string filePath = GetFilePath(Id);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Id = null;
        }

        public static T Find (Identifier pId ) 
        {
            if (pId == null)
                return default(T);

            string filePath = GetFilePath(pId);
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }
    }
}