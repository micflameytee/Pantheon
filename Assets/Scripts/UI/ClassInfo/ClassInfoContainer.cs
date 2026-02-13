using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassInfoContainer : MonoBehaviour
{
    public Transform classInfoPrefab;

    public ClassInfoList classInfoList;
    
    [SerializeField] private VerticalLayoutGroup root;

    void Awake()
    {
        // Kill all example / testing children
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ClassInfo classInfo in classInfoList.classInfoList)
        {
            Transform instance = Instantiate(classInfoPrefab, transform);
            ClassInfoPrefab script = instance.gameObject.GetComponent<ClassInfoPrefab>();
            
            script.SetInfo(classInfo.className, classInfo.description, classInfo.sprite);
        }
    }

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        StartCoroutine(UpdateLayout());
    }
    
    // Cursed, redraw layout one frame after Start() is called. Fixes incorrect UI positioning.  
    // https://tenor.com/view/mower-lawn-mower-starting-up-gif-10424584
    IEnumerator UpdateLayout()
    {
        root.enabled = false;
        yield return new WaitForEndOfFrame();
        root.enabled = true;
    }
}
