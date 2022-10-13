using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    protected static List<TimerComponent> _components = new List<TimerComponent>();
    protected static int _tagCount = 1000;
    private bool isBackground = false;//�Ƿ���Ժ�̨����


    //Update����֮ǰ
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(UpdateFrame());
    }

    //�����л�����̨
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!isBackground)
                StopAllCoroutines();
        }
        else
        {
            if (!isBackground)
                StartCoroutine(UpdateFrame());
        }
    }

    //ÿһ֡������
    private IEnumerator UpdateFrame()
    {

        while (true)
        {
            if (_components.Count > 0)
            {
                float dt = Time.deltaTime;
                TimerComponent t = null;
                for (int i = 0; i < _components.Count; ++i)
                {
                    t = _components[i];
                    t.tm += Time.deltaTime;
                    if (t.tm >= t.life)
                    {
                        t.tm -= t.life;
                        if (t.count == 1)
                        {
                            _components.RemoveAt(i--);
                            if (lateChannel.Contains(t.func))
                            {
                                lateChannel.Remove(t.func);
                            }
                        }
                        --t.count;
                        t.func();
                    }
                }
            }
            yield return 0;
        }
    }

    private static HashSet<Action> lateChannel = new HashSet<Action>();
    /// <summary>
    /// ��ε��� ִֻ��һ��
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="func"></param>
    public static void CallerLate(float delay, Action func)
    {
        if (!lateChannel.Contains(func))
        {
            lateChannel.Add(func);
            SetTimeout(delay, func);
        }
    }

    /// <summary>
    /// ��ʱ�ص�һ��
    /// </summary>
    /// <param name="delay">�ȴ�ʱ�� ��λ��</param>
    /// <param name="func">�ص�����</param>
    /// <returns>Timer ID</returns>
    public static int SetTimeout(float delay, Action func)
    {
        return SetInterval(delay, func);
    }


    /// <summary>
    /// ע��һ����ʱ��
    /// </summary>
    /// <param name="interval">��� ��λ��</param>
    /// <param name="func">���ȷ���</param>
    /// <param name="times">ѭ������ times{ 0 | < 1 }:һֱѭ�� </param>
    /// <returns>ÿ���������ı�ǩֵ</returns>
    public static int SetInterval(float interval, Action func, int times = 1)
    {
        var scheduler = new TimerComponent();
        scheduler.tm = 0;
        scheduler.life = interval;
        scheduler.func = func;
        scheduler.count = times;
        scheduler.tag = ++_tagCount;
        _components.Add(scheduler);
        if (!_inited) Init();
        return _tagCount;
    }

    /// <summary>
    /// ͨ��Tag��ȡ��ʱ������
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static TimerComponent GetTimer(int tag)
    {

        foreach (var scheduler in _components)
        {
            if (tag == scheduler.tag)
            {
                return scheduler;
            }
        }

        return null;
    }

    /// <summary>
    /// ����ʱ��
    /// </summary>
    /// <param name="tag">��ʱ����ǩ</param>
    /// <returns></returns>
    public static void ClearTimer(int tag)
    {
        int index = _components.FindIndex((TimerComponent t) =>
        {
            return t.tag == tag;
        });
        _components.RemoveAt(index);
    }

    /// <summary>
    /// �������ж�ʱ��
    /// </summary>
    public static void ClearTimers()
    {
        _components.Clear();
    }

    //��ʼ��
    protected static bool _inited = false;
    protected static void Init()
    {
        _inited = true;
    }

}

public class TimerComponent
{
    public int tag;
    public float tm;
    public float life;
    public int count;
    public Action func;
}

