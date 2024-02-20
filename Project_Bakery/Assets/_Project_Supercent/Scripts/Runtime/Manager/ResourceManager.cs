using System.Collections.Generic;
using UnityEngine;


public static class ResourceManager
{
    #region Dictionarys
    public static Dictionary<string, GameObject> objects;
    #endregion

    public static void Init()
    {
        objects = new Dictionary<string, GameObject>();
        AddResouces();
    }       // Init()

    // ! ���ҽ� �迭�� �߰�
    public static void AddResouces()
    {
        GameObject[] objResources = Resources.LoadAll<GameObject>(RDefine.PATH_OBJECT);


        AddDictionary(objResources, objects);


    }       // AddResource()

    // ! ���ҽ� �迭�� ��ųʸ��� ĳ��
    private static void AddDictionary<T>(T[] resources_, Dictionary<string, T> dictionary_)
    {

        foreach (T resource in resources_)
        {
            Object temp = resource as Object;
            dictionary_.Add(temp.name, resource);
        }
    }       // AddDictionary()
}