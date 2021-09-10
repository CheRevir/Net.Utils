using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Utils
{
    /// <summary>
    /// 读/写配置文件
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// 文件目录
        /// </summary>
        public readonly string FileDirectory;
        /// <summary>
        /// 文件名称
        /// </summary>
        public readonly string FileName;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get
            {
                return Path.Combine(this.FileDirectory, this.FileName);
            }
        }
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returned, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string value, string filePath);

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public IniFile(string fileName) : this(AppDomain.CurrentDomain.BaseDirectory, fileName) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fileDirectory">文件目录</param>
        /// <param name="fileName">文件名称</param>
        public IniFile(string fileDirectory, string fileName)
        {
            if (String.IsNullOrEmpty(fileDirectory))
            {
                throw new ArgumentNullException("fileDirectory不能为空");
            }
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName不能为空");
            }
            this.FileDirectory = fileDirectory;
            this.FileName = fileName;

            if (!Directory.Exists(this.FileDirectory))
            {
                Directory.CreateDirectory(this.FileDirectory);
            }
        }

        /// <summary>
        /// 读取一个值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">Key</param>
        /// <param name="def">默认值</param>
        /// <returns>值</returns>
        public int ReadInt(string section, string key, int def)
        {
            return GetPrivateProfileInt(section, key, def, this.FilePath);
        }

        /// <summary>
        /// 读取一个值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">Key</param>
        /// <param name="def">默认值</param>
        /// <returns>值</returns>
        public string ReadString(string section, string key, string def)
        {
            StringBuilder sb = new StringBuilder(2048);
            GetPrivateProfileString(section, key, def, sb, 2048, this.FilePath);
            return sb.ToString();
        }

        /// <summary>
        /// 写入一个值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">Key</param>
        /// <param name="value">值</param>
        /// <returns>是否写入成功</returns>
        public bool Write(string section, string key, object value)
        {
            return WritePrivateProfileString(section, key, value.ToString(), this.FilePath) != 0;
        }

        /// <summary>
        /// 删除指定Key
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">Key</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteKey(string section, string key)
        {
            return Write(section, key, null);
        }

        /// <summary>
        /// 删除指定节
        /// </summary>
        /// <param name="section">节</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteSection(string section)
        {
            return Write(section, null, null);
        }

        /// <summary>
        /// 删除所有节
        /// </summary>
        /// <returns>是否删除成功</returns>
        public bool DeleteAllSection()
        {
            return Write(null, null, null);
        }
    }
}