
using CultureChanger.Classes;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace CultureChanger.Screens.ViewModels
{
    class CultureItemVM : ViewModel
    {
        private CultureObject _item;
        private bool _is_selected;
        private CultureChangerProperties _cultureChangerProperties;
        private MBBindingList<CultureItemVM> _cultureItemList;


        public CultureItemVM(CultureObject value, ref MBBindingList<CultureItemVM> cultureItemList, ref CultureChangerProperties cultureChangerProperties)
        {
            this._item = value;
            this._is_selected = false;
            this._cultureChangerProperties = cultureChangerProperties;
            this._cultureItemList = cultureItemList;
        }

        [DataSourceProperty]
        public string ShortenedNameText
        {
            get
            {
                return this._item.Name.ToString();
            }
        }

        [DataSourceProperty]
        public string CultureID
        {
            get
            {
                return this._item.StringId;
            }
        }

        [DataSourceProperty]
        public bool IsSelected
        {
            get
            {
                return this._is_selected;
            }
            set
            {
                if (value == this._is_selected)
                    return;
                this._is_selected = value;
                this.OnPropertyChanged(nameof(IsSelected));
            }
        }

        private void ExecuteSelectCulture()
        {
            foreach (CultureItemVM element in this._cultureItemList)
            {
                element.IsSelected = false;
            }
            this._cultureChangerProperties.CurrentCulture = this._item;
            this.IsSelected = true;
        }
    }
}
