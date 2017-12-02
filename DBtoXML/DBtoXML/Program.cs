using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBtoXML
{
    class Program
    {
        static void Main( string[] args )
        {
            // XMLストリームを取得
            string xmlStream = SQLiteIF.GetStations( "dbファイルのフルパスを指定" );

            try
            {
                 // XMLストリームからXElementオブジェクトを作成する場合は、
                // LoadではなくParseを使うこと。
                XElement stationsElement = XElement.Parse( xmlStream );

                // <record>要素のコレクションを取得
                IEnumerable<XElement> stations = stationsElement.Elements( "record" );
                // <record>に属している要素から、必要な情報を取得する
                foreach ( var stationElement in stations )
                {
                    Console.WriteLine( string.Format( "駅名: {0}", stationElement.Element( "stationName" ).Value ) );
                    Console.WriteLine( string.Format( "緯度: {0}", stationElement.Element( "latitude" ).Value ) );
                    Console.WriteLine( string.Format( "経度: {0}", stationElement.Element( "longitude" ).Value ) );
                    Console.WriteLine();
                }
           }
            catch ( Exception ex )
            {
                Console.WriteLine( ex.Message);
            }
        }
    }
}
