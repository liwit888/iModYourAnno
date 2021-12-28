﻿using Imya.Models;
using Imya.Utils;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Imya.UI.Components
{
    /// <summary>
    /// Interaktionslogik für ModList.xaml
    /// </summary>
    public partial class ModList : UserControl, INotifyPropertyChanged
    {
        public LocalizedText ActivateText { get; } = TextManager.Instance.GetText("MODLIST_ACTIVATE");
        public LocalizedText DeactivateText { get; } = TextManager.Instance.GetText("MODLIST_DEACTIVATE");

        public Mod? CurrentlyDisplayedMod { get; private set; } = null;

        public ModDirectoryManager ModManager { get; private set; } = ModDirectoryManager.Instance;

        public TextManager TextManager { get; } = TextManager.Instance;

        public bool ShowActivateButton {
            get { return _showActivateButton; }
            private set
            {
                _showActivateButton = value;
                OnPropertyChanged("ShowActivateButton");
            }
        }
        private bool _showActivateButton;

        public bool ShowDeactivateButton
        {
            get { return _showDeactivateButton; }
            private set
            {
                _showDeactivateButton = value;
                OnPropertyChanged("ShowDeactivateButton");
            }
        }
        private bool _showDeactivateButton;

        public ModList()
        {
            InitializeComponent();
            DataContext = this;
            OnSelectionChanged();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            var selectedItems = ListBox_ModList.SelectedItems.Cast<Mod>().ToList();
            ShowActivateButton = selectedItems.Any(x => !x.Active);
            ShowDeactivateButton = selectedItems.Any(x => x.Active);

            CurrentlyDisplayedMod = ListBox_ModList.SelectedItems.Count > 0 ? ListBox_ModList.SelectedItems[ListBox_ModList.SelectedItems.Count -1] as Mod : ListBox_ModList.SelectedItem as Mod;
            ModList_SelectionChanged(CurrentlyDisplayedMod);
        }

        private void ActivateButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Mod m in ListBox_ModList.SelectedItems)
            {
                ModManager.Activate(m);
            }
            OnSelectionChanged(); 
        }

        private void DeactivateButton_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Mod m in ListBox_ModList.SelectedItems)
            {
                ModManager.Deactivate(m);
            }
            OnSelectionChanged();
        }

        private void OnSearchRequest(object sender, TextChangedEventArgs e)
        {
            string filterText = SearchTextBox.Text;
            ModDirectoryManager.Instance.FilterMods(x => FilterByKeywords(x, filterText));
        }

        /// <summary>
        /// Determines whether the category or name of a mod contain the current filter text.
        /// </summary>
        /// <param name="m">Mod to check</param>
        /// <param name="filterText">The boolean result</param>
        /// <returns></returns>
        private bool FilterByNameOrCategory(Mod m, String filterText)
        {
            return m.Name.Text.ToLower().Contains(filterText.ToLower()) || m.Category.Text.ToLower().Contains(filterText.ToLower());
        }

        /// <summary>
        /// Determines whether a mod name+category contains all keywords provided in a search.
        /// </summary>
        /// <param name="m">Mod to check</param>
        /// <param name="filterText">The boolean result</param>
        private bool FilterByKeywords(Mod m, String filterText)
        {
            String[] keywords = filterText.Split(" ");
            bool IsMatch = true;
            foreach (String s in keywords)
            {
                IsMatch = IsMatch ? FilterByNameOrCategory(m, s) : false;
            }
            return IsMatch;
        }

        public event ModListSelectionChangedHandler ModList_SelectionChanged = delegate { };
        public delegate void ModListSelectionChangedHandler(Mod mod);

        #region INotifyPropertyChangedMembers

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler is PropertyChangedEventHandler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }    
}
