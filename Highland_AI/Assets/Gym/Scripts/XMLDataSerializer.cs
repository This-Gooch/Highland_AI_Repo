using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XMLDataSerializer
{

    [SerializeField]
    [Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string savedPath = "Assets/Data/";
    [SerializeField]
    [Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string unitsPath = "units.xml";
    [SerializeField]
    [Tooltip("DO NOT EDIT UNLESS INSTRUCTED.")]
    string cardsPath = "cards.xml";

    //Saves new data from in editor unit creation.
    public void SaveUnits() 
    {
        //TODO: implement in editor unit creation.
        UnitList newList = new UnitList("main");

        /*//TODO: add units to save.
        if (SomedataStorage.unitLists.Count != 0)
        {
            for (int i = 0; i < SomedataStorage.unitLists.Count; i++)
            {
                newList.Add(SomedataStorage.unitLists[i]);
            }
        }*/

        System.Type[] unit = { typeof(Unit) };
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList), unit);
        FileStream fs = new FileStream(savedPath + unitsPath, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
        newList = null;
    }
    //Saves new data from in editor card creation.
    public void SaveCards()
    {
        //TODO: implement in editor card creation.
    }

    //Loads Unit data.
    public void LoadUnits()
    {
        if (!File.Exists(savedPath + unitsPath))
        {
            Debug.LogError("FILE " + savedPath + unitsPath + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(savedPath + unitsPath, FileMode.Open);
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
    public void LoadCards()
    {
        if (!File.Exists(savedPath + cardsPath))
        {
            Debug.LogError("FILE " + savedPath + cardsPath + " NOT FOUND!");
            return;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(savedPath + cardsPath, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        CardList loadedlist = (CardList)serializer.Deserialize(fs);

        //Sends the list to Libraries to be loaded in.
        Libaries.instance.Load_Card_Library(loadedlist.cardList);

        fs.Close();
    }



}