using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestAnimationNotWorking
{
	public class ToggleSearchTrigger : TriggerAction<Button>
	{
		protected async override void Invoke(Button sender)
		{
			StackLayout parentStack = sender.Parent.Parent as StackLayout;

			if (parentStack != null)
			{
				StackLayout searchLayout = parentStack.FindByName<StackLayout>("SearchForm");

				if (searchLayout != null)
				{
					bool isHiding = (searchLayout.HeightRequest < 0 ? true : false);

					await PerformAnimation(isHiding, searchLayout, sender);
				}
			}

		}

		public async Task PerformAnimation(bool isHiding, StackLayout searchLayout, Button sender)
		{
			await Task.Delay(250);

			var animation = new Animation(
						 d =>
						 {
							 searchLayout.HeightRequest = (isHiding ? 0 : -1);
							 searchLayout.Padding = (isHiding ? new Thickness(0, 0) : new Thickness(20, 10));

						 }, 1, 0
						);

			animation.Commit(searchLayout, "hideme", 16, 5000, Easing.Linear, finished: (d, v) =>
			{
				if (isHiding)
				{//"AngleUp32.png" : "AngleDown32.png";
					sender.Image = "AngleDown32.png";
				}
				else
				{
					sender.Image = "AngleUp32.png";
				}
			});
		}
	}
}
