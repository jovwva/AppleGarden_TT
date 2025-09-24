using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BackpackModel
{
    public readonly int MaxCapacity = 5;
    public BackpackSlot[] slots;
    
    public bool IsFull => GetOccupiedSlotsCount() >= MaxCapacity;
    public int  CurrentCount => GetOccupiedSlotsCount();
    
    public BackpackModel(int maxCapacity)
    {
        MaxCapacity = maxCapacity;
        slots = new BackpackSlot[MaxCapacity];
        
        for (int i = 0; i < MaxCapacity; i++)
        {
            slots[i] = new BackpackSlot();
        }
    }
    
    public bool AddFruit(FruitSO fruitData, Apple appleInstance)
    {
        if (IsFull) return false;
        
        // Находим первый свободный слот
        foreach (var slot in slots)
        {
            if (!slot.IsOccupied)
            {
                slot.SetFruit(fruitData, appleInstance);
                return true;
            }
        }
        return false;
    }
    
    public int RemoveFruit(Apple appleInstance)
    {
        for (int i = 0; i < MaxCapacity; i++)
        {
            if (slots[i].IsOccupied && slots[i].AppleInstance == appleInstance)
            {
                slots[i].Clear();
                return i;
            }
        }
        return -1;
    }
    
    public void Clear()
    {
        foreach (var slot in slots)
        {
            slot.Clear();
        }
    }
    
    private int GetOccupiedSlotsCount()
    {
        int count = 0;
        foreach (var slot in slots)
        {
            if (slot.IsOccupied) count++;
        }
        return count;
    }
}

[Serializable]
public class BackpackSlot
{
    public FruitSO FruitData { get; private set; }
    public Apple AppleInstance { get; private set; }
    public bool IsOccupied => FruitData != null;
    
    public void SetFruit(FruitSO fruitData, Apple appleInstance)
    {
        FruitData = fruitData;
        AppleInstance = appleInstance;
    }
    
    public void Clear()
    {
        FruitData = null;
        AppleInstance = null;
    }
}