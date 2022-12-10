using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace OneRiskServer.Services
{
    public class Columns
    {
        public string[] ColumnList;

        public Columns()
        {
            ColumnList = new string[10];

            ColumnList[0] = "P1";
            ColumnList[1] = "P2";
            ColumnList[2] = "P3";
            ColumnList[3] = "P4";
            ColumnList[4] = "P5";
            ColumnList[5] = "P6";
            ColumnList[6] = "P7";
            ColumnList[7] = "P8";
            ColumnList[8] = "P9";
            ColumnList[9] = "P10";
        }
    }

    public class DataStore
    {
        private const int RowCount = 10;
        private Columns _columns;
        private DataTable _table;
        private DataColumn _column;
        private DataRow _row;

        public DataStore()
        {
            _columns = new Columns();
            _table = new DataTable();
            AddColumn("D1", System.Type.GetType("System.Double"));

            for(var i = 0; i < _columns.ColumnList.Length; i++)
            {
                AddColumn(_columns.ColumnList[i]);
            }

            AddRows();
        }

        public int RowCount2
        {
            get { return _table.Rows.Count; }
        }

        public Columns Columns
        {
            get { return _columns; }
        }
            

        public DataRow this[int idx]
        {
            get { return _table.Rows[idx]; }
        }


        private void AddRows()
        {
            for (var i = 0; i < RowCount; i++)
            {
                _row = _table.NewRow();
                PopulateRow();
            }
        }

        private void PopulateRow()
        {
            _row["D1"] = GetDouble();
            for (var i = 0; i < _columns.ColumnList.Length; i++)
            {
                _row[_columns.ColumnList[i]] = GetString();
            }
        }

        private void AddColumn(string name, Type type = null)
        {
            // var typ = type ?? System.Type.GetType("System.Int32");
            var typ = type ?? System.Type.GetType("System.String");

            _column = new DataColumn();
            _column.DataType = typ;
            _column.ColumnName = name;
            // column.ReadOnly = true;
            // column.Unique = true;
            // Add the Column to the DataColumnCollection.
            _table.Columns.Add(_column);
        }

        static Random rd = new Random();
        internal static string GetString(int stringLength = 100)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        static Random random = new Random();
        public double GetDouble(double minimum = -100000000d, double maximum = 100000000d)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}