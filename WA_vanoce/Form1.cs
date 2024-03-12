namespace WA_vanoce
{
	public partial class Form1 : Form
	{
		int x;
		int y;
		Bitmap santa; // santa
		Bitmap snowfl; // vlocka
		int playerSpeed = 75; 
		int snowflSpeed = 80;
		int snowflX;
		int snowflY;
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			santa = Properties.Resources.Santa; //prirazenii obrazku santy
			snowfl = Properties.Resources.vlocka; //prirazeni obrazku vlocky
			//vytvoreni souradnic, ktere zajistiumisteni santy na stred spodni hrany okna
			x = (this.ClientSize.Width - (santa.Width / 2)) / 2;
			y = this.ClientSize.Height - (santa.Height / 2);
			//vytvoreni prvni vlocky
			ResetSnow();
		}
		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			//zmenseni obrazku
			int santaWidth = santa.Width / 2;
			int santaHeight = santa.Height / 2;
			int snowflWidth = snowfl.Width / 2;
			int snowflHeight = snowfl.Height / 2;
			//vykresleni santy
			e.Graphics.DrawImage(santa, x, y, santaWidth, santaHeight);
			//vykresleni vlocky
			e.Graphics.DrawImage(snowfl, snowflX, snowflY, snowflWidth, snowflHeight);
		}
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			//pohyb hrace
			switch (e.KeyCode)
			{
				case Keys.Left:
					x = Math.Max(x - playerSpeed, 0); //zajisteni, ze se hrac nevykresli mimo okno na leve strane	
					break;
				case Keys.Right:
					x = Math.Min(x + playerSpeed, this.ClientSize.Width - santa.Width / 2); //zajisteni, ze se hrac nevykresli mimo okno na prave strane	
					break;
			}
			Refresh();
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			//pad vlocky
			snowflY += snowflSpeed;

			//podminka pro kolizi hrace s vlockou
			if (snowflY + snowfl.Height / 2 >= y && snowflY <= y + santa.Height / 2 &&
				snowflX + snowfl.Width / 2 >= x && snowflX <= x + santa.Width / 2)
			{
				GameOver();
			}

			// podminka pro kolizi vlocky se spodni hranou okna
			if (snowflY >= this.ClientSize.Height)
			{
				ResetSnow();
			}
			Refresh();
		}
		private void ResetSnow()
		{
			//vytvoreni souradnic pro vlocku
			Random rnd = new Random();
			snowflX = rnd.Next(0, this.ClientSize.Width - snowfl.Width / 2);
			snowflY = 0;
		}
		private void GameOver()
		{
			timer1.Stop();
			MessageBox.Show("Game Over!");
			//zavreni aplikace
			this.Close();
		}
	}
}
