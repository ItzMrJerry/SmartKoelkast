namespace Test;

public partial class Recepten : ContentPage
{
	public Recepten(string filters)
	{
		InitializeComponent();

		Filter.Text = filters;
	}

    private async void Terug(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
		
	}

	private async void Ingredienten(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Ingredienten());
	}
}