using System.Threading.Tasks;

public class PopUpQuestion : BinaryQuestion
{
    public async override Task<bool> AskBinaryQuestion(string question, string answerYes, string answerNo)
    {
        gameObject.SetActive(true);
        bool lAnswer = await base.AskBinaryQuestion(question, answerYes, answerNo);
        gameObject.SetActive(false);

        return lAnswer;
    }
}