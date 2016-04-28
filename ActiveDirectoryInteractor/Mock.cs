////interfaces

//using System.Collections;
//using System.Collections.Generic;
//using System.DirectoryServices;
//using System.Text.RegularExpressions;


//public interface IDirectoryEntryAdapter
//    {
//        IDictionary Properties { get; } //of IList
//        void CommitChanges();
//    }

//    public interface IDirectorySearcherAdapter
//    {
//        string Filter { get; set; }
//        SearchScope SearchScope { get; set; }
//        ISearchResultAdapter FindOne();
//        DirectoryEntry GetDirectoryEntry();
//    }

//    public interface IResultPropertyValueCollectionAdapter
//    {
//        string this[int index] { get; }
//        IEnumerator GetEnumerator();
//        int Count { get; }
//    }

//    public interface ISearchResultAdapter
//    {
//        IDirectoryEntryAdapter GetDirectoryEntry();
//        IDictionary Properties { get; } //of IResultPropertyValueCollectionAdapter
//    }


////concretes

//    public class DirectoryEntryAdapter : IDirectoryEntryAdapter
//    {
//        public DirectoryEntryAdapter(DirectoryEntry entry)
//        {
//            _entry = entry;
//        }

//        private DirectoryEntry _entry;

//        public IDictionary Properties //of IList
//        {
//            get { return _entry.Properties; }
//        }

//        public void CommitChanges()
//        {
//            _entry.CommitChanges();
//        }
//    }

//    public class DirectorySearcherAdapter : IDirectorySearcherAdapter, System.IDisposable
//    {
//        public DirectorySearcherAdapter(DirectorySearcher searcher)
//        {
//            _searcher = searcher;
//        }

//        public string Filter { get { return _searcher.Filter; } set { _searcher.Filter = value; } }
//        public SearchScope SearchScope { get { return _searcher.SearchScope; } set { _searcher.SearchScope = value; } }

//        public ISearchResultAdapter FindOne() { return new SearchResultAdapter(_searcher.FindOne()); }
//        public DirectoryEntry GetDirectoryEntry() { return _searcher.SearchRoot; }

//        private DirectorySearcher _searcher;

//        public void Dispose()
//        {
//            _searcher.Dispose();
//        }
//    }

//    public class ResultPropertyValueCollectionAdapter : IResultPropertyValueCollectionAdapter, IEnumerable
//    {
//        public ResultPropertyValueCollectionAdapter(ResultPropertyValueCollection input)
//        {
//            _collection = input;
//        }

//        public string this[int index]
//        {
//            get { return _collection[index].ToString(); }
//            //set { _collection[index] = value.ToString(); }
//        }

//        public IEnumerator GetEnumerator()
//        {
//            return _collection.GetEnumerator();
//        }

//        public int Count { get { return _collection.Count; } }

//        private ResultPropertyValueCollection _collection;
//    }

//    public class SearchResultAdapter : ISearchResultAdapter
//    {
//        public IDictionary Properties //of IResultPropertyValueCollectionAdapter
//        {
//            get
//            {
//                var result = new Dictionary<string, IResultPropertyValueCollectionAdapter>();
                
//                foreach (string key in _result.Properties.PropertyNames)
//                {
//                    var newItem = new ResultPropertyValueCollectionAdapter(_result.Properties[key]);
//                    result.Add(key, newItem);
//                }
//                return result;
//            }
//        }

//        public SearchResultAdapter(SearchResult result)
//        {
//            _result = result;
//        }

//        public IDirectoryEntryAdapter GetDirectoryEntry()
//        {
//            return new DirectoryEntryAdapter(_result.GetDirectoryEntry());
//        }

//        private SearchResult _result;
//    }

////mocks


//    internal class MockISearchResultAdapter : ISearchResultAdapter
//    {
//        public MockISearchResultAdapter(IEnumerable<KeyValuePair<string, IResultPropertyValueCollectionAdapter>> values, IEnumerable<KeyValuePair<string, IList>> entryProperties)
//        {
//            foreach (var value in values)
//                _properties.Add(value.Key, value.Value);

//            _entry = new MockDirectoryEntry(entryProperties);
//        }

//        public IDirectoryEntryAdapter GetDirectoryEntry()
//        {
//            return _entry;
//        }

//        public System.Collections.IDictionary Properties
//        {
//            get { return _properties; }
//        }

//        public IResultPropertyValueCollectionAdapter this[string key]
//        {
//            get { return _properties[key] as IResultPropertyValueCollectionAdapter; }
//        }

//        public void AddGroups(string[] groups)
//        {
//            _properties.Add(Group.GROUPS, new MockIResultPropertyValueCollectionAdapter( groups ));
//        }

//        private readonly IDictionary _properties = new Dictionary<string, IResultPropertyValueCollectionAdapter>();
//        private readonly MockDirectoryEntry _entry;
//    }

//    internal class MockIResultPropertyValueCollectionAdapter : IResultPropertyValueCollectionAdapter
//    {
//        public MockIResultPropertyValueCollectionAdapter(string[] values)
//        {
//            _data = values;
//        }

//        public string this[int index]
//        {
//            get { return _data[index].ToString(); }
//            set { _data[index] = value; }
//        }

//        public IEnumerator GetEnumerator()
//        {
//            return _data.GetEnumerator();
//        }

//        public int Count
//        {
//            get { return _data.Count(); }
//        }

//        private readonly object[] _data;

//    }

//    internal class MockDirectoryEntry : IDirectoryEntryAdapter
//    {
//        public MockDirectoryEntry(IEnumerable<KeyValuePair<string,IList>> entryProperties)
//        {
//            foreach (var item in entryProperties)
//                _properties.Add(item.Key, item.Value);
//        }

//        public IDictionary Properties
//        {
//            get { return _properties; }
//        }

//        public void CommitChanges()
//        {
//            ;
//        }

//        private readonly IDictionary _properties = new Dictionary<string, IList>();

//    }