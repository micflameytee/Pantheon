using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSlot : MonoBehaviour
{
    public Image icon;
    public Sprite disconnected;

    public Sprite controller;

    public Sprite keyboard;
    
    enum States { DISCONNECTED, CONTROLLER, KEYBOARD };

    public GameObject topTextGameObject;
    public GameObject bottomTextGameObject;

    private TMP_Text topText;
    private TMP_Text bottomText;
    
    // Start is called before the first frame update
    void Start()
    {
        topText = topTextGameObject.GetComponent<TMP_Text>();
        bottomText = bottomTextGameObject.GetComponent<TMP_Text>();
        
        if (disconnected != null)
        {
            SetState(States.DISCONNECTED);
        }
        else
        {
            Debug.LogWarning("No disconnected sprite set for PlayerSlot");
        }
    }

    void SetState(States state, int playerNumber = -1)
    {
        switch (state)
        {
            case States.DISCONNECTED:
                topText.text = "Disconnected";
                icon.sprite = disconnected;
                bottomText.text = "Press any Key";
                break;
            case States.CONTROLLER:
                topText.text = "Player " + playerNumber;
                icon.sprite = controller;
                bottomText.text = "Ready";
                break;
            case States.KEYBOARD:
                topText.text = "Player " + playerNumber;
                icon.sprite = keyboard;
                bottomText.text = "Ready";
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
