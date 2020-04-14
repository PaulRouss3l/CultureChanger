﻿using CultureChanger.Classes;
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
			// change culture to the choosen one
			this._cultureChangerProperties.Settlement.Culture = this._cultureChangerProperties.CurrentCulture;
			// change bound settlement as well
			if (this._cultureChangerProperties.Settlement.BoundVillages.Count > 0)
			{

				foreach (Village element in this._cultureChangerProperties.Settlement.BoundVillages)
				{
					Settlement settlement = Settlement.FindAll(delegate(Settlement s) {
						if (s.Name.ToString() == element.Name.ToString())
						{
							return true;
						}
						return false;
					}).First();
					if (settlement == null)
					{
						continue;
					}
					settlement.Culture = this._cultureChangerProperties.Settlement.Culture;
				}
			}
			
			InformationManager.DisplayMessage(new InformationMessage("Culture changed to " + this._cultureChangerProperties.CurrentCultureName));
			ScreenManager.PopScreen();
		}
	}
}