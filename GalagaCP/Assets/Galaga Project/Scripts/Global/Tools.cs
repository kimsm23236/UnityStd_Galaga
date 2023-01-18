using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static class Tools
{
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObj_ = activeScene_.GetRootGameObjects();
        
        GameObject targetObj = default;
        foreach(GameObject rootObj in rootObj_)
        {
            if(rootObj.name.Equals(objName_))
            {
                targetObj = rootObj;
                return targetObj;
            }
            else 
            {
                continue;
            }
        }   // loop

        return targetObj;
    }   // GetRootObj()

    // 현재 활성화 되어 있는 씬을 찾아주는 함수
    public static Scene GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }   // GetActiveScene()
}
