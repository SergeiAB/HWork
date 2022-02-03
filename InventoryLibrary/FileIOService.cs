using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InventoryLibrary
{
    public class FileIOService
    {
        private readonly string _path;

        public FileIOService(string Path)
        {
            _path = Path;
        }

        // считвание данных из Json файла или создание файла с возвратом листа
        public List<Product> LoadData()
        {
            List<Product> result=new();
            bool fileExis = File.Exists(_path);// проверка наличия файла
            if (fileExis)
            {
                string fileText = File.ReadAllText(_path);//считывание файла
                result = JsonConvert.DeserializeObject<List<Product>>(fileText) ?? result;// конвертация Json в список
            }
            return result;

        }

        public void SaveData(List<Product> products)
        {
            using StreamWriter writer = File.CreateText(_path);
            string output = JsonConvert.SerializeObject(products);
            writer.Write(output);
        }
    }
}
