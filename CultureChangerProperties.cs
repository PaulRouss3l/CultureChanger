using TaleWorlds.SaveSystem;
using TaleWorlds.CampaignSystem;

namespace CultureChanger.Classes
{
	[SaveableClass(1)]
	class CultureChangerProperties
    {
		[SaveableField(0)] public string settlementID;
		private CultureObject _cultureCultureSelected;

		public CultureChangerProperties(string settlementID)
        {
            this.settlementID = settlementID;
		}

		private Settlement getSelf()
		{
			return Settlement.Find(this.settlementID);
		}

		public Settlement Settlement
		{
			get
			{
				return Settlement.Find(this.settlementID);
			}
		}

		public string CurrentCultureName
		{
			get
			{
				return this._cultureCultureSelected.Name.ToString();
			}
		}

		public CultureObject CurrentCulture
		{
			get
			{
				return this._cultureCultureSelected;
			}
			set
			{
				if (value == this._cultureCultureSelected)
					return;
				this._cultureCultureSelected = value;
			}
		}

	}
}
