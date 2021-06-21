## Form1
```csharp
using System;
using System.Windows.Forms;
using System.Xml;

namespace TableXmlMaker
{
    public partial class Form1 : Form
    {
        XmlDocument xmlFile = new XmlDocument();
        bool isChecked = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void WriteColumn(string selecter)
        {
            string url = label1.Text.ToString();
            xmlFile.Load(url);
            XmlNodeList tables = xmlFile.SelectNodes("./Tables/Table");
            XmlElement element = xmlFile.DocumentElement;
            
            if (isChecked)
            {
                foreach (XmlNode table in tables)
                {
                    if (table.Attributes["Name"].Value == selecter)
                    {
                        XmlNodeList columns = table.SelectNodes("./Columns/Column");
                        foreach (XmlNode column in columns)
                        {
                            var hasAttribute = column.Attributes["PrimaryKey"];
                            if (hasAttribute != null)
                            {
                                listBox2.Items.Add(column.Attributes["Name"].Value + " (Primary Key)" + Environment.NewLine);
                            }
                        }
                        break;
                    }
                }
            } else
            {
                foreach (XmlNode table in tables)
                {
                    if (table.Attributes["Name"].Value == selecter)
                    {
                        XmlNodeList columns = table.SelectNodes("./Columns/Column");
                        foreach (XmlNode column in columns)
                        {
                            listBox2.Items.Add(column.Attributes["Name"].Value + Environment.NewLine);
                        }
                        break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = "";
            dialog.Filter = "XML 파일|*.xml";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                label1.Text = dialog.FileName;
            else
                label1.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = label1.Text.ToString();

            try
            {
                xmlFile.Load(url);
                XmlElement root = xmlFile.DocumentElement;
                XmlNodeList tables = root.ChildNodes;
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                foreach (XmlNode table in tables)
                {
                    string nodeString;
                    nodeString = table.Attributes["Name"].Value;

                    listBox1.Items.Add(nodeString);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                isChecked = true;
            else
                isChecked = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string selecter = listBox1.SelectedItem.ToString();
            WriteColumn(selecter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newform2 = new Form2();
            newform2.Show();
        }
    }
}
```

# Form2

``` csharp
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TableXmlMaker
{
    public partial class Form2 : Form
    {
        XmlDocument xmlFile = new XmlDocument();
        XmlReader reader;
        string url = "";

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "";
            dialog.DefaultExt = "";
            dialog.Filter = "XML 파일|*.xml";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
                label1.Text = dialog.FileName;
            else
                label1.Text = "";

            url = label1.Text.ToString();
            reader = XmlReader.Create(url);
            treeView1.Nodes.Clear();

            try
            {
                xmlFile.Load(url);
                XmlElement root = xmlFile.DocumentElement;
                XmlNodeList tables = root.ChildNodes;

                treeView1.Nodes.Add(new TreeNode(xmlFile.DocumentElement.Name));
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

        private void AddTreeNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;

            xmlFile.Load(url);

            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for(int i=0; i<nodeList.Count; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddTreeNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = (inXmlNode.OuterXml).Trim();
            }
        }

        private void PrintTreeNode(TreeNode inputNode)
        {
            foreach(TreeNode childNode in inputNode.Nodes)
            {
                textBox1.Text += childNode.Text + Environment.NewLine;
                PrintTreeNode(childNode);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Clear();
            TreeNode selectedNod = treeView1.SelectedNode;
            PrintTreeNode(selectedNod);
        }

        private void 저장ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("값이 존재하지 않습니다");
                return;
            }
            StreamWriter saveFile;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = label1.Text;
            DialogResult result = dialog.ShowDialog();
            string uri = "";

            if (result == DialogResult.OK)
                uri = dialog.SelectedPath;

            saveFile = File.CreateText(uri+"\\node.txt");
            saveFile.Write(textBox1.Text);
            saveFile.Close();
        }
    }
}
```
