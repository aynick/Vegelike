using System.Collections;
using System.Collections.Generic;
using Game.Script.Inventory;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public Joystick _walkJoystick;
    public Button _attackBtn;
    public Button _jumpBtn;
    public Button _interactBtn;
    public Button _changeCharacterBtn;
    public Slider _healthPointSlider;
    public Button _skillBtn;
    public List<InventorySlot> slots;
}
