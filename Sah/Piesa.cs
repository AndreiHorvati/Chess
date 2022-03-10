using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sah
{
    public class Piesa
    {
        private Point _locatie;
        public PictureBox Imagine;
        public TipPiese _tip { get; set; }
        public Echipa _echipa { get; set; }
        public bool Folosibila { get; set; }
        private Form1 _form;
        private Point _index;
        private Piesa _piesaVeche;
        public bool AtacatDeAlb { get; set; }
        public bool AtacatDeNegru { get; set; }

        public static int[] TraseuCalX = { -2, -2, -1, 1, 2, 2, 1, -1 };
        public static int[] TraseuCalY = { -1, 1, 2, 2, 1, -1, -2, -2 };

        private static Echipa _echipaCurenta = Echipa.Negru;
        private static Piesa _piesaCurenta;

        public Piesa(Point locatie, TipPiese tip, Echipa echipa, Point index, Form1 form)
        {
            this._form = form;
            this._index = index;
            this.Imagine = new PictureBox();
            this._locatie = locatie;
            this._tip = tip;
            this._echipa = echipa;
            this.Imagine.Size = new Size(100, 100);
            this.Imagine.Location = _locatie;
            this.Imagine.BackColor = Color.Transparent;
            this.AtacatDeAlb = this.AtacatDeNegru = false;

            this.Imagine.Click += new EventHandler(Piesa_Click);

            if (echipa != Echipa.Nimic && tip != TipPiese.Nimic) 
            {
                if (this._echipa == Echipa.Alb)
                {
                    switch (tip)
                    {
                        case TipPiese.Tura:
                            this.Imagine.BackgroundImage = new Bitmap("tura_alb.png");
                            break;
                        case TipPiese.Nebun:
                            this.Imagine.BackgroundImage = new Bitmap("nebun_alb.png");
                            break;
                        case TipPiese.Cal:
                            this.Imagine.BackgroundImage = new Bitmap("cal_alb.png");
                            break;
                        case TipPiese.Rege:
                            this.Imagine.BackgroundImage = new Bitmap("rege_alb.png");
                            break;
                        case TipPiese.Regina:
                            this.Imagine.BackgroundImage = new Bitmap("regina_alb.png");
                            break;
                        case TipPiese.Pion:
                            this.Imagine.BackgroundImage = new Bitmap("pion_alb.png");
                            break;
                    }
                }
                else if (this._echipa == Echipa.Negru)
                {
                    switch (tip)
                    {
                        case TipPiese.Tura:
                            this.Imagine.BackgroundImage = new Bitmap("tura_negru.png");
                            break;
                        case TipPiese.Nebun:
                            this.Imagine.BackgroundImage = new Bitmap("nebun_negru.png");
                            break;
                        case TipPiese.Cal:
                            this.Imagine.BackgroundImage = new Bitmap("cal_negru.png");
                            break;
                        case TipPiese.Rege:
                            this.Imagine.BackgroundImage = new Bitmap("rege_negru.png");
                            break;
                        case TipPiese.Regina:
                            this.Imagine.BackgroundImage = new Bitmap("regina_negru.png");
                            break;
                        case TipPiese.Pion:
                            this.Imagine.BackgroundImage = new Bitmap("pion_negru.png");
                            break;
                    }
                }
            }
        }

        private bool ExistaAlbastre()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++) 
                {
                    if (this._form.Tabla[i, j].Imagine.BackColor == Color.LightBlue)
                        return true;
                }
            }
            return false;
        }

        private bool Verificare(int x,int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
                return false;
            return true;
        }

        protected void Piesa_Click(object sender, EventArgs e)
        {
            if (_piesaCurenta == this)
            {
                StergeAlbastre();
                if (this._echipa == Echipa.Alb)
                    _echipaCurenta = Echipa.Negru;
                else
                    _echipaCurenta = Echipa.Alb;
                _piesaCurenta = null;
            }
            else
            {
                if (this._echipa == Echipa.Alb)
                {
                    if (this.Imagine.BackColor != Color.LightBlue && this._echipa != _echipaCurenta && !ExistaAlbastre()) 
                    {
                        switch (this._tip)
                        {
                            case TipPiese.Pion:
                                if (Verificare(this._index.X - 1, this._index.Y) && this._form.Tabla[this._index.X - 1, this._index.Y]._echipa == Echipa.Nimic)   
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X - 1, this._index.Y - 1) && this._form.Tabla[this._index.X - 1, this._index.Y - 1]._echipa == Echipa.Negru && this._form.Tabla[this._index.X - 1, this._index.Y - 1]._tip != TipPiese.Rege)  
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X - 1, this._index.Y + 1) && this._form.Tabla[this._index.X - 1, this._index.Y + 1]._echipa == Echipa.Negru && this._form.Tabla[this._index.X - 1, this._index.Y + 1]._tip != TipPiese.Rege) 
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                break;
                            case TipPiese.Rege:
                                if (Verificare(this._index.X - 1, this._index.Y) && this._form.Tabla[this._index.X - 1, this._index.Y]._echipa != this._echipa && this._form.Tabla[this._index.X - 1, this._index.Y].AtacatDeNegru == false) 
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y) && this._form.Tabla[this._index.X + 1, this._index.Y]._echipa != this._echipa && this._form.Tabla[this._index.X + 1, this._index.Y].AtacatDeNegru == false) 
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X, this._index.Y - 1) && this._form.Tabla[this._index.X, this._index.Y - 1]._echipa != this._echipa && this._form.Tabla[this._index.X, this._index.Y - 1].AtacatDeNegru == false)
                                {
                                    this._form.Tabla[this._index.X, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X, this._index.Y + 1) && this._form.Tabla[this._index.X, this._index.Y + 1]._echipa != this._echipa && this._form.Tabla[this._index.X, this._index.Y + 1].AtacatDeNegru == false) 
                                {
                                    this._form.Tabla[this._index.X, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X - 1, this._index.Y - 1) && this._form.Tabla[this._index.X - 1, this._index.Y - 1]._echipa != this._echipa && this._form.Tabla[this._index.X - 1, this._index.Y - 1].AtacatDeNegru == false) 
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X - 1, this._index.Y + 1) && this._form.Tabla[this._index.X - 1, this._index.Y + 1]._echipa != this._echipa && this._form.Tabla[this._index.X - 1, this._index.Y + 1].AtacatDeNegru == false)
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y + 1) && this._form.Tabla[this._index.X + 1, this._index.Y + 1]._echipa != this._echipa && this._form.Tabla[this._index.X + 1, this._index.Y + 1].AtacatDeNegru == false)
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y - 1) && this._form.Tabla[this._index.X + 1, this._index.Y - 1]._echipa != this._echipa && this._form.Tabla[this._index.X + 1, this._index.Y - 1].AtacatDeNegru == false) 
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Alb;
                                    _piesaCurenta = this;
                                }
                                break;
                            case TipPiese.Cal:
                                for (int i = 0; i < 8; i++)
                                    if (Verificare(this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]) && this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]]._echipa != this._echipa && this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                    }
                                break;
                            case TipPiese.Tura:
                                for (int i = this._index.X - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.X + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                            case TipPiese.Nebun:
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y - i]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                            case TipPiese.Regina:
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.X - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.X + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege) 
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Alb;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Negru)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                        }
                    }
                }
                else if (this._echipa == Echipa.Negru)
                {
                    if (this.Imagine.BackColor != Color.LightBlue && this._echipa != _echipaCurenta && !ExistaAlbastre()) 
                    {
                        switch (this._tip)
                        {
                            case TipPiese.Pion:
                                if (Verificare(this._index.X + 1, this._index.Y) && this._form.Tabla[this._index.X + 1, this._index.Y]._echipa == Echipa.Nimic)   
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y - 1) && this._form.Tabla[this._index.X + 1, this._index.Y - 1]._echipa == Echipa.Alb && this._form.Tabla[this._index.X + 1, this._index.Y - 1]._tip != TipPiese.Rege)
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y + 1) && this._form.Tabla[this._index.X + 1, this._index.Y + 1]._echipa == Echipa.Alb && this._form.Tabla[this._index.X + 1, this._index.Y + 1]._tip != TipPiese.Rege)
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                break;
                            case TipPiese.Rege:
                                if (Verificare(this._index.X - 1, this._index.Y) && this._form.Tabla[this._index.X - 1, this._index.Y]._echipa != this._echipa && this._form.Tabla[this._index.X - 1, this._index.Y].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y) && this._form.Tabla[this._index.X + 1, this._index.Y]._echipa != this._echipa && this._form.Tabla[this._index.X + 1, this._index.Y].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X, this._index.Y - 1) && this._form.Tabla[this._index.X, this._index.Y - 1]._echipa != this._echipa && this._form.Tabla[this._index.X, this._index.Y - 1].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X, this._index.Y + 1) && this._form.Tabla[this._index.X, this._index.Y + 1]._echipa != this._echipa && this._form.Tabla[this._index.X, this._index.Y + 1].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X - 1, this._index.Y - 1) && this._form.Tabla[this._index.X - 1, this._index.Y - 1]._echipa != this._echipa && this._form.Tabla[this._index.X - 1, this._index.Y - 1].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X - 1, this._index.Y + 1) && this._form.Tabla[this._index.X - 1, this._index.Y + 1]._echipa != this._echipa && this._form.Tabla[this._index.X - 1, this._index.Y + 1].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X - 1, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X - 1, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y + 1) && this._form.Tabla[this._index.X + 1, this._index.Y + 1]._echipa != this._echipa && this._form.Tabla[this._index.X + 1, this._index.Y + 1].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y + 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y + 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                if (Verificare(this._index.X + 1, this._index.Y - 1) && this._form.Tabla[this._index.X + 1, this._index.Y - 1]._echipa != this._echipa && this._form.Tabla[this._index.X + 1, this._index.Y - 1].AtacatDeAlb == false)
                                {
                                    this._form.Tabla[this._index.X + 1, this._index.Y - 1].Imagine.BackColor = Color.LightBlue;
                                    this._form.Tabla[this._index.X + 1, this._index.Y - 1]._piesaVeche = this;
                                    _echipaCurenta = Echipa.Negru;
                                    _piesaCurenta = this;
                                }
                                break;
                            case TipPiese.Cal:
                                for (int i = 0; i < 8; i++)
                                    if (Verificare(this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]) && this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]]._echipa != this._echipa && this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + TraseuCalX[i], this._index.Y + TraseuCalY[i]]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                    }
                                break;
                            case TipPiese.Tura:
                                for (int i = this._index.X - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.X + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                            case TipPiese.Nebun:
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                break;
                            case TipPiese.Regina:
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y - i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y + i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X - i, this._index.Y + i); i++)
                                {
                                    if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa != this._echipa && this._form.Tabla[this._index.X - i, this._index.Y + i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X - i, this._index.Y + i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X - i, this._index.Y + i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X - i, this._index.Y + i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = 1; Verificare(this._index.X + i, this._index.Y - i); i++)
                                {
                                    if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa != this._echipa && this._form.Tabla[this._index.X + i, this._index.Y - i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X + i, this._index.Y - i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X + i, this._index.Y - i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X + i, this._index.Y - i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.X - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.X + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[i, this._index.Y]._echipa != this._echipa && this._form.Tabla[i, this._index.Y]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[i, this._index.Y].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[i, this._index.Y]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[i, this._index.Y]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y - 1; i >= 0; i--)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Alb)
                                            break;
                                    }
                                    else
                                        break;
                                }
                                for (int i = this._index.Y + 1; i <= 7; i++)
                                {
                                    if (this._form.Tabla[this._index.X, i]._echipa != this._echipa && this._form.Tabla[this._index.X, i]._tip != TipPiese.Rege)
                                    {
                                        this._form.Tabla[this._index.X, i].Imagine.BackColor = Color.LightBlue;
                                        this._form.Tabla[this._index.X, i]._piesaVeche = this;
                                        _echipaCurenta = Echipa.Negru;
                                        _piesaCurenta = this;
                                        if (this._form.Tabla[this._index.X, i]._echipa == Echipa.Alb)
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
            if (this.Imagine.BackColor == Color.LightBlue) 
            {
                if (this._piesaVeche._echipa == Echipa.Negru && this._echipa == Echipa.Alb)
                {
                    switch (this._tip)
                    {
                        case TipPiese.Pion:
                            this._form._pionAlb--;
                            this._form.nrPionAlb.Text = Convert.ToString(this._form._pionAlb);
                            break;
                        case TipPiese.Tura:
                            this._form._turaAlb--;
                            this._form.nrTuraAlb.Text = Convert.ToString(this._form._turaAlb);
                            break;
                        case TipPiese.Cal:
                            this._form._calAlb--;
                            this._form.nrCalAlb.Text = Convert.ToString(this._form._calAlb);
                            break;
                        case TipPiese.Nebun:
                            this._form._nebunAlb--;
                            this._form.nrNebunAlb.Text = Convert.ToString(this._form._nebunAlb);
                            break;
                        case TipPiese.Regina:
                            this._form._reginaAlb--;
                            this._form.nrReginaAlb.Text = Convert.ToString(this._form._reginaAlb);
                            break;
                    }
                }
                else if (this._piesaVeche._echipa == Echipa.Alb && this._echipa == Echipa.Negru) 
                {
                    switch (this._tip)
                    {
                        case TipPiese.Pion:
                            this._form._pionNegru--;
                            this._form.nrPionNegru.Text = Convert.ToString(this._form._pionNegru);
                            break;
                        case TipPiese.Tura:
                            this._form._turaNegru--;
                            this._form.nrTuraNegru.Text = Convert.ToString(this._form._turaNegru);
                            break;
                        case TipPiese.Cal:
                            this._form._calNegru--;
                            this._form.nrCalNegru.Text = Convert.ToString(this._form._calNegru);
                            break;
                        case TipPiese.Nebun:
                            this._form._nebunNegru--;
                            this._form.nrNebunNegru.Text = Convert.ToString(this._form._nebunNegru);
                            break;
                        case TipPiese.Regina:
                            this._form._reginaNegru--;
                            this._form.nrReginaNegru.Text = Convert.ToString(this._form._reginaNegru);
                            break;
                    }
                }

                this.Imagine.BackColor = Color.Transparent;
                this.Imagine.BackgroundImage = this._piesaVeche.Imagine.BackgroundImage;
                this._piesaVeche.Imagine.BackgroundImage = null;
                this._tip = this._piesaVeche._tip;
                this._echipa = this._piesaVeche._echipa;
                this._piesaVeche._echipa = Echipa.Nimic;
                this._piesaVeche._tip = TipPiese.Nimic;
                StergeAlbastre();
                this._form.CreazaAtacurile();

                if (this._form.CastigaAlb())
                    MessageBox.Show("Echip alb a castigat!");
                else if (this._form.CastigaNegru())
                    MessageBox.Show("Echipa negru a castigat!");
            }
        }

        private void StergeAlbastre()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (this._form.Tabla[i, j].Imagine.BackColor == Color.LightBlue)
                        this._form.Tabla[i, j].Imagine.BackColor = Color.Transparent;
        }
    }
}
