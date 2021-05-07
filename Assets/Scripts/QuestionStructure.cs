using UnityEngine;
[System.Serializable]
public struct QustionStruct
{
    public string question;
    [Space(3)]
    [Header("Answers")]
    public string trueAnswer;
    public string answer1;
    public string answer2;
    public string answer3;
    [Space(3)]
    [Header("Helper")]
    public string help;
}