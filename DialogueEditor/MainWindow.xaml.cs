using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Microsoft.Win32;

namespace DialogueEditor
{
    public partial class MainWindow : Window
    {
        CDialogue dialogue = new CDialogue();
        TreeViewItem selectedMessage = null;
        TreeViewItem selectedAnswer = null;

        public MainWindow() { InitializeComponent(); }

        // ─── TreeView ───────────────────────────────────────────────
        private void dlg_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            if (dlg.SelectedItem == null) return;
            TreeViewItem item = dlg.SelectedItem as TreeViewItem;

            if ((string)item.Tag == "message")
            {
                selectedMessage = item;
                long msgID = (long)selectedMessage.Header;
                txt.Text = dialogue.selectMessage(msgID);
                lbMessage.Content = msgID;
                lbAnswer.Content = "-";
                linkedMsg.Content = "-";
            }
            else
            {
                selectedMessage = item.Parent as TreeViewItem;
                long msgID = (long)selectedMessage.Header;
                long answID = (long)item.Header;
                txt.Text = dialogue.selectAnswer(msgID, answID);
                selectedAnswer = item;
                lbAnswer.Content = answID;
                lbMessage.Content = msgID;
                linkedMsg.Content = dialogue.linkedUID();
            }
        }

        // ─── Добавить сообщение ──────────────────────────────────────
        private void AddMsgButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt.Text)) return;

            CMessage msg = new CMessage();
            msg.text = txt.Text;

            TreeViewItem item = new TreeViewItem();
            item.Tag = "message";
            item.Header = dialogue.addMessage(msg);
            item.IsExpanded = true;
            item.IsSelected = true;

            dlg.Items.Add(item);
            selectedMessage = item;
        }

        // ─── Добавить ответ ──────────────────────────────────────────
        private void AddAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMessage == null || string.IsNullOrWhiteSpace(txt.Text)) return;

            CAnswer answ = new CAnswer();
            answ.text = txt.Text;

            string action = (answAction.SelectedItem as ComboBoxItem).Content.ToString();

            TreeViewItem item = new TreeViewItem();
            item.Tag = "answer";
            item.Header = dialogue.addAnswer(answ, action);

            selectedMessage.Items.Add(item);
            item.IsSelected = true;
            selectedAnswer = item;
        }

        // ─── Связать ответ с сообщением ──────────────────────────────
        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAnswer == null || selectedMessage == null) return;
            dialogue.connectAnswer((long)selectedMessage.Header);
            linkedMsg.Content = dialogue.linkedUID();
        }

        // ─── Редактировать ───────────────────────────────────────────
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dlg.SelectedItem == null) return;
            TreeViewItem item = dlg.SelectedItem as TreeViewItem;

            if ((string)item.Tag == "message")
            {
                dialogue.updateMessage(txt.Text);
            }
            else
            {
                string action = (answAction.SelectedItem as ComboBoxItem).Content.ToString();
                dialogue.updateAnswer(txt.Text, action);
            }
        }

        // ─── Удалить ─────────────────────────────────────────────────
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dlg.SelectedItem == null) return;
            TreeViewItem item = dlg.SelectedItem as TreeViewItem;

            if ((string)item.Tag == "message")
            {
                dialogue.removeMessage((long)item.Header);
                dlg.Items.Remove(item);
                selectedMessage = null;
            }
            else
            {
                long msgID = (long)selectedMessage.Header;
                dialogue.removeAnswer(msgID, (long)item.Header);
                selectedMessage.Items.Remove(item);
                selectedAnswer = null;
            }
        }

        // ─── Очистить ────────────────────────────────────────────────
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            dlgClear();
        }

        private void dlgClear()
        {
            dialogue.Clear();
            dlg.Items.Clear();
            selectedMessage = null;
            selectedAnswer = null;
            lbMessage.Content = "-";
            lbAnswer.Content = "-";
            linkedMsg.Content = "-";
            txt.Clear();
        }

        // ─── Сохранить в XML ─────────────────────────────────────────
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode rootNode = xmlDoc.CreateElement("messages");
            XmlAttribute attr = xmlDoc.CreateAttribute("uid");
            attr.Value = dialogue.getLastUID().ToString();
            rootNode.Attributes.Append(attr);
            xmlDoc.AppendChild(rootNode);

            foreach (CMessage msg in dialogue.getMessages())
            {
                XmlNode msgNode = xmlDoc.CreateElement("message");
                attr = xmlDoc.CreateAttribute("uid");
                attr.Value = msg.msgID.ToString();
                msgNode.Attributes.Append(attr);
                msgNode.InnerText = msg.text;

                XmlNode answersNode = xmlDoc.CreateElement("answers");
                foreach (CAnswer answ in msg.answers)
                {
                    XmlNode answNode = xmlDoc.CreateElement("answer");

                    attr = xmlDoc.CreateAttribute("auid");
                    attr.Value = answ.answID.ToString();
                    answNode.Attributes.Append(attr);

                    attr = xmlDoc.CreateAttribute("muid");
                    attr.Value = answ.msgID.ToString();
                    answNode.Attributes.Append(attr);

                    attr = xmlDoc.CreateAttribute("action");
                    attr.Value = answ.action;
                    answNode.Attributes.Append(attr);

                    answNode.InnerText = answ.text;
                    answersNode.AppendChild(answNode);
                }
                msgNode.AppendChild(answersNode);
                rootNode.AppendChild(msgNode);
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML files (*.xml)|*.xml";
            if (saveDialog.ShowDialog() == true)
                xmlDoc.Save(saveDialog.FileName);
        }

        // ─── Загрузить из XML ─────────────────────────────────────────
        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog loadDialog = new OpenFileDialog();
            loadDialog.Filter = "XML files (*.xml)|*.xml";
            if (loadDialog.ShowDialog() != true) return;

            dlgClear();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(loadDialog.FileName);

            XmlNode messages = xmlDoc.SelectSingleNode("//messages");
            dialogue.setLastUID(long.Parse(messages.Attributes["uid"].Value));

            XmlNodeList messageNodes = xmlDoc.SelectNodes("//messages/message");
            foreach (XmlNode messageNode in messageNodes)
            {
                CMessage msg = new CMessage();
                msg.text = messageNode.ChildNodes[0].InnerText;
                msg.msgID = long.Parse(messageNode.Attributes["uid"].Value);
                dialogue.loadMessage(msg);

                TreeViewItem msgItem = new TreeViewItem();
                msgItem.Tag = "message";
                msgItem.Header = msg.msgID;
                msgItem.IsExpanded = true;
                msgItem.IsSelected = true;
                dlg.Items.Add(msgItem);
                selectedMessage = msgItem;

                foreach (XmlNode answNode in messageNode.ChildNodes[1].ChildNodes)
                {
                    CAnswer answ = new CAnswer();
                    answ.answID = long.Parse(answNode.Attributes["auid"].Value);
                    answ.msgID = long.Parse(answNode.Attributes["muid"].Value);
                    answ.action = answNode.Attributes["action"].Value;
                    answ.text = answNode.InnerText;
                    dialogue.loadAnswer(answ);

                    TreeViewItem answItem = new TreeViewItem();
                    answItem.Tag = "answer";
                    answItem.Header = answ.answID;
                    selectedMessage.Items.Add(answItem);
                }
            }
        }
    }
}