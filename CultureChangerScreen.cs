using CultureChanger.Classes;
using CultureChanger.Screens.ViewModels;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;

namespace CultureChanger.Screens
{
    class CultureChangerScreen : ScreenBase
    {
		private CultureChangerProperties _cultureChangerProperties;
		private CultureChangerViewModel _datasource;
		private GauntletLayer _gauntletLayer;
		private GauntletMovie _movie;

		public CultureChangerScreen(ref CultureChangerProperties cultureChangerProperties)
		{
			this._cultureChangerProperties = cultureChangerProperties;
		}
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._datasource = new CultureChangerViewModel(ref this._cultureChangerProperties);
			this._gauntletLayer = new GauntletLayer(100, "GauntletLayer");
			this._gauntletLayer.IsFocusLayer = true;
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			ScreenManager.TrySetFocus(this._gauntletLayer);
			this._movie = this._gauntletLayer.LoadMovie(nameof(CultureChangerScreen), this._datasource);
		}

		protected override void OnActivate()
		{
			base.OnActivate();
			ScreenManager.TrySetFocus(this._gauntletLayer);
		}

		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
		}

		protected override void OnFinalize()
		{
			base.OnFinalize();
			RemoveLayer(_gauntletLayer);
			this._datasource = null;
			this._gauntletLayer = null;
		}
	}
}
