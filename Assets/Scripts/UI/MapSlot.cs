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

	    switch (mapMode)
	    {
		    case MapData.MapMode.FFA:
			    _bottomText.text = "FFA";
			    break;
		    case MapData.MapMode._2V2:
			    _bottomText.text = "2V2";
			    break;
		    default:
			    Debug.LogWarning("Unknown map mode: " + mapMode);
			    _bottomText.text = string.Empty;
			    break;
	    }
	    
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
