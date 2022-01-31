using System;
using System.Data;
using System.Data.SqlClient;


namespace TR.Edu.Ankara.EUB201.Finalwork.DataAccess
{

    /// <summary>
    /// Suplies database connectivity via ado.net components
    /// </summary>
    public class AdoNetDataRepository : IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private Action<string> _logger;
        private IDbTransaction _currentTransaction;
        private bool _disposed;

        public AdoNetDataRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        private void Open()
        {
            if (this._dbConnection.State == ConnectionState.Closed || this._dbConnection.State == ConnectionState.Broken)
            {
                this._dbConnection.Open();
            }
        }
        /// <summary>
        /// Begins a new transactio scope 
        /// </summary>
        public void BeginTransaction()
        {
            Open();
            _currentTransaction = _dbConnection.BeginTransaction();
        }
        /// <summary>
        /// Finishes current transactionscope by commiting database changes
        /// </summary>
        public void CommitTransaction()
        {
            _currentTransaction.Commit();
        }
        /// <summary>
        /// executes the sql given in query parameter
        /// </summary>
        /// <param name="query"></param>
        public void Execute(string query)
        {
            Open();
            _logger?.Invoke(query);

            using (IDbCommand dbCommand = _dbConnection.CreateCommand())
            {
                dbCommand.Transaction = _currentTransaction;
                dbCommand.CommandText = query;
                
                dbCommand.ExecuteScalar();
            }
        }
        /// <summary>
        /// Execures the query given in query parameter
        /// </summary>
        /// <typeparam name="T">Type of the query result</typeparam>
        /// <param name="query">Sql query</param>
        /// <returns>An object instance of type given in T</returns>
        public T Execute<T>(string query)
        {
            Open();
            _logger?.Invoke(query);

            object item = null;
            Type type = typeof(T);
            try
            {
                if (type == typeof(DataTable) || type == typeof(DataSet))
                {
                    using (SqlCommand dbCommand = _dbConnection.CreateCommand() as SqlCommand)
                    using (SqlDataAdapter dbDataAdapter = new SqlDataAdapter())
                    {
                        DataSet dataSet = new DataSet();
                        dbDataAdapter.SelectCommand = dbCommand;
                        dbDataAdapter.SelectCommand.Transaction = _currentTransaction as SqlTransaction;
                        dbDataAdapter.SelectCommand.CommandText = query;
                        dbDataAdapter.Fill(dataSet);
                        if (type != typeof(DataTable))
                        {
                            item = dataSet;
                        }
                        else
                        {
                            item = dataSet.Tables[dataSet.Tables.Count - 1];
                            dataSet.Dispose();
                        }
                    }
                }
                else if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime))
                {
                    using (IDbCommand dbCommand1 = _dbConnection.CreateCommand())
                    {
                        dbCommand1.Transaction = _currentTransaction;
                        dbCommand1.CommandText = query;
                        object obj = dbCommand1.ExecuteScalar();
                        if (obj == null)
                        {
                            item = default(T);
                        }
                        else if (type == typeof(DateTime))
                        {
                            item = (DateTime)obj;
                        }
                        else if (type == typeof(DateTime?))
                        {
                            item = (DateTime?)obj;
                        }
                        else if (type == typeof(string))
                        {
                            item = obj.ToString();
                        }
                        else if (type == typeof(int))
                        {
                            item = Convert.ToInt32(obj);
                        }
                        else if (type == typeof(int?))
                        {
                            item = (int?)(obj as int?);
                        }
                        else if (type == typeof(uint))
                        {
                            item = Convert.ToUInt32(obj);
                        }
                        else if (type == typeof(uint?))
                        {
                            item = (uint?)(obj as uint?);
                        }
                        else if (type == typeof(long))
                        {
                            item = Convert.ToInt64(obj);
                        }
                        else if (type == typeof(long?))
                        {
                            item = (long?)(obj as long?);
                        }
                        else if (type == typeof(ulong))
                        {
                            item = Convert.ToUInt64(obj);
                        }
                        else if (type == typeof(ulong?))
                        {
                            item = (ulong?)(obj as ulong?);
                        }
                        else if (type == typeof(byte))
                        {
                            item = Convert.ToByte(obj);
                        }
                        else if (type == typeof(byte?))
                        {
                            item = (byte?)(obj as byte?);
                        }
                        else if (type == typeof(sbyte))
                        {
                            item = Convert.ToSByte(obj);
                        }
                        else if (type == typeof(sbyte?))
                        {
                            item = (sbyte?)(obj as sbyte?);
                        }
                        else if (type == typeof(decimal))
                        {
                            item = Convert.ToDecimal(obj);
                        }
                        else if (type == typeof(decimal?))
                        {
                            item = (decimal?)(obj as decimal?);
                        }
                        else if (type == typeof(double))
                        {
                            item = Convert.ToDouble(obj);
                        }
                        else if (type == typeof(double?))
                        {
                            item = (double?)(obj as double?);
                        }
                        else if (type == typeof(bool))
                        {
                            item = Convert.ToBoolean(obj);
                        }
                        else if (type == typeof(bool?))
                        {
                            item = (bool?)(obj as bool?);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Bu metod sadece string,DataTable,DataSet,DateTime veya primitive türler ile birlikte kullanılabilir");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return (T)item;
        }

        /// <summary>
        /// Defines a query log callback 
        /// </summary>
        /// <param name="logger">Action delegate</param>
        public void Log(Action<string> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Finishes a transaction scope and rollbacks all database chages
        /// </summary>
        public void RollbackTransaction()
        {
            _currentTransaction.Rollback();
        }

        #region Implementation of IDisposible pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _currentTransaction?.Dispose();
                _dbConnection?.Dispose();
            }

            _disposed = true;
        }
        #endregion End Of Implementation of IDisposible pattern
    }
}