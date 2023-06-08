
using UnityEngine;
using UnityEngine.UI;
public class DragSlot : MonoBehaviour
{
    public static DragSlot instance;
    public Slot dragSlot;
    public Image dragSlotImage;
    public int beforePos;
    public bool isQuickSlot;
    private void Awake()
    {
        dragSlotImage = GetComponent<Image>();
    }
    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        dragSlotImage.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = dragSlotImage.color;
        color.a = _alpha;
        dragSlotImage.color = color;
    }
}
