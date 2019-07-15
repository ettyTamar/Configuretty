using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;

namespace Configuretty
{
    public partial class Form1 : Form
    {
        string m_filepath = "";
        XmlDocument xdoc = new XmlDocument();
        List<string> namesList = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> GetNamesFromXml(string filepath)
        {
            xdoc.Load(m_filepath);

            List<string> namesList = new List<string>();

            foreach (XmlNode node in xdoc.SelectNodes("//Mappings/Descriptions/Description/Name"))
            {
                string name = node.InnerText;

                namesList.Add(name);
            }

            return namesList.Distinct().ToList();
        }

        private void LINK_Click(object sender, EventArgs e)
        {
            string regex = @"^\d{1,9}[':']\d{1,9}[':']\d{1,9}[':']\d{1,9}[':']\d{1,9}[':']\d{1,9}[':']\d{1,9}";
            string regex2 = @"^[A-Z]{1,2}";

            if (!Regex.IsMatch(textBox1.Text, regex))
            {
                MessageBox.Show("Please enter a valid number separated by : ");
                textBox1.Text = string.Empty;
                return;
            }

            if (GetNamesFromXml(m_filepath).Contains(textBox2.Text))
            {
                MessageBox.Show("The name is already exists!");
                textBox2.Text = String.Empty;
                textBox2.Focus();
                return;
            }

            if (!Regex.IsMatch(textBox3.Text, regex2))
            {
                MessageBox.Show("Please enter only a valid country shortcat");
                textBox3.Text = string.Empty;
                return;
            }

            xdoc.Load(m_filepath);

            List<string> namesList = new List<string>();

            foreach (XmlNode node in xdoc.SelectNodes("//Mappings/Descriptions/Description/Name"))
            {
                string name = node.InnerText;

                namesList.Add(name);
            }

            xdoc.Load(m_filepath);
            XmlNode descriptionsNode = xdoc.SelectSingleNode("//Mappings/Descriptions");
            XmlNode description = xdoc.CreateElement("Description");
            descriptionsNode.SelectSingleNode("//Mappings/Descriptions/").AppendChild(description);
            XmlNode Name = xdoc.CreateElement("Name");
            Name.InnerText = textBox2.Text;
            description.AppendChild(Name);
            XmlNode dis = xdoc.CreateElement("DIS");
            dis.InnerText = textBox1.Text;
            description.AppendChild(dis);
            XmlNode battleDimension = xdoc.CreateElement("BattleDimension");
            battleDimension.InnerText = comboBox1.SelectedItem.ToString();
            description.AppendChild(battleDimension);
            XmlNode country = xdoc.CreateElement("Country");
            country.InnerText = textBox3.Text;
            description.AppendChild(country);
            XmlNode function = xdoc.CreateElement("Function");
            function.InnerText = textBox4.Text;
            description.AppendChild(function);
            XmlNode platformId = xdoc.CreateElement("platformId");
            description.AppendChild(platformId);
            xdoc.DocumentElement.AppendChild(description);
            xdoc.Save(m_filepath);
            MessageBox.Show("Entity added succefully");



            //return namesList.Distinct().ToList();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            m_filepath = openFileDialog1.FileName;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void Load_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"m_filepath");
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
