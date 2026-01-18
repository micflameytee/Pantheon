using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSlot : MonoBehaviour
{
    public Image icon;

    public GameObject topTextGameObject;
    public GameObject bottomTextGameObject;

    private TMP_Text topText;
    private TMP_Text bottomText;
    
    // Start is called before the first frame update
    void Start()
    {
        topText = topTextGameObject.GetComponent<TMP_Text>();
        bottomText = bottomTextGameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
