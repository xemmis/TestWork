using UnityEngine;
using UnityEngine.UI;

public class ArmorInfoWindow : MonoBehaviour
{
    [SerializeField] private Slot _selectedSlot;
    [SerializeField] private Item _selectedItem;
    [SerializeField] private Image _modalImage;
    [SerializeField] private Image _chestplateImage;
    [SerializeField] private Image _helmetImage;
    [SerializeField] private GameObject _modalWindow;

    private void Start()
    {
        Slot.OnItemClicked += OpenWindow;
    }

    private void OpenWindow(Item item, Slot slot)
    {
        _selectedItem = item;
        _selectedSlot = slot;
        _modalImage.sprite = item.Sprite;
        _modalWindow.SetActive(true);
    }

    public void EquipArmor()
    {
        if (_selectedItem.Helmet) _helmetImage.sprite = _selectedItem.Sprite;
        else _chestplateImage.sprite= _selectedItem.Sprite;
    }

    public void DeEquipArmor()
    {
        if (_selectedItem.Helmet) _helmetImage.sprite = null;
        else _chestplateImage.sprite = null;
        _selectedItem = null;
        _selectedSlot.UpdateSlot(null);
        _selectedSlot = null;
    }
}