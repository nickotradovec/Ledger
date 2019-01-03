using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using AppDatabase;
using Newtonsoft.Json;

namespace Ledger.DataObjects {
    public class Account {
        public int AccountId { get; set; }
        public decimal InitialAccountBalance { get; set; }
        public DateTime Commence { get; set; }
        public DateTime Cease { get; set; }
        public string AccountName { get; set;}

        // TODO: Data formatting should not be contained here.
        // Consider a script to reformat at the end or how to do it at hte control level
        public string Commence_Formatted
        { 
            get { return Commence.ToString("dd-MMM-yyyy"); }            
        }

        public string Cease_Formatted
        { 
            get 
            { 
                if (Cease >= new DateTime(9999,12,31)) {
                    return "";
                } else {
                    return Cease.ToString("dd-MMM-yyyy"); 
                }            
            }            
        }
        
        public Boolean IsValid (DateTime dtmCheck) {
            return (dtmCheck >= Commence && dtmCheck <= Cease);
        }

        [JsonIgnore]
        private AppDb Db { get; set; }
        public Account (AppDb db = null) {
            Db = db;
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@accountId",
                DbType = DbType.Int32,
                Value = AccountId,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@accountName",
                DbType = DbType.String,
                Value = AccountName,
            });
        }
    }
}