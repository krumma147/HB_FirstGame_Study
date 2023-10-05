using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Singleton
    public static UIManager instance;
	[SerializeField] GameObject gamePanel;
	[SerializeField] GameObject homePanel;
	[SerializeField] GameObject levelSelectPanel;
	[SerializeField] GameObject SoundSetting;
	[SerializeField] GameObject WelcomePanel;
	[SerializeField] Text coinTxt;

	[SerializeField] Button startButton;
    [SerializeField] Button openSettingButton;
    [SerializeField] Toggle closeSettingButton;
    [SerializeField] Button openWelcomeButton;
    [SerializeField] Button closeWelcomeButton;

	bool musicState = false;
    //   public static UIManager Instance
    //{
    //	get
    //	{
    //		if(instance == null)
    //		{
    //			instance = FindObjectOfType<UIManager>();
    //		}
    //		return instance;
    //	}
    //}



    private void Awake()
	{
		instance = this;
		//Start Game
		startButton.onClick.AddListener(StartGame);
		//Sound
        openSettingButton.onClick.AddListener(EnableSoundSetting);
        closeSettingButton.onValueChanged.AddListener(ToggleSoundSetting);
        //Welcome btn
        openWelcomeButton.onClick.AddListener(EnableWelcomePanel);
        closeWelcomeButton.onClick.AddListener(DisableWelcomePanel);
		
        //Disable GameUI and enable HomeUI
		ShowHomeUI();
		//When click on start button then enable Game UI and disable HomeUI (stop game play too)
	}

	public void StartGame()
	{
        DisableHomePanel();
		EnableLevelSelection();
		// Add your game start logic here
	}

	public void ShowHomeUI()
	{
		DisableGamePanel();
		EnableHomePanel();
		DisableSoundSetting();
		DisableLevelSelection();
		DisableWelcomePanel();
		// Add any specific logic for showing the home UI here
	}

	private void EnableLevelSelection()
	{
		levelSelectPanel.SetActive(true);
	}

	private void DisableLevelSelection()
	{
		levelSelectPanel.SetActive(false);
	}

	public void EnableWelcomePanel()
	{
		WelcomePanel.SetActive(true);
	}

	public void DisableWelcomePanel()
	{
		WelcomePanel.SetActive(false);
	}

    public void EnableSoundSetting()
    {
        SoundSetting.SetActive(!musicState);
		musicState = !musicState;
    }

    public void DisableSoundSetting()
	{
		SoundSetting.SetActive(false);
	}

    public void ToggleSoundSetting(bool isOn)
    {

    }

    private void EnableHomePanel()
	{
		homePanel.SetActive(true);
	}

	private void DisableHomePanel()
	{
		homePanel.SetActive(false);
	}

	private void EnableGamePanel()
	{
		gamePanel.SetActive(true);
	}

	private void DisableGamePanel()
	{
		gamePanel.SetActive(false);
	}

	public void setCoin(int coin)
	{
		coinTxt.text = coin.ToString();
	}
}
