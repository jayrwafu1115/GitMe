﻿using PanCardView.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImageViewer.ViewModels
{
    public sealed class CardsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _currentIndex;
        private int _imageCount = 1078;

        public CardsViewModel()
        {
            Items = new ObservableCollection<object>
            {
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Red },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Green },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Gold },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Silver },
                new { Source = CreateSource(), Ind = _imageCount++, Color = Color.Blue }
            };

            PanPositionChangedCommand = new Command(v =>
            {
                if (IsAutoAnimationRunning || IsUserInteractionRunning)
                {
                    return;
                }

                var index = CurrentIndex + (bool.Parse(v.ToString()) ? 1 : -1);
                if (index < 0 || index >= Items.Count)
                {
                    return;
                }
                CurrentIndex = index;
            });

            RemoveCurrentItemCommand = new Command(() =>
            {
                if (!Items.Any())
                {
                    return;
                }
                Items.RemoveAt(CurrentIndex.ToCyclingIndex(Items.Count));
            });

            GoToLastCommand = new Command(() =>
            {
                CurrentIndex = Items.Count - 1;
            });
        }

        public ICommand PanPositionChangedCommand { get; }

        public ICommand RemoveCurrentItemCommand { get; }

        public ICommand GoToLastCommand { get; }

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentIndex)));
            }
        }

        public bool IsAutoAnimationRunning { get; set; }

        public bool IsUserInteractionRunning { get; set; }

        public ObservableCollection<object> Items { get; }

        private string CreateSource()
        {
            var source = $"https://picsum.photos/500/500?image={_imageCount}";
            return source;
        }
    }
}
