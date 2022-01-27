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
            bool fileExis = File.Exists(_path);// проверка наличия файла
            if (!fileExis)
            {
                using (File.CreateText(_path)) { }
                

                //создание файла по указанному пути
                
                //Console.WriteLine("Create file " + _path);
                return new List<Product>();
                 
            }
            else
            {
                using (StreamReader reader = File.OpenText(_path))
                {
                    string fileText = File.ReadAllText(_path);//считывание файла
                    return JsonConvert.DeserializeObject<List<Product>>(fileText);// конвертация Json в список
                    
                }
            }
                
            
        }

        public void SaveData(List<Product> products)
        {
            using (StreamWriter writer = File.CreateText(_path))
            {
                string output = JsonConvert.SerializeObject(products);
                writer.Write(output);
            }
        }
    }
}
