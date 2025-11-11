using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Speed,
    Heal,
    Invincibility
}

[CreateAssetMenu(fileName = "newBuff", menuName = "Buff/newBuff")]
public class BuffData : ScriptableObject
{
    public string buffName;         // 버프 이름
    public string description;      // 버프 설명
    public BuffType type;           // 버프 타입
    public float duration;          // 버프 지속시간
    public Sprite icon;             // 버프 아이콘
}
