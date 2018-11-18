using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularIndex
{
    private int m_index = 0;
    public int MaxValue
    {
        get => MaxValue;
        set => MaxValue = value;
    }
    public int Index
    {
        get
        {
            return m_index;
        }
        set
        {
            m_index = value;
            m_index %= MaxValue;
            if (m_index < 0)
            {
                m_index += MaxValue;
            }
        }
    }

    public CircularIndex (int maxValue)
    {
        MaxValue = maxValue;
    }

    public static CircularIndex operator+ (CircularIndex a, int b)
    {
        CircularIndex c = new CircularIndex(a.MaxValue);
        c.Index += b;
        return c;
    }

    public static CircularIndex operator -(CircularIndex a, int b)
    {
        CircularIndex c = new CircularIndex(a.MaxValue);
        c.Index -= b;
        return c;
    }
}
