using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public static class XMLDataSerializer
{
    //Saves new data from in editor unit creation.
    public static void SaveUnits(UnitList newList, string path) 
    {   
        System.Type[] unit = { typeof(Unit) };
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList), unit);
        FileStream fs = new FileStream(path, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
    }
    //Saves new data from in editor card creation.
    public static void SaveCards(CardList newList, string path)
    {
       

        System.Type[] card = { typeof(Card), typeof(Minion), typeof(Action), typeof(Passive) };
        XmlSerializer serializer = new XmlSerializer(typeof(CardList), card);
        FileStream fs = new FileStream(path, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
    }

    //Loads Unit data.
    public static void LoadUnits(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("FILE " + path + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(path, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        UnitList loadedlist = (UnitList)serializer.Deserialize(fs);
       

        for (int i = 0; i < loadedlist.unitList.Count; i++)
        {
            //TODO: load the list to the appopriate location.
            //SomedataStorage.unitLists.Add(loadedlist.unitList[i]);
        }
        fs.Close();
    }
    //Loads card data into card library.
    public static void LoadCards(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("FILE " + path + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(path, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        CardList loadedlist = (CardList)serializer.Deserialize(fs);

        //Sends the list to Libraries to be loaded in.
        Libaries.instance.Load_Card_Library(loadedlist.cardList);

        fs.Close();
    }



}