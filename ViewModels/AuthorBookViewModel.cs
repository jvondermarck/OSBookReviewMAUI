﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AppCenter.Crashes;
using OSBookReviewMAUI.Models;
using OSBookReviewMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSBookReviewMAUI.ViewModels
{
    [QueryProperty(nameof(AID), nameof(AID))]
    public partial class AuthorBookViewModel : ObservableObject
    {

        public IDataStore<BookReview> BookDataStore => DependencyService.Get<IDataStore<BookReview>>();

        public AuthorBookViewModel()
        {
            Books = new();
        }

        [ObservableProperty]
        string authorName;

        [ObservableProperty]
        string publisherName;

        [ObservableProperty]
        int aID;

        [ObservableProperty]
        ObservableCollection<BookReview> books;

        [ICommand]
        async Task LoadItemsCommand()
        {
            try
            {
                Books.Clear();
                var sbooks = await BookDataStore.GetListAsync(AID);
                foreach (BookReview b in sbooks)
                {
                    Books.Add(b);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        public async void OnAppearing()
        {
            await LoadItemsCommand();
        }

    }
}
