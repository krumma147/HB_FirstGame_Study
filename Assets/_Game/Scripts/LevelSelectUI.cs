using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
	//Level Button
    [SerializeField] Button btnLevel1;
    [SerializeField] Button btnLevel2;
    [SerializeField] Button btnLevel3;

	//Level Map
	[SerializeField] GameObject map1;
	[SerializeField] GameObject map2;
	[SerializeField] GameObject map3;

	//Panel UI
	[SerializeField] GameObject levelSelectionPanel;
	[SerializeField] GameObject gamePanel;
	// Start is called before the first frame update
	void Start()
    {
		DeactiveAllMap();
		btnLevel1.onClick.AddListener(LoadLevel1);
        btnLevel2.onClick.AddListener(LoadLevel2);
        btnLevel3.onClick.AddListener(LoadLevel3);
	}

	private void LoadLevel1()
	{
		map1.SetActive(true);
		levelSelectionPanel.SetActive(false);
		gamePanel.SetActive(true);
	}

	private void LoadLevel3()
	{
		map2.SetActive(true);
		levelSelectionPanel.SetActive(false);
		gamePanel.SetActive(true);
	}

	private void LoadLevel2()
	{
		map3.SetActive(true);
		levelSelectionPanel.SetActive(false);
		gamePanel.SetActive(true);
	}

	void DeactiveAllMap()
	{
		map1.SetActive(false);
		map2.SetActive(false);
		map3.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
        
    }

}
