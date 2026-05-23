using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTicketSettings", menuName = "TicketSettings")]
public class TicketSettings : ScriptableObject
{
    public int SpinCost;
    public string Dfficulty;
    public List<CardChanceSetting> Chances;
}

[Serializable]
public class CardChanceSetting
{
    public Sprite icon;
    public float chance;
    public float reward;
}