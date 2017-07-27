using System;
using System.Collections.Generic;
using System.Threading;


public static class Extension_Methods {


    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
    #region List Extensions
    //Shuffle function for lists
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    //Pop function for lists.
    public static T Pop<T>(this List<T> list)
    {
        T r = list[0];
        list.RemoveAt(0);
        return r;
    }
    #endregion

    #region String Extensions

    public static string ParseName(this string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            throw new ArgumentException("Error the string is empty or null.");
        }
            
        //Accepted delimiter character.
        char[] delimiterChars = { ' ', '_' };
        //First split the name into seperate words.
        string[] words = s.Split(delimiterChars);

        //List of word not to give an upper case.
        string[] exceptions = { "the", "of", "a", "an", "from" };

        for (int i = 0; i < words.Length; ++i)
        {
            //If the word is not the first one and is an exceptions we skip it.
            if (i != 0 && Array.IndexOf(exceptions, words[i]) != -1)
            {
                continue;
            }
            char[] a = words[i].ToCharArray();
            a[0] = char.ToUpper(a[0]);
            words[i] = new string(a);
        }
        //Rebuild the string.
        string final = "";
        for (int i = 0; i < words.Length; ++i)
        {
            final += words[i];
            if (i != words.Length - 1)
            {
                final += " ";
            }
        }

        return final;      
        
    }



    #endregion

    
}

#region Math

public static class AdvanceMath
{
    /// <summary>
    /// Calculates the Y position on an arc given the Chord(Length) and Sagitta(Height) and current X position.
    /// Does not work on impossible circles.
    /// </summary>
    /// <param name="chord"></param>
    /// <param name="sagitta"></param>
    /// <returns></returns>
    public static float Calculate_deltaY_Arc(float chord, float sagitta, float currentX)
    {
        if (chord / 2f < sagitta)
        {
            UnityEngine.Debug.LogWarning("Invalid circle, returning 0. Sagitta cannot be longer than a chord.");
            return 0f;
        }
        float x0 = chord / 2f;

        float y0 = (sagitta - UnityEngine.Mathf.Pow(x0, 2) / sagitta) / 2f;

        float r2 = UnityEngine.Mathf.Pow(x0, 2) + UnityEngine.Mathf.Pow(y0, 2);

        float yDelta = y0 + UnityEngine.Mathf.Sqrt(r2 - UnityEngine.Mathf.Pow((currentX - x0), 2));

        return yDelta;
    }
}

#endregion