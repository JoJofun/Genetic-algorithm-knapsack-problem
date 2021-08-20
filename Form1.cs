using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnapsackGenetic
{
    public partial class Form1 : Form
    {

        int[] weights = new int[10] { 6, 14, 13, 9, 11, 16, 20, 17, 3, 5 };
        int[] values = new int[10] { 18, 60, 47, 55, 53, 72, 90, 83, 21, 16 };
        int chsize = 32;


        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                textBox3.AppendText("Item " + i + " W: " + (weights[i].ToString()).PadLeft(2) + " V: " + (values[i].ToString()).PadLeft(2) + System.Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            int gensiz = decimal.ToInt32(numericUpDown3.Value);
            progressBar1.Maximum = gensiz;
            
            double mutrate = decimal.ToDouble(numericUpDown2.Value);
            chsize = decimal.ToInt32(numericUpDown1.Value);
            textBox1.Text = "Generation 1_____________________";
            chromosome mychromosome = new chromosome(weights, values, 35, chsize, mutrate);
            for (int i = 0; i < chsize; i++)
            {
                textBox1.AppendText(System.Environment.NewLine + "Chromosome " + (i + 1) + ' ' + mychromosome.str_chrom(i) + " Fitness: " + mychromosome.fitness(i));
                progressBar1.Increment(1);
            }
            

            for (int j = 1; j < gensiz; j++)
            {
                mychromosome.new_generation();
                textBox1.AppendText(System.Environment.NewLine + System.Environment.NewLine + "Generation " + (j + 1) + "_____________________");
                for (int i = 0; i < chsize; i++)
                {
                    progressBar1.Increment(1);
                    textBox1.AppendText(System.Environment.NewLine + "Chromosome " + (i + 1) + ' ' + mychromosome.str_chrom(i) + " Fitness: " + mychromosome.fitness(i));
                }
            }
            textBox2.Text = (mychromosome.str_best());
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
