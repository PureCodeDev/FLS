using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Collections.ObjectModel;

namespace WPF
{
    public class IST //information security threat
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string InfluenceObject { get; set; }
        public string PrivacyViolation { get; set; }
        public string AccessibilityViolation { get; set; }
        public string IntegrityViolation { get; set; }
        public string IntroductionDate { get; set; }
        public string LastChangeDate { get; set; }
    }
    public class ExcelAccess
    {
        OleDbConnection Conn;//connection
        OleDbCommand Cmd;//comand

        //delegate string Violation(string a);

        public ExcelAccess(string fileName)
        {
            Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Environment.CurrentDirectory + $"\\{fileName}" + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"");
        }
        ObservableCollection<IST> source;
        string F(string str) => str == "1" ? "да" : (str == "0" ? "нет" : str);
        public async Task<ObservableCollection<IST>> GetDataFormExcelAsync()
        {
            ObservableCollection<IST> threats = new ObservableCollection<IST>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand("SELECT * FROM  [Sheet$]", Conn);

            var Reader = await Cmd.ExecuteReaderAsync();
            int i = 0;

            //Reader.Read();//костыль, т.к. не все заголовки отображает и проще скрыть верхнюю строку

            while (Reader.Read())//не все заголовки отображает, возможно из-за первой строки в excel
            {
                //f = f == -1 ? 2 : 0;
                i++;
                //int id = int.Parse(Reader[f].ToString());
                // System.Diagnostics.Trace.WriteLine(id);
                //System.Windows.MessageBox.Show(Reader.FieldCount.ToString());
                //Violation v = (string a) => a == "1" ? "да" : (a == "0" ? "нет" : a);
                threats.Add(
                    new IST()
                    {

                        Id = Reader[0].ToString(),//int.Parse(Reader[0].ToString()),
                        Name = Reader[1].ToString(),
                        Description = Reader[2].ToString(),
                        Source = Reader[3].ToString(),
                        InfluenceObject = Reader[4].ToString(),
                        PrivacyViolation = F(Reader[5].ToString()),
                        AccessibilityViolation = F(Reader[6].ToString() /*== "1" ? "да" : "нет"*/),
                        IntegrityViolation = F(Reader[7].ToString() /*== "1" ? "да" : "нет"*/),
                        IntroductionDate = Reader[8].ToString(),
                        LastChangeDate = Reader[9].ToString()

                    });
            }
            Reader.Close();
            Conn.Close();
            source = new ObservableCollection<IST>(threats);
            return threats;
        }

        public async Task<bool> InsertOrUpdateRowInExcelAsync(IST threat)
        {
            bool IsSave = false;

            if (threat.Id != "0")
            {
                await Conn.OpenAsync();
                Cmd = new OleDbCommand();
                Cmd.Connection = Conn;
                Cmd.Parameters.AddWithValue("@Id", threat.Id);
                Cmd.Parameters.AddWithValue("@Name", threat.Name);
                Cmd.Parameters.AddWithValue("@Description", threat.Description);
                Cmd.Parameters.AddWithValue("@Source", threat.Source);
                Cmd.Parameters.AddWithValue("@InfluenceObject", threat.InfluenceObject);
                Cmd.Parameters.AddWithValue("@PrivacyViolation", threat.PrivacyViolation);
                Cmd.Parameters.AddWithValue("@AccessibilityViolation", threat.AccessibilityViolation);
                Cmd.Parameters.AddWithValue("@IntegrityViolation", threat.IntegrityViolation);
                Cmd.Parameters.AddWithValue("@IntroductionDate", threat.IntroductionDate);
                Cmd.Parameters.AddWithValue("@LastChangeDate", threat.LastChangeDate);

                if (!CheckIfRecordExistAsync(threat).Result)
                {
                    Cmd.CommandText = "Insert into [Sheet$] values (@Id,@Name,@Description,@Source,@InfluenceObject,@PrivacyViolation," +
                        "@AccessibilityViolation,@IntegrityViolation,@IntroductionDate,@LastChangeDate)";
                }
                else
                {
                    //if (threat.Name != String.Empty || threat.DeptName != String.Empty)
                    //{
                    Cmd.CommandText = "Update [Sheet$] set Id=@Id,Name=@Name,Description=@Description,Source=@Source,InfluenceObject=@InfluenceObject,PrivacyViolation=@PrivacyViolation," +
                        "AccessibilityViolation=@AccessibilityViolation,IntegrityViolation=@IntegrityViolation,IntroductionDate=@IntroductionDate,LastChangeDate=@LastChangeDate where Id=@Id";
                    //}
                }
                int result = await Cmd.ExecuteNonQueryAsync();
                if (result > 0)
                {
                    IsSave = true;
                }
                Conn.Close();
            }
            return IsSave;

        }

        private async Task<bool> CheckIfRecordExistAsync(IST threat)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet$] where Id=@Id";
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }
    }

}
