using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // 버프 상태 관리 스크립트

    private static BuffManager _instance;
    public static BuffManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("BuffManager").AddComponent<BuffManager>();
            }

            return _instance;
        }
    }

    [Header("Prefab")]
    [SerializeField] private Transform buffSlotParent;          // 버프 슬롯 부모 Transform
    [SerializeField] private GameObject buffSlotPrefab;         // 버프 슬롯 프리팹

    [Header("List")]
    [SerializeField] private BuffSlot[] buffSlotList;           // 버프 리스트

    private int maxBuffSlotCount;                               // 버프 슬롯 최대 개수
    private int currentSlotIndex;                               // 현재 슬롯 인덱스

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        // 버프 슬롯은 최대 개수 10개만 가질 수 있도록 설정
        maxBuffSlotCount = Define.BUFF_MAX_SLOT_COUNT; 
        buffSlotList = new BuffSlot[maxBuffSlotCount];

        // 슬롯 생성
        GenerateBuffSlot();

        currentSlotIndex = 0;
    }

    private void GenerateBuffSlot()
    {
        // 슬롯 미리 생성
        for(int i=0; i<buffSlotList.Length; i++)
        {
            BuffSlot buffSlot = Instantiate(buffSlotPrefab, buffSlotParent).GetComponent<BuffSlot>();
            buffSlot.Init();
            buffSlotList[i] = buffSlot;
        }
    }

    public void AddBuff(Buff buff)
    {
        // 중복된 버프이면 버프 지속시간 초기화

        // 소지 할 수 있는 버프 개수를 넘었는지 체크
        if (currentSlotIndex >= maxBuffSlotCount) return;

        // Buff Event 세팅
        buff.SetActive(true);
        //buff.eventBuffOff += RemoveBuff(buff);

        // BuffSlot에 넣고 세팅
        buffSlotList[currentSlotIndex].Set(buff);

        // Slot Index 증가
        currentSlotIndex++;
    }

    public void RemoveBuff(Buff buff)
    {
        // 현재 BuffSlot 중 해당하는 buff를 찾아 지운다.
        for(int i=buffSlotList.Length-1; i>=0; i--)
        {
            if(buffSlotList[i].Buff == buff)
            {
                buffSlotList[i].ResetSlot();
            }
        }

        UpdateBuffSlot();
    }

    private void UpdateBuffSlot()
    {
        // 버프 슬롯 업데이트 : 버프 UI 갱신

        List<BuffSlot> activeBuff = new List<BuffSlot>(); 

        // 버프가 지속되고 있는 슬롯 찾기
        for (int i = buffSlotList.Length - 1; i >= 0; i--)
        {
            if (buffSlotList[i].isActive)
            {
                activeBuff.Add(buffSlotList[i]);
            }
        }

        // 버프 앞으로 땡기기
        for(int i=0;  i< buffSlotList.Length; i++)
        {
            int index = i;
            if(index < activeBuff.Count)
            {
                buffSlotList[index].Set(activeBuff[index].Buff);
            }
            else
            {
                buffSlotList[index].ResetSlot();
            }
        }
    }
}
