using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Windows.Storage;
using System.Runtime.Serialization;
using Windows.Storage.Streams;
using XAMLMetroAppIsolatedStorageHelper;

namespace recipecards
{
    class SaveAndLoad
    {

        //public IngredientLib Ing_obj = new IngredientLib();
        //public IngredientLib Loaded_Data = new IngredientLib();
        //public LocalAppData ExtraAppData = new LocalAppData();
       


        public SaveAndLoad() 
        {
            //Ing_obj.StoreIng.Add("BANNA");
            //Ing_obj.StoreIng.Add("asdhiqw");
           
            
        }

        public async Task SaveData(LocalAppData Data)
        {
            var ObjectStorageHelper = new StorageHelper<LocalAppData>(StorageType.Local);
            await Task.Run(() => ObjectStorageHelper.SaveASync(Data, "AllAppData"));
        }

        public async Task<LocalAppData> LoadData()
        {
            var ObjectStorageHelper = new StorageHelper<LocalAppData>(StorageType.Local);
            LocalAppData LoadedData = await ObjectStorageHelper.LoadASync("AllAppData");

            return LoadedData;
        }

        public async Task CreateAppLocalData()
        {
            var ObjectStorageHelper = new StorageHelper<LocalAppData>(StorageType.Local);
            LocalAppData LoadedData = await ObjectStorageHelper.LoadASync("AllAppData");

            //If file hasn't been created. Create it
            if (LoadedData == null)
            {
                //Never created this file, create it now
                LocalAppData NewAppDataStruct = new LocalAppData();
                await Task.Run(() => ObjectStorageHelper.SaveASync(NewAppDataStruct, "AllAppData"));
            }
        }


        /*public async Task SaveCardData(CardObj SaveData, string File_Name)
        {

            var ObjectStorageHelper = new StorageHelper<CardObj>(StorageType.Local);
            await Task.Run(() => ObjectStorageHelper.SaveASync(SaveData, File_Name));
            await IncCardCountOne();
        }


        public async Task<CardObj> LoadCardData(string File_Name)
        {
            var ObjectStorageHelper = new StorageHelper<CardObj>(StorageType.Local);
            CardObj LoadedData = await ObjectStorageHelper.LoadASync(File_Name);

            return LoadedData;
        }

        public async Task IncCardCountOne()
        {
            var ObjectStorageHelper = new StorageHelper<LocalAppData>(StorageType.Local);
            LocalAppData ExtraAppData = await ObjectStorageHelper.LoadASync("ExtraAppData");

            ExtraAppData.CardCount += 1;

            //send it back to storage
            await Task.Run(() => ObjectStorageHelper.SaveASync(ExtraAppData, "ExtraAppData"));
            
        }

        

        public async Task<int> GetCardCount()
        {
            var ObjectStorageHelper = new StorageHelper<LocalAppData>(StorageType.Local);
            LocalAppData ExtraAppData = await ObjectStorageHelper.LoadASync("ExtraAppData");

            return ExtraAppData.CardCount;
        }
        public async Task CreateAppLocalData()
        {
            var ObjectStorageHelper = new StorageHelper<LocalAppData>(StorageType.Local);
            LocalAppData LoadedData = await ObjectStorageHelper.LoadASync("ExtraAppData");

            //If file hasn't been created. Create it
            if (LoadedData == null)
            {
                //Never created this file, create it now
                LocalAppData NewAppDataStruct = new LocalAppData();
                await Task.Run(() => ObjectStorageHelper.SaveASync(NewAppDataStruct, "ExtraAppData"));
            }
        }*/

        
    }


   
}
