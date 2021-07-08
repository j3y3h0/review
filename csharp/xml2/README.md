# XML 파싱 및 데이터베이스 연동 프로그램(미완성 개발중)

![image](https://user-images.githubusercontent.com/85467436/124886822-91ca0800-e00f-11eb-8671-affca910803c.png)

```c#
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TableXmlMaker
{
    public partial class Form1 : Form
    {
        XmlDocument xmlFile = new XmlDocument();
        XmlReader reader;

        OpenFileDialog dialog = new OpenFileDialog();
        FolderBrowserDialog saveDialog = new FolderBrowserDialog();
        StreamWriter saveFile;

        List<string> idList = new List<string>(); //XML파일의 테이블 ID를 임시로 담을 리스트 배열

        string directory = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void 열기ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dialog.FileName = "";
            dialog.DefaultExt = "";
            dialog.Filter = "XML 파일|*.xml";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                label2.Text = dialog.FileName;
                directory = label2.Text;
                reader = XmlReader.Create(directory);
            }
            else
                label2.Text = "";

            treeView1.Nodes.Clear();

            try
            {
                xmlFile.Load(directory);
                XmlElement root = xmlFile.DocumentElement;
                XmlNodeList tables = root.ChildNodes;

                treeView1.Nodes.Add(new TreeNode("<" + xmlFile.DocumentElement.Name + " />"));
                TreeNode tNode = new TreeNode();
                tNode = treeView1.Nodes[0];

                AddTreeNode(xmlFile.DocumentElement, tNode);
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNod = treeView1.SelectedNode;
            if(selectedNod.Text.IndexOf("Tables") > 0)
            {
                MessageBox.Show("올바르지 않은 선택 : Tables");
                return;
            }
            else if (selectedNod.Text.IndexOf("Column") > 0)
            {
                MessageBox.Show("올바르지 않은 선택 : Column");
                return;
            }
            else
            {
                if (idList.Contains(selectedNod.Text))
                    return;
                else
                {
                    idList.Add(selectedNod.Text);
                    PrintTreeNode(selectedNod);
                    textBox1.Text += Environment.NewLine;
                }
            }
        }

        private void PrintTreeNode(TreeNode inputNode)
        {
            foreach (TreeNode childNode in inputNode.Nodes)
            {
                textBox1.Text += childNode.Text + Environment.NewLine;
                PrintTreeNode(childNode);
            }
        }

        private void AddTreeNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            TreeNode tNode;
            XmlNode xNode;
            XmlNodeList nodeList;
            XmlAttribute hasAttribute;

            xmlFile.Load(directory);

            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;

                for (int i = 0; i < nodeList.Count; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    hasAttribute = xNode.Attributes["Name"];
                    if (hasAttribute == null)
                    {
                        inTreeNode.Nodes.Add(new TreeNode("<" + xNode.Name + ">"));
                    }
                    else
                    {
                        inTreeNode.Nodes.Add(new TreeNode("<" + xNode.Attributes["Name"].Value + ">"));
                    }
                    tNode = inTreeNode.Nodes[i];
                    AddTreeNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }

        private void 저장ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string url;
            if (textBox1.Text == "")
            {
                MessageBox.Show("값이 존재하지 않습니다.");
                return;
            }

            DialogResult result = saveDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                url = saveDialog.SelectedPath;
                saveFile = File.CreateText(url + "\\node.txt");
                saveFile.Write(textBox1.Text);
                saveFile.Close();
            }

        }

        private void 초기화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                treeView1.Nodes.Clear();
                textBox1.Clear();
                idList.Clear();
                label2.Text = "Directory";
        }
    }
}

```
