using CultureChanger.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace CultureChanger.Screens.ViewModels
{
	internal class CultureChangerViewModel : ViewModel
	{
		[DataSourceProperty]
		public MBBindingList<CultureItemVM> Cultures
		{
			get
			{
				return this._cultureItems;
			}
			set
			{
				if (value == this._cultureItems)
					return;
				this._cultureItems = value;
				this.OnPropertyChanged(nameof(Cultures));
			}
		}

		public MBBindingList<CultureItemVM> _cultureItems;

		private CultureChangerProperties _cultureChangerProperties;

		public CultureChangerViewModel(ref CultureChangerProperties cultureChangerProperties)
		{
			this._cultureChangerProperties = cultureChangerProperties;
			this.Cultures = new MBBindingList<CultureItemVM>();

			var cultures = MBObjectManager.Instance.GetObjectTypeList<CultureObject>();
			foreach (CultureObject element in cultures)
			{
				if (element.IsMainCulture)
				{
					Cultures.Add(new CultureItemVM(element, ref this._cultureItems, ref this._cultureChangerProperties));
				}
			}
		}

		private void ExitChangeCultureMenu()
		{
			this._cultureChangerProperties.Settlement.Culture = this._cultureChangerProperties.CurrentCulture;
			InformationManager.DisplayMessage(new InformationMessage("Culture changed to " + this._cultureChangerProperties.CurrentCultureName));
			ScreenManager.PopScreen();
		}
	}
}