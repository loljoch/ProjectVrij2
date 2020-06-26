using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GraphicOptions : MonoBehaviour
{
	public TMP_Dropdown dropDownResolutions;
	private Resolution[] _resolutions;

	private void Start()
	{
		_resolutions = Screen.resolutions;
		dropDownResolutions.ClearOptions();
		List<string> _options = new List<string>();
		int _currentResolutionIndex = 0;
		for (int i = 0; i < _resolutions.Length; i++)
		{
			string _option = _resolutions[i].width + " X " + _resolutions[i].height;
			_options.Add(_option);
			if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
			{
				_currentResolutionIndex = i;
			}
		}

		dropDownResolutions.AddOptions(_options);
		dropDownResolutions.value = _currentResolutionIndex;
		dropDownResolutions.RefreshShownValue();
	}

	public void SetResolution(int resolutionIndex)
	{
		Resolution _resolution = _resolutions[resolutionIndex];
		Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
	}
}