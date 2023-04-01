using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button restartBtn;

    private void Start()
    {
        restartBtn.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
