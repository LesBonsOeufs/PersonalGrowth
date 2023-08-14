using TMPro;
using UnityEngine;

public class BQSettableText : BinaryQuestion
{
    [SerializeField] private TextMeshProUGUI questionSlot;
    [SerializeField] private TextMeshProUGUI textSlotYes;
    [SerializeField] private TextMeshProUGUI textSlotNo;

    public void SetQuestionTexts(string question, string answerYes, string answerNo)
    {
        questionSlot.text = question;
        textSlotYes.text = answerYes;
        textSlotNo.text = answerNo;
    }
}
