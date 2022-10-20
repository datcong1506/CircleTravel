using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheComponentManager : Singleton<CacheComponentManager>
{
    
    //Circle controler Caches
    public CacheComponent<CircleController> CCCache = new CacheComponent<CircleController>();

    public CacheComponent<PostController> PostCache = new CacheComponent<PostController>();


    public void ResetCache()
    {
        CCCache.Clear();
        PostCache.Clear();
    }
}
