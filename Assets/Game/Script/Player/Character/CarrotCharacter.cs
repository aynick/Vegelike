using System;
using UnityEngine;

namespace Game.Script.Character
{
    public class CarrotCharacter : CharacterBase
    {
        private void Start()
        {
            _playerBehavior = transform.parent?.GetComponent<PlayerBehavior>();
        }
    }
}