using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Singleton
    public static UIManager instance;
	[SerializeField] GameObject gameUI;
	[SerializeField] GameObject homeUI;
	[SerializeField] GameObject SoundSetting;
	[SerializeField] GameObject WelcomePanel;
	[SerializeField] Text coinTxt;
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
		//Disable GameUI and enable HomeUI
		DisableSoundSetting();
		DisableWelcomePanel();
		ShowHomeUI();
		//When click on start button then enable Game UI and disable HomeUI (stop game play too)
	}

	public void StartGame()
	{
		EnableGameUI();
		DisableHomeUI();
		// Add your game start logic here
	}

	public void ShowHomeUI()
	{
		DisableGameUI();
		EnableHomeUI();
		// Add any specific logic for showing the home UI here
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
		SoundSetting.SetActive(true);
	}

	public void DisableSoundSetting()
	{
		SoundSetting.SetActive(false);
	}

	private void EnableHomeUI()
	{
		homeUI.SetActive(true);
	}

	private void DisableHomeUI()
	{
		homeUI.SetActive(false);
	}

	private void EnableGameUI()
	{
		gameUI.SetActive(true);
	}

	private void DisableGameUI()
	{
		gameUI.SetActive(false);
	}

	public void setCoin(int coin)
	{
		coinTxt.text = coin.ToString();
	}
}
