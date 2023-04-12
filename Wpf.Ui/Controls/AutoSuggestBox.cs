﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Wpf.Ui.Controls;

// TODO: Fix closing and loosing focus

/// <summary>
/// Represents a text control that makes suggestions to users as they enter text using a keyboard.
/// </summary>
[ToolboxItem(true)]
[ToolboxBitmap(typeof(AutoSuggestBox), "AutoSuggestBox.bmp")]
[TemplatePart(Name = "PART_Popup", Type = typeof(System.Windows.Controls.Primitives.Popup))]
[TemplatePart(Name = "PART_SuggestionsPresenter", Type = typeof(System.Windows.Controls.ListView))]
public class AutoSuggestBox : Wpf.Ui.Controls.TextBox
{
    /// <summary>
    /// The current text in <see cref="System.Windows.Controls.TextBox.Text"/> used for validation purposes.
    /// </summary>
    private string _currentText = String.Empty;

    /// <summary>
    /// Template element represented by the <c>PART_Popup</c> name.
    /// </summary>
    private const string ElementPopup = "PART_Popup";

    /// <summary>
    /// Template element represented by the <c>PART_SuggestionsPresenter</c> name.
    /// </summary>
    private const string ElementSuggestionsPresenter = "PART_SuggestionsPresenter";

    /// <summary>
    /// Popup with suggestions.
    /// </summary>
    protected Popup? Popup { get; private set; }

    /// <summary>
    /// List of suggestions inside <see cref="Popup"/>.
    /// </summary>
    protected ListView? SuggestionsPresenter { get; private set; }

    /// <summary>
    /// Property for <see cref="ItemsSource"/>.
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource),
        typeof(object), typeof(AutoSuggestBox),
        new PropertyMetadata(null!, OnItemsSourceChanged));

    /// <summary>
    /// Property for <see cref="FilteredItemsSource"/>.
    /// </summary>
    public static readonly DependencyProperty FilteredItemsSourceProperty = DependencyProperty.Register(nameof(FilteredItemsSource),
        typeof(object), typeof(AutoSuggestBox),
        new PropertyMetadata(null!));

    /// <summary>
    /// Property for <see cref="IsSuggestionListOpen"/>.
    /// </summary>
    public static readonly DependencyProperty IsSuggestionListOpenProperty = DependencyProperty.Register(nameof(IsSuggestionListOpen),
        typeof(bool), typeof(AutoSuggestBox),
        new PropertyMetadata(false));

    /// <summary>
    /// Property for <see cref="MaxDropDownHeight"/>.
    /// </summary>
    public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register(nameof(MaxDropDownHeight),
        typeof(double), typeof(AutoSuggestBox),
        new PropertyMetadata(240d));

    /// <summary>
    /// Routed event for <see cref="QuerySubmitted"/>.
    /// </summary>
    public static readonly RoutedEvent QuerySubmittedEvent = EventManager.RegisterRoutedEvent(
        nameof(QuerySubmitted), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AutoSuggestBox));

    /// <summary>
    /// Routed event for <see cref="SuggestionChosen"/>.
    /// </summary>
    public static readonly RoutedEvent SuggestionChosenEvent = EventManager.RegisterRoutedEvent(
        nameof(SuggestionChosen), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AutoSuggestBox));

    /// <summary>
    /// <see cref="AutoSuggestBox"/> does no accept returns.
    /// </summary>
    public new bool AcceptsReturn
    {
        get => false;
        set => throw new NotImplementedException($"{typeof(AutoSuggestBox)} does not accept returns.");
    }

    /// <summary>
    /// <see cref="AutoSuggestBox"/> does not accept changes to the number of lines.
    /// </summary>
    public new int MaxLines
    {
        get => 1;
        set => throw new NotImplementedException($"{typeof(AutoSuggestBox)} does not accept changes to the number of lines.");
    }

    /// <summary>
    /// <see cref="AutoSuggestBox"/> does not accept changes to the number of lines.
    /// </summary>
    public new int MinLines
    {
        get => 1;
        set => throw new NotImplementedException($"{typeof(AutoSuggestBox)} does not accept changes to the number of lines.");
    }

    /// <summary>
    /// ItemsSource specifies a collection used to generate the list of suggestions
    /// for <see cref="AutoSuggestBox"/>.
    /// </summary>
    [Bindable(true)]
    public object ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set
        {
            if (value == null)
                ClearValue(ItemsSourceProperty);
            else
                SetValue(ItemsSourceProperty, value);
        }
    }

    /// <summary>
    /// Filtered <see cref="ItemsSource"/> based on provided text.
    /// </summary>
    public object FilteredItemsSource
    {
        get => GetValue(FilteredItemsSourceProperty);
        private set
        {
            if (value == null)
                ClearValue(FilteredItemsSourceProperty);
            else
                SetValue(FilteredItemsSourceProperty, value);
        }
    }

    /// <summary>
    /// Gets or sets a value representing whether the suggestion list should be opened.
    /// </summary>
    public bool IsSuggestionListOpen
    {
        get => (bool)GetValue(IsSuggestionListOpenProperty);
        set => SetValue(IsSuggestionListOpenProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum height of the drop-down list with suggestions.
    /// </summary>
    public double MaxDropDownHeight
    {
        get => (double)GetValue(MaxDropDownHeightProperty);
        set => SetValue(MaxDropDownHeightProperty, value);
    }

    /// <summary>
    /// Event occurs when a user commits a query string.
    /// </summary>
    public event RoutedEventHandler QuerySubmitted
    {
        add => AddHandler(QuerySubmittedEvent, value);
        remove => RemoveHandler(QuerySubmittedEvent, value);
    }

    /// <summary>
    /// Event occurs when the user selects an item from the recommended ones.
    /// </summary>
    public event RoutedEventHandler SuggestionChosen
    {
        add => AddHandler(SuggestionChosenEvent, value);
        remove => RemoveHandler(SuggestionChosenEvent, value);
    }

    /// <summary>
    /// Gets the suggested result that the user chose.
    /// </summary>
    public object? ChosenSuggestion { get; protected set; } = null;

    /// <summary>
    /// Invoked whenever application code or an internal process,
    /// such as a rebuilding layout pass, calls the ApplyTemplate method.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild(ElementPopup) is Popup popup)
            Popup = popup;

        if (GetTemplateChild(ElementSuggestionsPresenter) is ListView listView)
            SuggestionsPresenter = listView;

        if (SuggestionsPresenter == null!)
            return;

        SuggestionsPresenter.SelectionChanged += OnSuggestionsPresenterSelectionChanged;
        SuggestionsPresenter.LostFocus += OnSuggestionsPresenterLostFocus;
    }

    /// <inheritdoc />
    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);

        if (ItemsSource is not ICollection itemsSourceCollection)
            return;

        var newText = Text;

        if (_currentText == newText)
            return;

        _currentText = newText;

        if (String.IsNullOrEmpty(newText))
        {
            FilteredItemsSource = ItemsSource;
        }
        else
        {
            var formattedNewText = newText.ToLower();

            var filteredCollection = new List<object>();

            foreach (var collectionItem in itemsSourceCollection)
                if ((collectionItem?.ToString()?.ToLower() ?? String.Empty).Contains(formattedNewText) && collectionItem != null)
                    filteredCollection.Add(collectionItem);

            FilteredItemsSource = filteredCollection;
        }

        OnQuerySubmitted();

        IsSuggestionListOpen = true;
    }

    /// <inheritdoc />
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Down && Popup != null && SuggestionsPresenter != null && Popup.IsOpen)
        {
            SuggestionsPresenter.Focus();

            e.Handled = true;

            return;
        }

        base.OnKeyDown(e);
    }

    /// <inheritdoc />
    protected override void OnLostFocus(RoutedEventArgs e)
    {
        IsSuggestionListOpen = false;

        base.OnLostFocus(e);
    }

    /// <inheritdoc />
    protected override void OnClearButtonClick()
    {
        base.OnClearButtonClick();

        // TODO: Fix clearing search results
        FilteredItemsSource = ItemsSource;
        ChosenSuggestion = null;
    }

    /// <summary>
    /// This virtual method is called after presenter containing suggestion loses focus.
    /// </summary>
    protected virtual void OnSuggestionsPresenterLostFocus(object sender, RoutedEventArgs e)
    {
        if (!IsFocused)
            IsSuggestionListOpen = false;
    }

    /// <summary>
    /// This virtual method is called after one of the suggestion is selected.
    /// </summary>
    protected virtual void OnSuggestionsPresenterSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ListView listView)
            return;

        var selected = listView.SelectedItem;

        listView.UnselectAll();

        ChosenSuggestion = selected ?? null;
        var chosenSuggestionString = selected?.ToString() ?? String.Empty;

        Text = chosenSuggestionString;
        CaretIndex = chosenSuggestionString.Length;
        IsSuggestionListOpen = false;

        Focus();

        OnSuggestionChosen();
    }

    /// <summary>
    /// This virtual method is called after submitting a query.
    /// </summary>
    protected virtual void OnQuerySubmitted()
    {
        var newEvent = new RoutedEventArgs(QuerySubmittedEvent, this);
        RaiseEvent(newEvent);
    }

    /// <summary>
    /// This virtual method is called after selecting a suggestion.
    /// </summary>
    protected virtual void OnSuggestionChosen()
    {
        var newEvent = new RoutedEventArgs(SuggestionChosenEvent, this);
        RaiseEvent(newEvent);
    }

    /// <summary>
    /// This virtual method is called after <see cref="ItemsSource"/> is changed.
    /// </summary>
    protected virtual void OnItemsSourceChanged(IEnumerable<string> itemsSource)
    {
        FilteredItemsSource = itemsSource;
    }

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not AutoSuggestBox autoSuggestBox)
            return;

        if (e.NewValue is IEnumerable<string> itemSource)
            autoSuggestBox.OnItemsSourceChanged(itemSource);
    }
}

