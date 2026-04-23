using DialogueEditor;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void ActionDelegate();

public class DialogueSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogueWindow;
    public GameObject answers;
    public TMP_Text message;
    public TMP_Text answerPrefab;   // ← prefab Answer

    private Dictionary<string, ActionDelegate> actions
        = new Dictionary<string, ActionDelegate>();
    private CDialogue dialogue = new CDialogue();

    // ─── Загрузка диалога из TextAsset (XML) ─────────────────────
    public void loadDialogue(TextAsset xmlFile)
    {
        dialogue.Clear();
        actions.Clear();
        actions["none"] = null;
        actions["dialogue end"] = dialogueEnd;
        actions["door open"] = null;   // назначается снаружи через setAction
        actions["increaseStrengthByOne"] = null;
        actions["increaseStrengthByTwo"] = null;
        actions["increaseIntelligenceByOne"] = null;
        actions["increaseIntelligenceByTwo"] = null;
        actions["CheckStrength"] = null;
        actions["CheckIntelligence"] = null;


        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);

        XmlNodeList messageNodes = xmlDoc.SelectNodes("//messages/message");
        foreach (XmlNode messageNode in messageNodes)
        {
            CMessage msg = new CMessage();
            msg.text = messageNode.ChildNodes[0].InnerText;
            msg.msgID = long.Parse(messageNode.Attributes["uid"].Value);
            dialogue.loadMessage(msg);

            foreach (XmlNode answNode in messageNode.ChildNodes[1].ChildNodes)
            {
                CAnswer answ = new CAnswer();
                answ.answID = long.Parse(answNode.Attributes["auid"].Value);
                answ.msgID = long.Parse(answNode.Attributes["muid"].Value);
                answ.action = answNode.Attributes["action"].Value;
                answ.text = answNode.InnerText;
                dialogue.loadAnswer(answ);
            }
        }

        showMessage(dialogue.getMessages()[0].msgID, "none");
        dialogueWindow.SetActive(true);
    }

    // ─── Показать сообщение ───────────────────────────────────────
    public void showMessage(long uid, string act)
    {
        if (actions.ContainsKey(act)) actions[act]?.Invoke();
        if (uid == -1) return;

        foreach (Transform child in answers.transform)
            Destroy(child.gameObject);

        message.text = dialogue.selectMessage(uid);

        foreach (CAnswer ans in dialogue.getAnswers())
        {
            long capturedMsgID = ans.msgID;
            string capturedAct = ans.action;

            TMP_Text btn = Instantiate(answerPrefab, answers.transform);
            btn.text = ans.text;
            btn.GetComponent<Button>().onClick.AddListener(() =>
                showMessage(capturedMsgID, capturedAct));
        }
    }

    public void dialogueEnd()
    {
        dialogueWindow.SetActive(false);
    }

    public void setAction(string name, ActionDelegate act)
    {
        if (actions.ContainsKey(name))
            actions[name] = act;
    }
}