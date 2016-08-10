using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AllianceProject.Types
{
    public class PersistenceBase
    {
        private static readonly string _companyDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "companyDataFile.json");
        private static readonly string _customerDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "customerDataFile.json");
        public Identifier Id { get; set; }

        public void Save()
        {
            if (this is Company)
            {
                Save(this, _companyDataFilePath);
            }
            else if (this is Customer)
            {
                Save(this, _customerDataFilePath);
            }
        }

        public void Save<T>(T pItem, string pFilePath)
            where T : PersistenceBase
        {
            Id.FillIdentifier(this.ToString());
            string json;
            List<T> list = new List<T>();

            if (File.Exists(pFilePath))
            {
                json = File.ReadAllText(pFilePath);
                list = JsonConvert.DeserializeObject<List<T>>(json);
            }

            // Check if old item already exist in list
            if (list.FirstOrDefault(p => p.Id.Hash == this.Id.Hash) != null)
            {
                var item = list.FirstOrDefault(p => p.Id.Hash == this.Id.Hash);
                list.Remove(item);
            }

            list.Add(this as T);

            json = JsonConvert.SerializeObject(list.ToArray());

            //write string to file
            File.WriteAllText(pFilePath, json);
        }
        
        public void Delete()
        {
            if (this is Company)
            {
                Delete(this, _companyDataFilePath);
            }
            else if (this is Customer)
            {
                Delete(this, _customerDataFilePath);
            }
        }

        public void Delete<T>(T pItem, string pFilePath)
            where T : PersistenceBase
        {
            string json;
            List<T> list = new List<T>();

            if (File.Exists(pFilePath))
            {
                json = File.ReadAllText(pFilePath);
                list = JsonConvert.DeserializeObject<List<T>>(json);
            }

            // Check if item exist
            if (list.FirstOrDefault(p => p.Id.Hash == this.Id.Hash) != null)
            {
                var item = list.FirstOrDefault(p => p.Id.Hash == this.Id.Hash);
                list.Remove(item);
            }

            json = JsonConvert.SerializeObject(list.ToArray());

            //write string to file
            File.WriteAllText(pFilePath, json);

            Id = null;
        }

        public static Company Find(CompanyId pId)
        {
            return Find<Company, CompanyId>(pId, _companyDataFilePath);
        }

        public static Customer Find(CustomerId pId)
        {
            return Find<Customer, CustomerId>(pId, _customerDataFilePath);
        }

        private static T Find<T, TIdentifier> (TIdentifier pId, string pFilePath) 
            where T : PersistenceBase
            where TIdentifier : Identifier
        {
            if (pId == null)
                return null;

            if (File.Exists(pFilePath))
            {
                var json = File.ReadAllText(pFilePath);
                var data = JsonConvert.DeserializeObject<List<T>>(json);

                // Find in list
                if (data.FirstOrDefault(p => p.Id.Hash == pId.Hash) != null)
                {
                    return data.FirstOrDefault(p => p.Id.Hash == pId.Hash);
                }
            }
            return default(T);
        }
    }
}