using FlagData;
using Xamarin.Forms;
using System;
using Xamarin.Essentials;

namespace FunFlacts
{
    public partial class AllFlags : ContentPage
    {
        private FunFlactsViewModel vm;
        private bool EditModeFlag = false;
        public AllFlags()
        {
            InitializeComponent();
            vm = DependencyService.Get<FunFlactsViewModel>();
            listView.ItemsSource = vm.Flags;
        }

        private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            vm.CurrentFlag = ((Flag)e.SelectedItem);

            if (EditModeFlag)
            {
                vm.Flags.Remove((Flag)e.SelectedItem);
                return;
            }

            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new FlagDetailsPage());
            }
        }

        private void OnEnterEdit(object sender, EventArgs e)
        {
            EditModeFlag = true;

            ToolbarItem toolbarItemEditCancel = new ToolbarItem();
            toolbarItemEditCancel.IconImageSource = "ic_cancel.png";
            toolbarItemEditCancel.Clicked += new EventHandler(this.OnCancelEdit);
            this.ToolbarItems.Add(toolbarItemEditCancel);
            this.ToolbarItems.RemoveAt(0);
        }

        private void OnCancelEdit(object sender, EventArgs e)
        {
            EditModeFlag = false;

            ToolbarItem toolbarItemEditEnter = new ToolbarItem();
            toolbarItemEditEnter.IconImageSource = "ic_edit.png";
            toolbarItemEditEnter.Clicked += new EventHandler(this.OnEnterEdit);
            this.ToolbarItems.Add(toolbarItemEditEnter);
            this.ToolbarItems.RemoveAt(0);
        }
    }
}
