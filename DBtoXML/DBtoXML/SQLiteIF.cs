using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Xml.Linq;

namespace DBtoXML
{
    public class SQLiteIF
    {
        /// <summary>
        /// 駅情報一覧をXML形式で取得
        /// </summary>
        /// <param name="dbFilePath">.dbファイルのフルパス</param>
        static public string GetStations( string dbFilePath )
        {
            // 取得したレコードをListで保持します。
            // 取得したデータはすべてstring型とする。
            List<Dictionary<string, string>> stationRecords = new List<Dictionary<string, string>>();

            // DBに接続して、クエリを実行する。
            using ( var sqliteConn = new SQLiteConnection( "Data Source=" + dbFilePath ) )
            {
                sqliteConn.Open();
                using ( SQLiteCommand command = sqliteConn.CreateCommand() )
                {
                    // クエリ実行
                    command.CommandText = "SELECT * from station";
                    using ( SQLiteDataReader reader = command.ExecuteReader() )
                    {
                        // 一行一行読み込んでいく
                        while ( reader.Read() )
                        {
                            // Listに追加
                            Dictionary<string, string> record = new Dictionary<string, string>() {
                                { "stationName", reader["station_name"].ToString() },
                                { "longitude", reader["lon"].ToString() },
                                { "latitude",  reader["lat"].ToString()}
                            };
                            stationRecords.Add( record );
                        }
                    }
                }
                sqliteConn.Close();
            }

            // ルート部分のエレメントオブジェクト作成
            XElement rootElement = new XElement( "records" );
            // ルートに対して子のエレメントを追加していく
            foreach ( var record in stationRecords )
            {
                XElement el = new XElement( "record",
                    from keyValue in record
                    select new XElement( keyValue.Key, keyValue.Value )
                    );
                rootElement.Add( el );
            }
            return rootElement.ToString();
        }
    }
}
