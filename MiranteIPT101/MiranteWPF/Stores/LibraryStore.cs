using LibraryDomain.Models;

namespace MiranteWPF.Stores;

public class LibraryStore
{
    private List<LibraryRecord> _records = new();
    
    public event Action? RecordsChanged;

    public IEnumerable<LibraryRecord> Records => _records;

    public void SetRecords(IEnumerable<LibraryRecord> records)
    {
        _records = records.ToList();
        RecordsChanged?.Invoke();
    }

    public void AddRecord(LibraryRecord record)
    {
        _records.Add(record);
        RecordsChanged?.Invoke();
    }

    public void UpdateRecord(LibraryRecord record)
    {
        var index = _records.FindIndex(r => r.Id == record.Id);
        if (index >= 0)
        {
            _records[index] = record;
            RecordsChanged?.Invoke();
        }
    }

    public void RemoveRecord(int id)
    {
        _records.RemoveAll(r => r.Id == id);
        RecordsChanged?.Invoke();
    }
}
