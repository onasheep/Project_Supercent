using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static partial class GFunc
{
    public static bool IsValid(this GameObject gameObject_)
    {
        bool isValid = gameObject_ == null || gameObject_ == default;
        return !isValid;
    }

    public static bool IsValid<T>(this List<T> list_)
    {
        bool isValid = list_ == null || list_.Count < 1;
        return !isValid;
    }

    public static bool IsValid<T>(this T[] array_)
    {
        bool isValid = array_ == null || array_.Length < 1;
        return !isValid;
    }

    //! 씬의 루트 오브젝트를 서치해서 찾아 주는 함수
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene = GetActiveScene();
        GameObject[] rootObjs_ = activeScene.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name == objName_)
            {
                targetObj_ = rootObj;
                return targetObj_;
            }
            else { continue; }
        }       // loop 

        return targetObj_;
    }       // GetRootObj

    //! 특정 오브젝트의 자식 오브젝트를 모두 리스트로 리턴하는 함수
    public static List<GameObject> GetChildrenObjs
        (this GameObject targetObj_)
    {
        List<GameObject> objs = new List<GameObject>();
        GameObject searchTarget = default;

        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            objs.Add(searchTarget);
        }

        if (objs.IsValid()) { return objs; }
        else { return default(List<GameObject>); }
    }       // GetChildrenObjs()


    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾는 함수 
    public static GameObject FindChildObj
        (this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);

                // 방어로직
                if (searchResult == null || searchResult == default) { /* Pass */ }
                else { return searchResult; }
            }
        }       // loop

        return searchResult;
    }       // FindChildObj()

    //! 씬의 루트 오브젝트로 부터 자식 오브젝트의 컴퍼넌트를 가져오는 함수
    public static T FindChildComponent<T>
        (this GameObject targetObj_, string objName_) where T : Component
    {
        T searchResultComponent = default(T);
        GameObject searchResultObj = default(GameObject);

        searchResultObj = targetObj_.FindChildObj(objName_);
        if (searchResultObj.IsValid())
        {
            searchResultComponent = searchResultObj.GetComponent<T>();
        }

        return searchResultComponent;
    }       // FindChildComponent()


    public static Scene GetActiveScene()
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }       // GetActiveScene()
}