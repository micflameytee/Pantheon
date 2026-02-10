using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassInfoPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text _className;
    [SerializeField] private TMP_Text _classDescription;
    [SerializeField] private Image _classImage;

    public void SetInfo(string className, string classDescription, Sprite classSprite)
    {
        _className.text = className;
        _classDescription.text = classDescription;
        _classImage.sprite = classSprite;
    }
}
