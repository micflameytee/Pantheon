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

    
    private MapData.MapMode[] _supportedModes;

    private TMP_Text _topText;
    private TMP_Text _bottomText;
	
	public Toggle toggle;

	[HideInInspector] public string sceneName;
    
    // Start is called before the first frame update
    void Awake()
    {
        _topText = topTextGameObject.GetComponent<TMP_Text>();
        _bottomText = bottomTextGameObject.GetComponent<TMP_Text>();
    
		toggle.onValueChanged.AddListener(OnToggleChanged);
        
		// Set style based on initial state
        OnToggleChanged(toggle.isOn);
	}

    public void SetData(string topText, MapData.MapMode[] supportedModes, string sceneName, Sprite sprite = null)
    {
	    _topText.text = topText;
	    _bottomText.text = "";
	    this.sceneName = sceneName;
	    //_bottomText.text = MapData.GetMapModeString(mapMode);

	    _supportedModes = supportedModes;

	    if (sprite)
	    {
		    icon.sprite = sprite;
	    }
    }

    public void SetToggleGroup(ToggleGroup group) {
		toggle.group = group;
	}

	public bool IsSupportedMode(MapData.MapMode mode)
	{
		foreach (MapData.MapMode _mode in _supportedModes)
		{
			if (_mode == mode)
			{
				return true;
			}
		}
		
		return false;
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


}
