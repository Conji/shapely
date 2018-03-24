using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shapely.Core
{
    /// <summary>
    ///     The base object all objects that interact with a database must inherit from.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BusinessBase<T> where T : BusinessBase<T>, new()
    {
        #region Private Fields

        private static Dictionary<Guid, T> m_Store;
        private static T m_FakeInstance = new T();
        private static object m_SyncLock = new object();

        #endregion

        #region Public Properties

        /// <summary>
        ///     The unique identifier of the object.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The event handler called when a property is changed with <see cref="SetValue{TValue}(TValue, ref TValue, string)"/>.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        /// <summary>
        ///     The public local store of the object. If the store doesn't exist or is null, it will call <see cref="FillStore"/>.
        /// </summary>
        public static Dictionary<Guid, T> Store
        {
            get
            {
                if (m_Store == null)
                {
                    lock (m_SyncLock)
                    {
                        m_Store = m_FakeInstance.FillStore().ToDictionary(kvp => kvp.Id);
                    }
                }
                return m_Store;
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        ///     Whether or not the object is new. To change this, call <see cref="MarkOld"/>.
        /// </summary>
        protected bool IsNew { get; set; }
        /// <summary>
        ///     Whether or not the object is marked for deletion. To change this, call <see cref="MarkForDeletion"/>.
        /// </summary>
        protected bool IsDeleted { get; set; }
        /// <summary>
        ///     Whether or not the object is changed. This is set automatically through <see cref="SetValue{TValue}(TValue, ref TValue, string)"/>
        /// </summary>
        protected bool IsChanged { get; private set; }

        #endregion

        #region .ctor

        protected BusinessBase()
        {
            IsNew = true;
            IsDeleted = false;
            IsChanged = false;
            Id = Guid.NewGuid();

        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Marks the object as pre-existing, typically from a database service.
        /// </summary>
        public void MarkOld()
        {
            IsNew = false;
        }

        /// <summary>
        ///     Marks the object for deletion on next save.
        /// </summary>
        public void MarkForDeletion()
        {
            IsDeleted = true;
            IsChanged = true;
        }

        /// <summary>
        ///     Saves any changes or deletes of the object. If no changes were made, then nothing happens.
        /// </summary>
        public void Save()
        {
            if (!IsChanged) return; // so we don't make unnecessary db calls
            if (IsDeleted)
            {
                DataDelete();
            }
            else if (IsNew)
            {
                DataInsert();
            }
            else
            {
                DataUpdate();
            }
        }

        /// <summary>
        ///     Call to execute the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static T GetById(Guid id)
        {
            var result = Store.ContainsKey(id) ? Store[id] : null;
            if (result == null)
            {
                result = m_FakeInstance.DataSelect(id);
                if (result != null) Store.Add(result.Id, result);
            }
            return result;
        }

        #endregion

        #region Protected Methods

        protected void SetValue<TValue>(TValue value, ref TValue field, [CallerMemberName]string propertyName = "")
        {
            if (value.Equals(field)) return;
            field = value;
            OnPropertyChanged(propertyName);
            IsChanged = true;
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        ///     Is called to fill local data store. This method must access a database service.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<T> FillStore();

        /// <summary>
        ///     Is called when a requested object is not found with a matching ID. This method must access a database service.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected abstract T DataSelect(Guid id);

        /// <summary>
        ///     Is called when saving and this object is new. Must access a database service.
        /// </summary>
        /// <param name="id"></param>
        protected abstract void DataInsert();

        /// <summary>
        ///     Is called when saving and this object is old. Must access a database service.
        /// </summary>
        /// <param name="id"></param>
        protected abstract void DataUpdate();

        /// <summary>
        ///     Is called when saving and this object is marked as deleted. Must access a database service.
        /// </summary>
        /// <param name="id"></param>
        protected abstract void DataDelete();

        #endregion
    }
}
