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
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
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

        public ExcelAccess()
        {
            Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ Environment.CurrentDirectory + "\\thrlist.xlsx" + ";" + "Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"");
        }

        public async Task<ObservableCollection<IST>> GetDataFormExcelAsync()
        {
            ObservableCollection<IST> threats = new ObservableCollection<IST>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand("Select * from [Sheet$]", Conn);

            var Reader = await Cmd.ExecuteReaderAsync();
            int i = 0;
            while (Reader.Read())
            {
                i++;
                threats.Add(
                    new IST()
                    {
                        Id = i,
                        Name = Reader[1].ToString(),
                        Description = Reader[2].ToString(),
                        Source = Reader[3].ToString(),
                        PrivacyViolation = (Reader[4].ToString() /*== "1" ? "да" : "нет"*/),
                        AccessibilityViolation = (Reader[5].ToString() /*== "1" ? "да" : "нет"*/),
                        IntegrityViolation = (Reader[6].ToString() /*== "1"?"да":"нет"*/),
                        IntroductionDate = Reader[7].ToString(),
                        LastChangeDate = Reader[8].ToString()

                    });
            }
            Reader.Close();
            Conn.Close();
            return threats;
        }

        public async Task<bool> InsertOrUpdateRowInExcelAsync(IST threat)
        {
            bool IsSave = false;

            if (threat.Id != 0)
            {
                await Conn.OpenAsync();
                Cmd = new OleDbCommand();
                Cmd.Connection = Conn;
                Cmd.Parameters.AddWithValue("@Id", threat.Id);
                Cmd.Parameters.AddWithValue("@Name", threat.Name);
                Cmd.Parameters.AddWithValue("@Description", threat.Description);
                Cmd.Parameters.AddWithValue("@Source", threat.Source);
                Cmd.Parameters.AddWithValue("@PrivacyViolation", threat.PrivacyViolation);
                Cmd.Parameters.AddWithValue("@AccessibilityViolation", threat.AccessibilityViolation);
                Cmd.Parameters.AddWithValue("@IntegrityViolation", threat.IntegrityViolation);
                Cmd.Parameters.AddWithValue("@IntroductionDate", threat.IntroductionDate);
                Cmd.Parameters.AddWithValue("@LastChangeDate", threat.LastChangeDate);

                if (!CheckIfRecordExistAsync(threat).Result)
                {
                    Cmd.CommandText = "Insert into [Sheet1$] values (@Id,@Name,@Description,@Source,@PrivacyViolation," +
                        "@AccessibilityViolation,@IntegrityViolation,@IntroductionDate,@LastChangeDate)";
                }
                else
                {
                    //if (threat.Name != String.Empty || threat.DeptName != String.Empty)
                    //{
                    Cmd.CommandText = "Update [Sheet1$] set EmpNo=@EmpNo,EmpName=@EmpName,Salary=@Salary,DeptName=@DeptName where EmpNo=@EmpNo";
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
            Cmd.CommandText = "Select * from [Sheet1$] where Id=@Id";
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
