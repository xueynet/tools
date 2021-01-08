using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Windows;

namespace ServerIpConfig
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string jsonPath = "app.json";

    #region 通用类

        /// <summary>
        /// 格式化JSON字符串
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>输出字符串</returns>
        public static string FormatJsonStr(string str)
        {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 读取JSON文件
        /// </summary>
        /// <param name="jsonPath">json文件路径</param>
        /// <returns>json字符串</returns>
        public static string ReadJsonString(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                Error("配置文件不存在：" + jsonPath);
                return string.Empty;
            }
            return File.ReadAllText(jsonPath, Encoding.Default);
        }

        /// <summary>
        ///读取JSON文件
        /// </summary>
        /// <param name="jsonPath">json文件路径</param>
        /// <returns>JObject对象</returns>
        public static JObject ReadJsonObj(string jsonPath)
        {
            string json = ReadJsonString(jsonPath);
            JObject jsonObj = null;
            if (!string.IsNullOrEmpty(json))
            {
                jsonObj = (JObject)JsonConvert.DeserializeObject(json);
            }
            return jsonObj;
        }

        #region 写入JSON
        /// <summary>
        /// 写入JSON
        /// </summary>
        /// <returns></returns>
        public static bool Write(string jsonStr, string jsonPath)
        {
            try
            {
                System.IO.File.WriteAllText(jsonPath, jsonStr, Encoding.Default);
                return true;
            }
            catch (System.Exception ex)
            {
                Error("保存结果异常" + ex.Message + ex.StackTrace);
                return false;
            }

        }
        #endregion

        public static void Error(string msg)
        {
            
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();

        }
    }
}
