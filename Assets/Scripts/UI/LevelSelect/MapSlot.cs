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

    private TMP_Text _topText;
    private TMP_Text _bottomText;
	
	public Toggle toggle;
    
    // Start is called before the first frame update
    void Awake()
    {
        _topText = topTextGameObject.GetComponent<TMP_Text>();
        _bottomText = bottomTextGameObject.GetComponent<TMP_Text>();
    
		toggle.onValueChanged.AddListener(OnToggleChanged);
        
		// Set style based on initial state
        OnToggleChanged(toggle.isOn);
	}

    public void SetData(string topText, MapData.MapMode mapMode, Sprite sprite = null)
    {
	    _topText.text = topText;
	    _bottomText.text = MapData.GetMapModeString(mapMode);
	    
	    if (sprite) { icon.sprite = sprite; }
    }

    private void OnToggleChanged(bool isOn) {
		// Italics if selected, normal text if not selected
		if (isOn) {
			_topText.fontStyle = FontStyles.Italic | FontStyles.Underline;
			_bottomText.fontStyle = FontStyles.Italic | FontStyles.Underline;
		} else {
			_topText.fontStyle = FontStyles.Normal;
			_bottomText.fontStyle = FontStyles.Normal;
		}
	}

	public void SetToggleGroup(ToggleGroup group) {
		toggle.group = group;
	}
}
