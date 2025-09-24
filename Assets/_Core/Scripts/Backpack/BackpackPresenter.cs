using System;
using UnityEngine;

public class BackpackPresenter : MonoBehaviour
{
    [SerializeField] private PlaceholderView[] placeholderViews;
    
    private BackpackModel _model;

    private void Awake()
    {
        _model = new BackpackModel(5);
    }
    
    public bool AddFruit(Apple appleInstance)
    {
        if (_model.AddFruit(appleInstance.FruitData, appleInstance))
        {
            int slotIndex = GetSlotIndex(appleInstance);
            placeholderViews[slotIndex].SetIcon(appleInstance.FruitData.Icon);
            
            return true;
        }
        
        return false;
    }
    
    public void RemoveFruit(Apple appleInstance)
    {
        int slotIndex = _model.RemoveFruit(appleInstance);
        
        if (slotIndex >= 0)
        {
            placeholderViews[slotIndex].Reset();
        }
    }
    
    private int GetSlotIndex(Apple appleInstance)
    {
        for (int i = 0; i < _model.slots.Length; i++)
        {
            if (_model.slots[i].AppleInstance == appleInstance)
                return i;
        }
        return -1;
    }
}
