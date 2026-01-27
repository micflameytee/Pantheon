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

    [HideInInspector]
    public int playerMin;
    [HideInInspector]
    public int playerMax;

    
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

    public void SetData(string sceneName, string mapName, MapData.MapMode[] supportedModes, int playerMin, int playerMax, Sprite sprite = null)
    {
	    _topText.text = mapName;
	    _bottomText.text = "";
	    this.sceneName = sceneName;
	    
	    _supportedModes = supportedModes;

	    this.playerMin = playerMin; 
	    this.playerMax = playerMax;
	    //_bottomText.text = MapData.GetMapModeString(mapMode);

	    
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
