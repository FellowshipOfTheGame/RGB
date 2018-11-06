using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public Transform m_limitUp;
    public Transform m_limitDown;
    public Transform m_limitLeft;
    public Transform m_limitRight;

    public static float MaxY { get { return m_manager.m_limitUp.position.y; } }
    public static float MinY { get { return m_manager.m_limitDown.position.y; } }
    public static float MaxX { get { return m_manager.m_limitRight.position.x; } }
    public static float MinX { get { return m_manager.m_limitLeft.position.x; } }

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static SceneMgr m_manager;


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - SceneMgr : scene manager must be unique!");
        m_manager = this;
    }
}
