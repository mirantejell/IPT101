using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;



namespace MiranteWpfApp
{
    public class LoanRecord : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string fullName = "";
        public string FullName
        {
            get => fullName;
            set { if (fullName != value) { fullName = value; OnPropertyChanged(nameof(FullName)); } }
        }

        private string courseYear = "";
        public string CourseYear
        {
            get => courseYear;
            set { if (courseYear != value) { courseYear = value; OnPropertyChanged(nameof(CourseYear)); } }
        }

        private string bookTitle = "";
        public string BookTitle
        {
            get => bookTitle;
            set { if (bookTitle != value) { bookTitle = value; OnPropertyChanged(nameof(BookTitle)); } }
        }

        public override string ToString() => $"{FullName} — {BookTitle}";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
namespace MiranteWpfApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<LoanRecord> Records = new ObservableCollection<LoanRecord>();
        private LoanRecord? editingRecord = null;
        private readonly ICollectionView recordsView;

        public MainWindow()
        {
            InitializeComponent();

            recordsView = CollectionViewSource.GetDefaultView(Records);
            lstRecords.ItemsSource = recordsView;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var full = txtFullName.Text.Trim();
            var course = txtCourseYear.Text.Trim();
            var book = txtBookTitle.Text.Trim();

            if (string.IsNullOrEmpty(full) || string.IsNullOrEmpty(book))
            {
                MessageBox.Show("Full Name and Book Title are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var rec = new LoanRecord
            {
                Id = GenerateId(),
                FullName = full,
                CourseYear = course,
                BookTitle = book
            };

            Records.Add(rec);
            ClearInputs();
        }

        private void LstRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sel = lstRecords.SelectedItem as LoanRecord;
            bool has = sel != null;
            btnEdit.IsEnabled = has;
            btnDelete.IsEnabled = has;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e) => StartEditing();

        private void StartEditing()
        {
            var sel = lstRecords.SelectedItem as LoanRecord;
            if (sel == null) return;

            editingRecord = sel;
            txtFullName.Text = sel.FullName;
            txtCourseYear.Text = sel.CourseYear;
            txtBookTitle.Text = sel.BookTitle;

            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnUpdate.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnDelete.IsEnabled = false;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (editingRecord == null) return;

            var full = txtFullName.Text.Trim();
            var course = txtCourseYear.Text.Trim();
            var book = txtBookTitle.Text.Trim();

            if (string.IsNullOrEmpty(full) || string.IsNullOrEmpty(book))
            {
                MessageBox.Show("Full Name and Book Title are required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            editingRecord.FullName = full;
            editingRecord.CourseYear = course;
            editingRecord.BookTitle = book;

            // notify view and reset state
            recordsView.Refresh();
            editingRecord = null;
            ClearInputs();
            ResetButtons();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = lstRecords.SelectedItem as LoanRecord;
            if (sel == null) return;

            var res = MessageBox.Show($"Delete record for \"{sel.FullName}\" (\"{sel.BookTitle}\")?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Records.Remove(sel);
                ClearInputs();
                ResetButtons();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            editingRecord = null;
            ClearInputs();
            ResetButtons();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var q = (txtSearch.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(q))
            {
                recordsView.Filter = null;
            }
            else
            {
                recordsView.Filter = obj =>
                {
                    if (obj is LoanRecord r)
                    {
                        return r.FullName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                               || r.BookTitle.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0
                               || r.CourseYear.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    return false;
                };
            }
        }

        private void BtnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Clear();
            recordsView.Filter = null;
        }

        private void ClearInputs()
        {
            txtFullName.Clear();
            txtCourseYear.Clear();
            txtBookTitle.Clear();
            txtFullName.Focus();
        }

        private void ResetButtons()
        {
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = lstRecords.SelectedItem != null;
            btnUpdate.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnDelete.IsEnabled = lstRecords.SelectedItem != null;
        }

        private int GenerateId() => Records.Any() ? Records.Max(r => r.Id) + 1 : 1;
    }
}