## Form1

![image](https://user-images.githubusercontent.com/18677603/124420455-71454800-dd9a-11eb-8ba0-421607586b87.png)


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

![image](https://user-images.githubusercontent.com/18677603/124424879-b5d4e180-dda2-11eb-912a-a74614611c2c.png)


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
        OpenFileDialog openDialog = new OpenFileDialog();
        FolderBrowserDialog saveDialog = new FolderBrowserDialog();
        StreamWriter saveFile;
        


        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openDialog.FileName = "";
            openDialog.DefaultExt = "";
            openDialog.Filter = "XML 파일|*.xml";
            DialogResult result = openDialog.ShowDialog();
            if (result == DialogResult.OK)
                label1.Text = openDialog.FileName;
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

        private void AddTreeNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            TreeNode tNode;
            XmlNode xNode;
            XmlNodeList nodeList;
            XmlAttribute hasAttribute;

            xmlFile.Load(url);

            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for(int i=0; i<nodeList.Count; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    hasAttribute = xNode.Attributes["Name"];
                    if(hasAttribute == null)
                    {
                        inTreeNode.Nodes.Add(new TreeNode("<" + xNode.Name + " />"));
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

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("값이 존재하지 않습니다");
                return;
            }
            DialogResult result = saveDialog.ShowDialog();
            string uri = string.Empty;

            if (result == DialogResult.OK)
                uri = saveDialog.SelectedPath;

            saveFile = File.CreateText(uri + "\\node.txt");
            saveFile.Write(textBox1.Text);
            saveFile.Close();
        }
    }
}
```
