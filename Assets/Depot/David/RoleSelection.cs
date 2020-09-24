using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class RoleSelection : MonoBehaviour
{
    private Player m_Player1;
    private Player m_Player2;

    [SerializeField]
    private GameObject m_ControllerP1;
    [SerializeField]
    private GameObject m_ControllerP2;
    
    private Vector3 m_OriginalPositionP1;
    private Vector3 m_OriginalPositionP2;

    // Available position for Coroboy (if one is taken, the other is used)
    [SerializeField]
    private Transform m_PositionUnderCoroboy;

    // Available position for Corocop
    [SerializeField]
    private Transform m_PositionUnderCorocop;

    // To Track Players Position
    [SerializeField]
    private int m_PositionP1 = 0;
    [SerializeField]
    private int m_PositionP2 = 0;


    [SerializeField]
    private bool m_P1isMoving;
    [SerializeField]
    private bool m_P2isMoving;

    void Start()
    {
        DontDestroyOnLoad(this);
        Cursor.visible = false;
        m_OriginalPositionP1 = m_ControllerP1.transform.position;
        m_OriginalPositionP2 = m_ControllerP2.transform.position;

        m_Player1 = ReInput.players.GetPlayer(0);
        m_Player2 = ReInput.players.GetPlayer(1);
    }

    void Update()
    {
        CharacterSelectionManager();
    }

    void CharacterSelectionManager()
    {
        // Player 1

        if (m_Player1.GetAxis("MoveLeftRight") < 0 && !m_P1isMoving)
        {
            m_P1isMoving = true;
            if (m_PositionP1 == 1)
            {
                m_ControllerP1.transform.position = m_OriginalPositionP1;
                m_PositionP1--;
            }
            else if (m_PositionP1 == 0 && m_PositionP2 != -1)
            {
                m_ControllerP1.transform.position = m_PositionUnderCoroboy.position;
                m_PositionP1--;
            }
            else
            {
                //Invalid Position (feedback)
            }

            Debug.Log("Gauche");
        }
        else if (m_Player1.GetAxis("MoveLeftRight") > 0 && !m_P1isMoving)
        {
            m_P1isMoving = true;
            if (m_PositionP1 < 0)
            {
                m_ControllerP1.transform.position = m_OriginalPositionP1;
                m_PositionP1++;
            }
            else if (m_PositionP1 == 0 && m_PositionP2 != 1)
            {
                m_PositionP1++;
                m_ControllerP1.transform.position = m_PositionUnderCorocop.position;
            }
            else
            {
                //Invalid Position (feedback)
            }

            Debug.Log("Droite");
        }
        else if (Input.GetAxis("Horizontal") == 0 && m_P1isMoving)
        {
            m_P1isMoving = false;
        }


        // Player 2

        if (m_Player2.GetAxis("MoveLeftRight") < 0 && !m_P2isMoving)
        {
            m_P2isMoving = true;
            if (m_PositionP2 == 1)
            {
                m_ControllerP2.transform.position = m_OriginalPositionP2;
                m_PositionP2--;
            }
            else if (m_PositionP2 == 0 && m_PositionP1 != -1)
            {
                m_ControllerP2.transform.position = m_PositionUnderCoroboy.position;
                m_PositionP2--;
            }
            else
            {
                //Invalid Position (feedback)
            }

            Debug.Log("Gauche");
        }
        else if (m_Player2.GetAxis("MoveLeftRight") > 0 && !m_P2isMoving)
        {
            m_P2isMoving = true;
            if (m_PositionP2 < 0)
            {
                m_ControllerP2.transform.position = m_OriginalPositionP2;
                m_PositionP2++;
            }
            else if (m_PositionP2 == 0 && m_PositionP1 != 1)
            {
                m_PositionP2++;
                m_ControllerP2.transform.position = m_PositionUnderCorocop.position;
            }
            else
            {
                //Invalid Position (feedback)
            }

            Debug.Log("Droite");
        }
        else if (m_Player2.GetAxis("MoveLeftRight") == 0 && m_P2isMoving)
        {
            m_P2isMoving = false;
        }

        if(m_Player2.GetButtonDown("Start") || m_Player1.GetButtonDown("Start"))
        {
            if (CheckIfPositionIsOK())
            {
                PlayerPrefs.SetInt("P1Choice", m_PositionP1);
                PlayerPrefs.SetInt("P2Choice", m_PositionP2);

                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("NOPE");
            }
        }

    }

    bool CheckIfPositionIsOK()
    {
        if (m_PositionP1 != m_PositionP2 && m_PositionP1 != 0 && m_PositionP2 != 0)
            return true;
        return false;
    }
    /*
    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameManager.instance.p1Choice = m_P1Choice;
            GameManager.instance.p2Choice = m_P2Choice;
            Destroy(gameObject);
        }
    }
    */
}
