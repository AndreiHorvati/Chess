using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sah
{
    public enum TipPiese
    {
        Pion, Tura, Cal, Nebun, Rege, Regina, Nimic
    }

    public enum Echipa
    {
        Alb, Negru, Nimic
    }

    public partial class Form1 : Form
    {
        public Piesa[,] Tabla = new Piesa[8, 8];
        public int _pionAlb = 8, _turaAlb = 2, _calAlb = 2, _nebunAlb = 2, _reginaAlb = 1;
        public int _pionNegru = 8, _turaNegru = 2, _calNegru = 2, _nebunNegru = 2, _reginaNegru = 1;

        public Form1()
        {
            InitializeComponent();
            CreareButonIesire();
            CreareTabla();
            CreazaAtacurile();
        }

        private TipPiese SetarePiesa(int x, int y)
        {
            if (x == 1 || x == 6)
                return TipPiese.Pion;
            else if (x == 0 || x == 7)
            {
                if (y == 0 || y == 7)
                    return TipPiese.Tura;
                else if (y == 1 || y == 6)
                    return TipPiese.Cal;
                else if (y == 2 || y == 5)
                    return TipPiese.Nebun;
                else if (y == 3)
                    return TipPiese.Regina;
                else
                    return TipPiese.Rege;
            }
            else
                return TipPiese.Nimic;
        }

        private Echipa SetareEchipa(int x)
        {
            if (x < 2)
                return Echipa.Negru;
            else if (x > 5)
                return Echipa.Alb;
            else
                return Echipa.Nimic;
        }

        private void CreareTabla()
        {
            int x, y;
            x = 560;
            y = 120;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Tabla[i, j] = new Piesa(new Point(x, y), SetarePiesa(i, j), SetareEchipa(i), new Point(i, j), this);
                    this.Controls.Add(Tabla[i, j].Imagine);
                    x += 100;
                }
                x = 560;
                y += 100;
            }
        }

        private void CreareButonIesire()
        {
            Button btnIesire = new Button();
            btnIesire.Size = new Size(50, 50);
            btnIesire.BackColor = Color.Coral;
            btnIesire.Text = "X";
            btnIesire.Font = new Font(btnIesire.Font, FontStyle.Bold);
            btnIesire.Location = new Point(1870, 0);
            btnIesire.Click += new EventHandler(btnIesire_Click);
            this.Controls.Add(btnIesire);
        }

        protected void btnIesire_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DesenareTabla(PaintEventArgs e)
        {
            int x, y;
            x = 560;
            y = 120;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    if (i % 2 != 0)
                    {
                        if (j % 2 != 0)
                            e.Graphics.FillRectangle(Brushes.Azure, x, y, 100, 100);
                        else
                            e.Graphics.FillRectangle(Brushes.BurlyWood, x, y, 100, 100);
                    }
                    else
                    {
                        if (j % 2 == 0)
                            e.Graphics.FillRectangle(Brushes.Azure, x, y, 100, 100);
                        else
                            e.Graphics.FillRectangle(Brushes.BurlyWood, x, y, 100, 100);
                    }
                    y += 100;
                }
                x += 100;
                y = 120;
            }
        }

        private bool Verificare(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
                return false;
            return true;
        }

        public void CreazaAtacurile()
        {
            StergeAtacurile();

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Point pozitieCurenta = new Point(i, j);

                    if (this.Tabla[i, j]._echipa == Echipa.Alb)
                    {
                        switch(this.Tabla[i,j]._tip)
                        {
                            case TipPiese.Pion:
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y - 1)) 
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y - 1].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y + 1)) 
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y + 1].AtacatDeAlb = true;
                                break;
                            case TipPiese.Rege:
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y)) 
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X, pozitieCurenta.Y - 1].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X, pozitieCurenta.Y + 1].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y - 1].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y + 1].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y + 1].AtacatDeAlb = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y - 1].AtacatDeAlb = true;
                                break;
                            case TipPiese.Tura:
                                for (int k = pozitieCurenta.X - 1; k >= 0; k--)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeAlb = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.X + 1; k < 8; k++)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeAlb = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y + 1; k < 8; k++)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeAlb = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Alb) 
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y - 1; k >= 0; k--) 
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeAlb = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                            case TipPiese.Cal:
                                for (int k = 0; k < 8; k++)
                                {
                                    if (Verificare(pozitieCurenta.X + Piesa.TraseuCalX[k], pozitieCurenta.Y + Piesa.TraseuCalY[k]))
                                        this.Tabla[pozitieCurenta.X + Piesa.TraseuCalX[k], pozitieCurenta.Y + Piesa.TraseuCalY[k]].AtacatDeAlb = true;
                                }
                                break;
                            case TipPiese.Nebun:
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k]._echipa == Echipa.Alb)
                                        break;
                                }
                                break;
                            case TipPiese.Regina:
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k].AtacatDeAlb = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k]._echipa == Echipa.Alb)
                                        break;
                                }
                                for (int k = pozitieCurenta.X - 1; k >= 0; k--)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeAlb = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.X + 1; k < 8; k++)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeAlb = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y + 1; k < 8; k++)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeAlb = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y - 1; k >= 0; k--)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeAlb = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                        }
                    }
                    else if(this.Tabla[i,j]._echipa == Echipa.Negru)
                    {
                        switch (this.Tabla[i, j]._tip)
                        {
                            case TipPiese.Pion:
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y - 1].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y + 1].AtacatDeNegru = true;
                                break;
                            case TipPiese.Rege:
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y))
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X, pozitieCurenta.Y - 1].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X, pozitieCurenta.Y + 1].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y - 1].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X - 1, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X - 1, pozitieCurenta.Y + 1].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y + 1))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y + 1].AtacatDeNegru = true;
                                if (Verificare(pozitieCurenta.X + 1, pozitieCurenta.Y - 1))
                                    this.Tabla[pozitieCurenta.X + 1, pozitieCurenta.Y - 1].AtacatDeNegru = true;
                                break;
                            case TipPiese.Tura:
                                for (int k = pozitieCurenta.X - 1; k >= 0; k--)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeNegru = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.X + 1; k < 8; k++)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeNegru = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y + 1; k < 8; k++)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeNegru = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y - 1; k >= 0; k--)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeNegru = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                            case TipPiese.Cal:
                                for (int k = 0; k < 8; k++)
                                {
                                    if (Verificare(pozitieCurenta.X + Piesa.TraseuCalX[k], pozitieCurenta.Y + Piesa.TraseuCalY[k]))
                                        this.Tabla[pozitieCurenta.X + Piesa.TraseuCalX[k], pozitieCurenta.Y + Piesa.TraseuCalY[k]].AtacatDeNegru = true;
                                }
                                break;
                            case TipPiese.Nebun:
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k]._echipa == Echipa.Negru)
                                        break;
                                }
                                break;
                            case TipPiese.Regina:
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y - k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X - k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X - k, pozitieCurenta.Y + k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y - k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y - k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = 1; Verificare(pozitieCurenta.X + k, pozitieCurenta.Y + k); k++)
                                {
                                    this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k].AtacatDeNegru = true;
                                    if (this.Tabla[pozitieCurenta.X + k, pozitieCurenta.Y + k]._echipa == Echipa.Negru)
                                        break;
                                }
                                for (int k = pozitieCurenta.X - 1; k >= 0; k--)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeNegru = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.X + 1; k < 8; k++)
                                {
                                    if (Verificare(k, pozitieCurenta.Y))
                                    {
                                        this.Tabla[k, pozitieCurenta.Y].AtacatDeNegru = true;
                                        if (this.Tabla[k, pozitieCurenta.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y + 1; k < 8; k++)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeNegru = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int k = pozitieCurenta.Y - 1; k >= 0; k--)
                                {
                                    if (Verificare(pozitieCurenta.X, k))
                                    {
                                        this.Tabla[pozitieCurenta.X, k].AtacatDeNegru = true;
                                        if (this.Tabla[pozitieCurenta.X, k]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                        }
                    }
                }
        }

        private void StergeAtacurile()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    this.Tabla[i, j].AtacatDeAlb = false;
                    this.Tabla[i, j].AtacatDeNegru = false;
                }
        }

        public bool CastigaNegru()
        {
            Point coordonateRege = new Point();
            bool ok = false;

            int[] dx = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (this.Tabla[i, j]._echipa == Echipa.Alb && this.Tabla[i, j]._tip == TipPiese.Rege)
                    {
                        coordonateRege.X = i;
                        coordonateRege.Y = j;
                        ok = true;
                        break;
                    }
                }
                if (ok)
                    break;
            }
            if (this.Tabla[coordonateRege.X, coordonateRege.Y].AtacatDeNegru == false)
                return false;
            for (int i = 0; i < 8; i++)
                if (Verificare(coordonateRege.X + dx[i], coordonateRege.Y + dy[i]))
                    if(this.Tabla[coordonateRege.X + dx[i], coordonateRege.Y + dy[i]].AtacatDeNegru == false && this.Tabla[coordonateRege.X + dx[i], coordonateRege.Y + dy[i]]._echipa != this.Tabla[coordonateRege.X, coordonateRege.Y]._echipa) 
                        return false;
            return true;
        }

        public bool CastigaAlb()
        {
            Point coordonateRege = new Point();
            bool ok = false;

            int[] dx = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dy = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 };

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (this.Tabla[i, j]._echipa == Echipa.Negru && this.Tabla[i, j]._tip == TipPiese.Rege)
                    {
                        coordonateRege.X = i;
                        coordonateRege.Y = j;
                        ok = true;
                        break;
                    }
                }
                if (ok)
                    break;
            }
            if (this.Tabla[coordonateRege.X, coordonateRege.Y].AtacatDeAlb == false)
                return false;
            for (int i = 0; i < 8; i++)
                if (Verificare(coordonateRege.X + dx[i], coordonateRege.Y + dy[i]) && this.Tabla[coordonateRege.X + dx[i], coordonateRege.Y + dy[i]].AtacatDeAlb == false && this.Tabla[coordonateRege.X + dx[i], coordonateRege.Y + dy[i]]._echipa != this.Tabla[coordonateRege.X, coordonateRege.Y]._echipa) 
                    return false;
            return true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DesenareTabla(e);
        }
    }
}
