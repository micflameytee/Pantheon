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
	
	public Toggle toggle;
    
    // Start is called before the first frame update
    void Start()
    {
        topText = topTextGameObject.GetComponent<TMP_Text>();
        bottomText = bottomTextGameObject.GetComponent<TMP_Text>();
    
		toggle.onValueChanged.AddListener(OnToggleChanged);
        
		// Set style based on initial state
        OnToggleChanged(toggle.isOn);
	}

    private void OnToggleChanged(bool isOn) {
		// Italics if selected, normal text if not selected
		if (isOn) {
			topText.fontStyle = FontStyles.Italic;
			bottomText.fontStyle = FontStyles.Italic;
		} else {
			topText.fontStyle = FontStyles.Normal;
			bottomText.fontStyle = FontStyles.Normal;
		}
	}

	public void SetToggleGroup(ToggleGroup group) {
		toggle.group = group;
	}
}
