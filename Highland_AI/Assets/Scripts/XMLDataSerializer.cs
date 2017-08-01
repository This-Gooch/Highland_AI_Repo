using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public static class XMLDataSerializer
{
    //System types for cards.
    static System.Type[] cardTypes = { typeof(Card), typeof(Minion), typeof(Action), typeof(Passive) };

    //Saves new data from in editor unit creation.
    public static void SaveUnits(UnitList newList, string path) 
    {   
        System.Type[] unit = { typeof(UnitInfo) };
        XmlSerializer serializer = new XmlSerializer(typeof(UnitList), unit);
        FileStream fs = new FileStream(path, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
    }
    //Saves new data from in editor card creation.
    public static void SaveCards(CardList newList, string path)
    {        
        XmlSerializer serializer = new XmlSerializer(typeof(CardList), cardTypes);
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

        //Sends the data to libraries for loading.
        Libraries.instance.Load_Unit_Library(loadedlist.unitList);

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
        XmlSerializer serializer = new XmlSerializer(typeof(CardList), cardTypes);
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(path, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        CardList loadedlist = (CardList)serializer.Deserialize(fs);

        //Sends the list to Libraries to be loaded in.
        Libraries.instance.Load_Card_Library(loadedlist.cardList);

        fs.Close();
    }

    #region InGame saves/loads
    //Loads all the cards of a specific deck.
    //Naming convention for decks should be hero_name+deck_given_name
    public static DeckList LoadDeck(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError("FILE " + path + " NOT FOUND!");
            return null;
        }
        XmlSerializer serializer = new XmlSerializer(typeof(DeckList));
        // To read the file, create a FileStream.
        FileStream fs = new FileStream(path, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        DeckList loadedlist = (DeckList)serializer.Deserialize(fs);


        fs.Close();
        return loadedlist;
    }
    //Save all the cards of a specific deck.
    //Naming convention for decks should be hero_name+deck_given_name
    public static void SaveDeck(DeckList newList, string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DeckList));
        FileStream fs = new FileStream(path, FileMode.Create);
        serializer.Serialize(fs, newList);
        fs.Close();
    }
    #endregion


}