using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instanse;

    //������� ���-�� ���������� ��������
    public int Score { get; private set;}
    //������ ����� �������� ������
    public int Level { get; private set;}

    private void Awake()
    {
        instanse = this;

        if(PlayerPrefs.HasKey("Score") && PlayerPrefs.HasKey("Level"))
        {
            Score = PlayerPrefs.GetInt("Score");
            Level = PlayerPrefs.GetInt("Level");
        }
    }

    public void AddScore()
    {
        Score++;
        PlayerPrefs.SetInt("Score",Score);
        PlayerPrefs.Save();
    }

    public void ClearScore()
    {
        Score=0;
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.Save();
    }

    public void SaveLevel(int _level)
    {
        Level = _level;
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.Save();
    }
}
