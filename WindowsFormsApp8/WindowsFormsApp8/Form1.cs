/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
 *Curso Técnico em Informática - Data de Entrega: 20 / 05 / 2024
 * Autores do Projeto: Eduarda Moreira e Gustavo Zago
 *
 * Turma: 3ºIID
 * Atividade Proposta em aula
 * Observação: muito bonitinho
 * 
 * 
 * ******************************************************************/
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.IO;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (arquivo == false)
            {
                selectedIndex = comboBox1.SelectedIndex;
                espessura = comboBox2.SelectedIndex;
                if (espessura <= 0)
                    espessura = 3;
            }

            if (selectedIndex <= 0)
                linha = normal;
            else if (selectedIndex == 1)
                linha = dash;
            else if (selectedIndex == 2)
                linha = dot;
            else if (selectedIndex == 3)
                linha = dashdot;
            else if (selectedIndex == 4)
                linha = dashdotdot;

            if (forma == 1)
            {
                DesenhaReta(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x, y);
                click = 0;
            }
            else if (forma == 2)
            {
                DesenhaLosango(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x, y);
                click = 0;
            }
            else if (forma == 3)
            {
                if (s == true)
                {
                    DesenhaARC(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x[0], y[0], a, 0, 360);
                    click = 0;
                    s = false;
                }
            }
            else if (forma == 4)
            {
                if (s == true)
                {
                    DesenhaRetangulo(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x, y, b, a);
                    click = 0;
                    s = false;
                }
            }
            else if (forma == 5)
            {
                if (s == true)
                {
                    DesenhaElipse(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x[0], y[0], a, b, 0, 360);
                    click = 0;
                    s = false;

                }
            }
            else if (forma == 6)
            {
                DesenhaPentagono(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x, y);
                click = 0;
            }
            else if (forma == 7)
            {
                DesenhaTriangulo(e, EstiloLinha(e, linha, caneta(corPaint, espessura)), x, y);
                click = 0;
            }
            arquivo = false;
        }
        int forma, a, b, click, selectedIndex, espessura = 0;
        float[] dash = { 5, 2 };          // DASH - traços longos intercalados com espaços curtos
        float[] dot = { 1, 2 };           // DOT - pontos espaçados uniformemente
        float[] dashdot = { 5, 2, 1, 2 }; // DASHDOT - traços longos intercalados com pontos
        float[] dashdotdot = { 5, 2, 1, 2, 1, 2 }; // DASHDOTDOT - traços longos intercalados com pontos e espaços curtos
        float[] normal = { 1 };
        int[] x = new int[6];
        int[] y = new int[6];
        float[] linha;
        bool s, f, arquivo = false;

        Color corPaint = Color.FromArgb(0, 0, 0);
        private Pen caneta(Color cor, int espessura)
        {
            return new Pen(cor, espessura);
        }

        public void ponto(PaintEventArgs e, Pen caneta, int x1, int y1, int x2, int y2)
        {
            e.Graphics.DrawLine(caneta, x1, y1, x2 + 1, y2);
        }

        public void DesenhaReta(PaintEventArgs e, Pen caneta, int[] x, int[] y)
        {
            ponto(e, caneta, x[0], y[0], x[1], y[1]);
        }
        private Pen EstiloLinha(PaintEventArgs e, float[] estilo, Pen caneta)
        {
            caneta.DashPattern = estilo;
            return caneta;
        }
        public void DesenhaARC(PaintEventArgs e, Pen caneta, int x1, int y1, int raio, int ti, int tf)
        {
            for (int teta = ti; teta <= tf; teta++)
            {
                double rad = teta * Math.PI / 180; // Conversão de graus para radianos
                double xd = x1 + raio * Math.Cos(rad);
                int xi = (int)xd;
                double yd = y1 + raio * Math.Sin(rad);
                int yi = (int)yd;
                ponto(e, caneta, xi, yi, xi, yi);
            }
        }

        public void DesenhaRetangulo(PaintEventArgs e, Pen caneta, int[] x, int[] y, int largura, int altura)
        {
            e.Graphics.DrawRectangle(caneta, x[0], y[0], largura, altura);
        }
        public void DesenhaTriangulo(PaintEventArgs e, Pen caneta, int[] x1, int[] y1)
        {
            for (int i = 0; i <= 2; i++)
            {
                if (i == 0) ponto(e, caneta, x1[2], y1[2], x1[0], y1[0]);
                if (i != 2) ponto(e, caneta, x1[i], y1[i], x1[i + 1], y1[i + 1]);
            }
        }
        public void DesenhaLosango(PaintEventArgs e, Pen caneta, int[] x, int[] y)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (i == 0) ponto(e, caneta, x[3], y[3], x[0], y[0]);
                if (i != 3) ponto(e, caneta, x[i], y[i], x[i + 1], y[i + 1]);
            }
        }

        public void DesenhaPentagono(PaintEventArgs e, Pen caneta, int[] x, int[] y)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (i == 0) ponto(e, caneta, x[4], y[4], x[0], y[0]);
                if (i != 4) ponto(e, caneta, x[i], y[i], x[i + 1], y[i + 1]);
            }
        }
        public void DesenhaElipse(PaintEventArgs e, Pen caneta, int x1, int y1, int raiox, int raioy, int ti, int tf)
        {
            for (int teta = ti; teta <= tf; teta++)
            {
                double rad = teta * Math.PI / 180; // Conversão de graus para radianos
                double xd = x1 + raiox * Math.Cos(rad);
                int xi = (int)xd;
                double yd = y1 + raioy * Math.Sin(rad);
                int yi = (int)yd;
                ponto(e, caneta, xi, yi, xi, yi);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(0, 0, 0);
            button28.BackColor = corPaint;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(255, 255, 255);
            button28.BackColor = corPaint;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(64, 64, 64);
            button28.BackColor = corPaint;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(171, 171, 171);
            button28.BackColor = corPaint;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(128, 0, 0);
            button28.BackColor = corPaint;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(128, 64, 0);
            button28.BackColor = corPaint;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(192, 0, 0);
            button28.BackColor = corPaint;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(255, 192, 192);
            button28.BackColor = corPaint;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(218, 165, 32);
            button28.BackColor = corPaint;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(255, 165, 0);
            button28.BackColor = corPaint;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(255, 255, 0);
            button28.BackColor = corPaint;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(255, 255, 128);
            button28.BackColor = corPaint;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(50, 205, 50);
            button28.BackColor = corPaint;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(173, 255, 47);
            button28.BackColor = corPaint;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(128, 255, 255);
            button28.BackColor = corPaint;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(32, 178, 170);
            button28.BackColor = corPaint;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(0, 0, 192);
            button28.BackColor = corPaint;
        }


        private void button18_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(128, 128, 255);
            button28.BackColor = corPaint;
        }
        private void button19_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(128, 0, 128);
            button28.BackColor = corPaint;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            corPaint = Color.FromArgb(191, 205, 219);
            button28.BackColor = corPaint;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            forma = 3;
            button21.Enabled = false;
            button22.Enabled = true;
            button23.Enabled = true;
            button24.Enabled = true;
            button25.Enabled = true;
            button26.Enabled = true;
            button27.Enabled = true;
            button31.Enabled = true;
            textBox1.Enabled = true;
            button31.Text = "Digite o raio";
            button32.Text = "Digite";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            forma = 4;
            button21.Enabled = true;
            button22.Enabled = false;
            button23.Enabled = true;
            button24.Enabled = true;
            button25.Enabled = true;
            button26.Enabled = true;
            button27.Enabled = true;
            button31.Enabled = true;
            textBox1.Enabled = true;
            button32.Enabled = true;
            textBox2.Enabled = true;
            button31.Text = "Digite a altura";
            button32.Text = "Digite a largura";

        }

        private void button23_Click(object sender, EventArgs e)
        {
            forma = 6;
            button21.Enabled = true;
            button22.Enabled = true;
            button23.Enabled = false;
            button24.Enabled = true;
            button25.Enabled = true;
            button26.Enabled = true;
            button27.Enabled = true;
            button31.Enabled = false;
            textBox1.Enabled = false;
            button32.Enabled = false;
            textBox2.Enabled = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            forma = 2;
            button21.Enabled = true;
            button22.Enabled = true;
            button23.Enabled = true;
            button24.Enabled = false;
            button25.Enabled = true;
            button26.Enabled = true;
            button27.Enabled = true;
            button31.Enabled = false;
            textBox1.Enabled = false;
            button32.Enabled = false;
            textBox2.Enabled = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            forma = 5;
            button21.Enabled = true;
            button22.Enabled = true;
            button23.Enabled = true;
            button24.Enabled = true;
            button25.Enabled = false;
            button26.Enabled = true;
            button27.Enabled = true;
            button31.Enabled = true;
            textBox1.Enabled = true;
            button32.Enabled = true;
            textBox2.Enabled = true;
            button31.Text = "Digite raio 1";
            button32.Text = "Digite raio 2";

        }

        private void button26_Click(object sender, EventArgs e)
        {
            forma = 1;
            button21.Enabled = true;
            button22.Enabled = true;
            button23.Enabled = true;
            button24.Enabled = true;
            button25.Enabled = true;
            button26.Enabled = false;
            button27.Enabled = true;
            button31.Enabled = false;
            textBox1.Enabled = false;
            button32.Enabled = false;
            textBox2.Enabled = false;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            forma = 7;
            button21.Enabled = true;
            button22.Enabled = true;
            button23.Enabled = true;
            button24.Enabled = true;
            button25.Enabled = true;
            button26.Enabled = true;
            button27.Enabled = false;
            button31.Enabled = false;
            textBox1.Enabled = false;
            button32.Enabled = false;
            textBox2.Enabled = false;

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {

        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > 0)
            {
                button31.Enabled = false;
                textBox1.Enabled = false;
                a = int.Parse(textBox1.Text);
                s = true;
                f = true;
            }
            else
            {
                MessageBox.Show("DIGITE VALOR CORRETO E ENVIE");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFigures();

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadFigures();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (f == true && int.Parse(textBox2.Text) > 0)
            {
                button31.Enabled = false;
                textBox1.Enabled = false;
                button32.Enabled = false;
                textBox2.Enabled = false;
                b = int.Parse(textBox2.Text);
                s = true;
            }
            else
            {
                MessageBox.Show("DIGITE VALORES CORRETOS E ENVIE");
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (forma == 1) // reta
            {
                x[click] = e.X;
                y[click] = e.Y;
                if (click == 1) Invalidate();
            }
            else if (forma == 2) // losango
            {
                x[click] = e.X;
                y[click] = e.Y;
                if (click == 3) Invalidate();
            }
            else if (forma == 3 || forma == 5 || forma == 4) // arco e elipse e retangulo
            {
                x[click] = e.X;
                y[click] = e.Y;
                if (click == 0)
                {
                    Invalidate();
                    s = true; // Permitir desenhar no próximo Paint
                }
            }
            else if (forma == 6) // pentágono
            {
                x[click] = e.X;
                y[click] = e.Y;
                if (click == 4) Invalidate();
            }
            else if (forma == 7) // triângulo
            {
                x[click] = e.X;
                y[click] = e.Y;
                if (click == 2) Invalidate();
            }
            click++;

        }

        private void SaveFigures()
        {
            // Cria um SaveFileDialog para permitir que o usuário escolha onde salvar o arquivo
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Define os filtros para o diálogo de salvamento (apenas arquivos de texto e todos os arquivos)
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                // Define a extensão padrão do arquivo como .txt
                saveFileDialog.DefaultExt = "txt";
                // Adiciona automaticamente a extensão se o usuário não a fornecer
                saveFileDialog.AddExtension = true;
                // Define o diretório inicial do diálogo de salvamento
                saveFileDialog.InitialDirectory = @"C:\Arquivos";

                // Verifica se o usuário clicou em OK no diálogo de salvamento
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtém o caminho do arquivo selecionado pelo usuário
                    string filePath = saveFileDialog.FileName;
                    // Cria um StreamWriter para escrever no arquivo especificado
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Escreve os dados da configuração da figura (tipo, índice selecionado, cor e espessura)
                        writer.WriteLine($"{forma},{selectedIndex},{corPaint.ToArgb()},{espessura}");
                        // Escreve as coordenadas da figura
                        for (int i = 0; i < x.Length; i++)
                        {
                            writer.WriteLine($"{x[i]},{y[i]}");
                        }
                        // Verifica se a figura é um círculo, elipse ou triângulo (forma 3, 4 ou 5)
                        if (forma == 3 || forma == 5 || forma == 4)
                        {
                            // Escreve valores adicionais necessários para essas figuras (por exemplo, raios)
                            writer.WriteLine($"{a},{b}");
                        }
                    }
                }
            }
        }

        private void LoadFigures()
        {
            // Cria um OpenFileDialog para permitir que o usuário escolha qual arquivo abrir
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Define os filtros para o diálogo de abertura (apenas arquivos de texto e todos os arquivos)
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                // Define o diretório inicial do diálogo de abertura
                openFileDialog.InitialDirectory = @"C:\Arquivos";

                // Verifica se o usuário clicou em OK no diálogo de abertura
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtém o caminho do arquivo selecionado pelo usuário
                    string filePath = openFileDialog.FileName;
                    // Verifica se o arquivo existe
                    if (File.Exists(filePath))
                    {
                        // Cria um StreamReader para ler o arquivo especificado
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            // Lê a primeira linha do arquivo e divide em partes
                            string[] config = reader.ReadLine().Split(',');
                            // Define os valores de configuração da figura (tipo, índice selecionado, cor e espessura)
                            forma = int.Parse(config[0]);
                            selectedIndex = int.Parse(config[1]);
                            corPaint = Color.FromArgb(int.Parse(config[2]));
                            espessura = int.Parse(config[3]);
                            // Lê e define as coordenadas da figura
                            for (int i = 0; i < x.Length; i++)
                            {
                                string[] coordinates = reader.ReadLine().Split(',');
                                x[i] = int.Parse(coordinates[0]);
                                y[i] = int.Parse(coordinates[1]);
                            }

                            // Verifica se a figura é um círculo, elipse ou triângulo (forma 3, 4 ou 5)
                            if (forma == 3 || forma == 5 || forma == 4)
                            {
                                // Lê e define valores adicionais necessários para essas figuras (por exemplo, raios)
                                string[] radii = reader.ReadLine().Split(',');
                                a = int.Parse(radii[0]);
                                b = int.Parse(radii[1]);
                                s = true; // Indica que os valores adicionais foram definidos
                            }
                            arquivo = true; // Indica que um arquivo foi carregado
                            Invalidate(); // Redesenha a área gráfica para mostrar as figuras carregadas
                        }
                    }
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}