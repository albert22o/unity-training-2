using DialogueEditor;
using System.Collections.Generic;

public class CDialogue
{
    private List<CMessage> messages = new List<CMessage>();
    private long UID = 0;
    private CMessage selectedMessage = null;
    private CAnswer selectedAnswer = null;

    private long getUID() { UID++; return UID; }

    public CMessage findMsg(long msgID)
    {
        foreach (CMessage msg in messages)
            if (msg.msgID == msgID) return msg;
        return null;
    }

    public CAnswer findAnsw(long answID)
    {
        foreach (CAnswer answ in selectedMessage.answers)
            if (answ.answID == answID) return answ;
        return null;
    }

    public string selectMessage(long msgID)
    {
        selectedMessage = findMsg(msgID);
        return selectedMessage.text;
    }

    public string selectAnswer(long msgID, long answID)
    {
        selectMessage(msgID);
        selectedAnswer = findAnsw(answID);
        return selectedAnswer.text;
    }

    public List<CMessage> getMessages() => messages;

    public List<CAnswer> getAnswers() => selectedMessage.answers;

    public long linkedUID() => selectedAnswer?.msgID ?? -1;

    public void Clear()
    {
        messages.Clear();
        UID = 0;
        selectedMessage = null;
        selectedAnswer = null;
    }

    public void loadMessage(CMessage msg)
    {
        messages.Add(msg);
        selectedMessage = msg;
    }

    public void loadAnswer(CAnswer answ)
    {
        selectedMessage.answers.Add(answ);
    }

    public void setLastUID(long uid) { UID = uid; }
}