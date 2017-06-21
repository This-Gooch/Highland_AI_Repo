﻿using System;
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
    
    
}

//Personal preference for quick debugs. Instead for Debug.Log() you can use console.log.
public static class console
{
    public static void log(object message)
    {
        UnityEngine.Debug.Log(message);
    }
}