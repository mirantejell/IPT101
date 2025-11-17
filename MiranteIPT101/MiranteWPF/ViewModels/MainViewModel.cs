using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LibraryDomain.Commands;
using LibraryDomain.Models;
using LibraryDomain.Queries;
using MiranteWPF.Commands;
using MiranteWPF.Stores;

namespace MiranteWPF.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ILibraryCommandService _commandService;
    private readonly ILibraryQueryService _queryService;
    private readonly LibraryStore _store;

    private string _fullName = string.Empty;
    private string _courseYear = string.Empty;
    private string _bookTitle = string.Empty;
    private string _searchText = string.Empty;
    private LibraryRecord? _selectedRecord;
    private bool _isEditMode;

    public MainViewModel(
        ILibraryCommandService commandService,
        ILibraryQueryService queryService,
        LibraryStore store)
    {
        _commandService = commandService;
        _queryService = queryService;
        _store = store;

        Records = new ObservableCollection<LibraryRecord>();
        
        AddCommand = new RelayCommand(async _ => await AddRecordAsync(), _ => CanAdd);
        EditCommand = new RelayCommand(_ => EditRecord(), _ => CanEdit);
        UpdateCommand = new RelayCommand(async _ => await UpdateRecordAsync(), _ => CanUpdate);
        DeleteCommand = new RelayCommand(async _ => await DeleteRecordAsync(), _ => CanDelete);
        CancelCommand = new RelayCommand(_ => CancelEdit(), _ => CanCancel);
        SearchCommand = new RelayCommand(async _ => await SearchRecordsAsync());
        ClearSearchCommand = new RelayCommand(async _ => await LoadAllRecordsAsync());

        _store.RecordsChanged += OnStoreRecordsChanged;
        
        _ = LoadAllRecordsAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<LibraryRecord> Records { get; }

    public string FullName
    {
        get => _fullName;
        set { _fullName = value; OnPropertyChanged(); }
    }

    public string CourseYear
    {
        get => _courseYear;
        set { _courseYear = value; OnPropertyChanged(); }
    }

    public string BookTitle
    {
        get => _bookTitle;
        set { _bookTitle = value; OnPropertyChanged(); }
    }

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); }
    }

    public LibraryRecord? SelectedRecord
    {
        get => _selectedRecord;
        set
        {
            _selectedRecord = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanEdit));
            OnPropertyChanged(nameof(CanDelete));
        }
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            _isEditMode = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanAdd));
            OnPropertyChanged(nameof(CanEdit));
            OnPropertyChanged(nameof(CanUpdate));
            OnPropertyChanged(nameof(CanDelete));
            OnPropertyChanged(nameof(CanCancel));
        }
    }

    public ICommand AddCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand SearchCommand { get; }
    public ICommand ClearSearchCommand { get; }

    public bool CanAdd => !IsEditMode && !string.IsNullOrWhiteSpace(FullName);
    public bool CanEdit => SelectedRecord != null && !IsEditMode;
    public bool CanUpdate => IsEditMode && !string.IsNullOrWhiteSpace(FullName);
    public bool CanDelete => SelectedRecord != null && !IsEditMode;
    public bool CanCancel => IsEditMode;

    private async Task LoadAllRecordsAsync()
    {
        try
        {
            var records = await _queryService.GetAllRecordsAsync();
            _store.SetRecords(records);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading records: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task AddRecordAsync()
    {
        try
        {
            var record = await _commandService.AddRecordAsync(FullName, CourseYear, BookTitle);
            _store.AddRecord(record);
            ClearForm();
            MessageBox.Show("Record added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void EditRecord()
    {
        if (SelectedRecord == null) return;

        FullName = SelectedRecord.FullName;
        CourseYear = SelectedRecord.CourseYear;
        BookTitle = SelectedRecord.BookTitle;
        IsEditMode = true;
    }

    private async Task UpdateRecordAsync()
    {
        if (SelectedRecord == null) return;

        try
        {
            await _commandService.UpdateRecordAsync(SelectedRecord.Id, FullName, CourseYear, BookTitle);
            
            SelectedRecord.FullName = FullName;
            SelectedRecord.CourseYear = CourseYear;
            SelectedRecord.BookTitle = BookTitle;
            
            _store.UpdateRecord(SelectedRecord);
            ClearForm();
            IsEditMode = false;
            MessageBox.Show("Record updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task DeleteRecordAsync()
    {
        if (SelectedRecord == null) return;

        var result = MessageBox.Show(
            "Are you sure you want to delete this record?",
            "Confirm Delete",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                await _commandService.DeleteRecordAsync(SelectedRecord.Id);
                _store.RemoveRecord(SelectedRecord.Id);
                ClearForm();
                MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void CancelEdit()
    {
        ClearForm();
        IsEditMode = false;
    }

    private async Task SearchRecordsAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadAllRecordsAsync();
            return;
        }

        try
        {
            var records = await _queryService.SearchRecordsAsync(SearchText);
            _store.SetRecords(records);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error searching records: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void ClearForm()
    {
        FullName = string.Empty;
        CourseYear = string.Empty;
        BookTitle = string.Empty;
        SelectedRecord = null;
    }

    private void OnStoreRecordsChanged()
    {
        Records.Clear();
        foreach (var record in _store.Records)
        {
            Records.Add(record);
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
