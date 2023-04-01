using System;
using System.Collections;
using System.Collections.Generic;
using Game.Script;
using UnityEngine;

public class PlayerCharacterChange : MonoBehaviour
{
    public CharacterBase[] characters = new CharacterBase[2];
    [SerializeField] private PlayerEventHandler playerEventHandler;
    private CharacterBase spawnedCharacter;
    private int currentCharacterIndex = 0;
    
    private void Start()
    {
        playerEventHandler.OnCharacterChanged += ChangeCharacter;
        SpawnCharacter(currentCharacterIndex);
    }

    private void SpawnCharacter(int index)
    {
        spawnedCharacter = Instantiate(characters[index], transform);
    }

    private void ChangeCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex == 2) currentCharacterIndex = 0;
        Destroy(spawnedCharacter.gameObject);
        playerEventHandler.OnCharacterDestroy();
        SpawnCharacter(currentCharacterIndex);
    }
}
