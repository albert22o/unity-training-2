namespace DialogueEditor
{
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

        public long addMessage(CMessage msg)
        {
            msg.msgID = getUID();
            messages.Add(msg);
            selectedMessage = msg;
            return msg.msgID;
        }

        public long addAnswer(CAnswer answ, string action)
        {
            answ.answID = getUID();
            answ.action = action;
            selectedMessage.answers.Add(answ);
            return answ.answID;
        }

        public void connectAnswer(long msgID)
        {
            selectedAnswer.msgID = msgID;
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
            return selectedAnswer.text + " [action: " + selectedAnswer.action + "]";
        }

        public void removeMessage(long msgID)
        {
            selectMessage(msgID);
            messages.Remove(selectedMessage);
            selectedMessage = null;
        }

        public void removeAnswer(long msgID, long answID)
        {
            selectMessage(msgID);
            selectAnswer(msgID, answID);
            selectedMessage.answers.Remove(selectedAnswer);
            selectedAnswer = null;
        }

        public void updateMessage(string text)
        {
            selectedMessage.text = text;
        }

        public void updateAnswer(string text, string action)
        {
            selectedAnswer.text = text;
            selectedAnswer.action = action;
        }

        public List<CMessage> getMessages() => messages;

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

        public long getLastUID() => UID;

        public void setLastUID(long uid) { UID = uid; }
    }
}