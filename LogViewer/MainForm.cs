using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogViewer.BinaryLog;
using BinLog = LogViewer.BinaryLog.BinaryLog;

namespace LogViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ResetProcessComboBox();
            ProcessComboBox.SelectedIndex = 0;
        }

        private List<TextLogInfo> TextLogs = new List<TextLogInfo>();
        private List<(ulong, string)> Processes = new List<(ulong, string)>();
        private List<BinaryLogInfo> BinaryLogs = new List<BinaryLogInfo>();
        private int CurrentLogIndex = -1;

        private void ResetProcessComboBox()
        {
            ProcessComboBox.Items.Clear();
            ProcessComboBox.Items.Insert(0, "All processes");
        }

        private void ClearLogs()
        {
            LogBox.Text = "";
            TextLogs.Clear();
            LogPacketGrid.SelectedObject = null;
        }

        private void ListLogs(BinLog bin_log, bool all_processes, (ulong, string) process)
        {
            ClearLogs();
            var last_got = false;
            bin_log.Packets.ForEach(log_packet =>
            {
                var process_name = "";
                var text_log_info = new TextLogInfo(0, 0, null, null);
                log_packet.Chunks.ForEach(data_chunk =>
                {
                    switch (data_chunk.Key)
                    {
                        case LogDataChunkKey.TextLog:
                            var start = LogBox.Text.Length;
                            var end = start + data_chunk.Data.Length;
                            text_log_info = new TextLogInfo(start, end, log_packet, data_chunk);
                            break;
                        case LogDataChunkKey.ProcessName:
                            process_name = data_chunk.ReadString();
                            break;
                    }
                });
                var tmp_last_got = last_got;
                last_got = false;
                if(all_processes || ((log_packet.Header.ProcessId == process.Item1)/* && (process_name == process.Item2)*/) || (tmp_last_got && !log_packet.Header.IsHead()))
                {
                    if(text_log_info.Chunk != null)
                    {
                        var text_data = text_log_info.Chunk.ReadString();
                        if (!log_packet.Header.IsHead())
                        {
                            var end_pos_extra = text_log_info.EndPosition - text_log_info.StartPosition;
                            text_log_info = TextLogs.Last();
                            TextLogs.RemoveAt(TextLogs.Count - 1);
                            text_log_info.PacketCount++;
                            text_log_info.EndPosition += end_pos_extra;
                        }
                        TextLogs.Add(text_log_info);
                        LogBox.Text += text_data.Replace("\n", "\r\n");
                        last_got = true;
                    }
                }
            });
        }

        private void ListLogsDefault(BinLog bin_log)
        {
            if (ProcessComboBox.SelectedIndex == 0) ListLogs(bin_log, true, (0, null));
            else ListLogs(bin_log, false, Processes[ProcessComboBox.SelectedIndex - 1]);
        }

        private void ProcessComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var bin_log = new BinLog();
            if (CurrentLogIndex >= 0) bin_log = BinaryLogs[CurrentLogIndex].Log;

            ListLogsDefault(bin_log);
        }

        private void LogBox_Click(object sender, EventArgs e)
        {
            if (!ClickCheckBox.Checked) return;

            LogPacketGrid.SelectedObject = null;
            var done = false;
            var pos = LogBox.SelectionStart;
            TextLogs.ForEach(text_log =>
            {
                if (!done)
                {
                    if ((text_log.StartPosition <= pos) && (text_log.EndPosition >= pos))
                    {
                        var process_name = "";
                        var thread_name = "";
                        var file_name = "";
                        uint line_number = 0;
                        var log_packet_info = new LogPacketInfo();
                        ulong timestamp = 0;
                        text_log.Packet.Chunks.ForEach(data_chunk =>
                        {
                            
                            switch(data_chunk.Key)
                            {
                                case LogDataChunkKey.FunctionName:
                                    log_packet_info.Function = data_chunk.ReadString();
                                    break;
                                case LogDataChunkKey.FileName:
                                    file_name = data_chunk.ReadString();
                                    break;
                                case LogDataChunkKey.LineNumber:
                                    line_number = data_chunk.ReadU32();
                                    break;
                                case LogDataChunkKey.ProcessName:
                                    process_name = data_chunk.ReadString();
                                    break;
                                case LogDataChunkKey.ThreadName:
                                    thread_name = data_chunk.ReadString();
                                    break;
                                case LogDataChunkKey.UserSystemClock:
                                    timestamp = data_chunk.ReadU64();
                                    break;
                            }
                        });
                        if (!string.IsNullOrEmpty(file_name) && (line_number >= 0)) log_packet_info.Location = file_name + $":{line_number}";
                        if (string.IsNullOrWhiteSpace(process_name)) log_packet_info.ProcessName = $"Process ID 0x{text_log.Packet.Header.ProcessId:X}";
                        else log_packet_info.ProcessName = process_name + $" (process ID 0x{text_log.Packet.Header.ProcessId:X})";
                        if (string.IsNullOrWhiteSpace(thread_name)) log_packet_info.ThreadName = $"Thread ID 0x{text_log.Packet.Header.ThreadId:X}";
                        else log_packet_info.ThreadName = thread_name + $" (thread ID 0x{text_log.Packet.Header.ThreadId:X})";

                        var date_time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
                        log_packet_info.Timestamp = date_time.ToString("G");
                        log_packet_info.PacketCount = text_log.PacketCount;
                        LogPacketGrid.SelectedObject = log_packet_info;
                        LogBox.SelectionStart = text_log.StartPosition;
                        LogBox.SelectionLength = text_log.EndPosition - text_log.StartPosition;
                        done = true;
                    }
                }
            });
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is used to read Nintendo binary log files.\nProgram made by XorTroll", Text);
            Process.Start("https://github.com/XorTroll");
        }

        private void ListLogProcesses(BinLog bin_log)
        {
            ResetProcessComboBox();
            Processes.Clear();
            bin_log.Packets.ForEach(log_packet =>
            {
                if (log_packet.Header.IsHead())
                {
                    var process_name = "";
                    var has_text_log = false;
                    log_packet.Chunks.ForEach(data_chunk =>
                    {
                        switch (data_chunk.Key)
                        {
                            case LogDataChunkKey.TextLog:
                                has_text_log = true;
                                break;
                            case LogDataChunkKey.ProcessName:
                                process_name = data_chunk.ReadString();
                                break;
                        }
                    });
                    if (has_text_log)
                    {
                        var process_str = $"Process ID 0x{log_packet.Header.ProcessId:X}";
                        if (!string.IsNullOrEmpty(process_name)) process_str = process_name + $" ({process_str})";
                        if (!ProcessComboBox.Items.Contains(process_str))
                        {
                            Processes.Add((log_packet.Header.ProcessId, process_name));
                            ProcessComboBox.Items.Add(process_str);
                        }
                    }
                }
            });
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Nintendo/NX Binary Log (*.nxbinlog)|*.nxbinlog";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SerialComboBox.Enabled = false;
                DateComboBox.Enabled = false;
                BinaryLogs.Clear();
                var bin_log = BinLog.LoadFrom(ofd.FileName);
                MessageBox.Show($"Binary log - version: {bin_log.Header.Version}, packet count: {bin_log.Packets.Count}");
                CurrentLogIndex = 0;
                BinaryLogs.Add(new BinaryLogInfo { SerialNumber = null, Log = bin_log, DateTime = new DateTime() });
                ListLogProcesses(bin_log);
                ProcessComboBox.SelectedIndex = 0;
            }
        }

        private void DirectoryOpenButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                SerialComboBox.Enabled = false;
                DateComboBox.Enabled = false;
                BinaryLogs.Clear();
                var any_loaded = false;
                try
                {
                    var files = Directory.GetFiles(fbd.SelectedPath);
                    if (files.Any())
                    {
                        foreach (var file in files)
                        {
                            if (Path.GetExtension(file).ToLower() == ".nxbinlog")
                            {
                                var name = Path.GetFileNameWithoutExtension(file);
                                var name_parts = name.Split('_');
                                if (name_parts.Length == 2)
                                {
                                    var serial_no = name_parts[0];
                                    var date = name_parts[1];
                                    if (date.Length == 14)
                                    {
                                        var year = int.Parse(date.Substring(0, 4));
                                        var mon = int.Parse(date.Substring(4, 2));
                                        var day = int.Parse(date.Substring(6, 2));
                                        var hour = int.Parse(date.Substring(8, 2));
                                        var min = int.Parse(date.Substring(10, 2));
                                        var sec = int.Parse(date.Substring(12, 2));

                                        var bin_log = new BinLog();
                                        try
                                        {
                                            bin_log = BinLog.LoadFrom(file);
                                        }
                                        catch
                                        {
                                            continue;
                                        }

                                        BinaryLogs.Add(new BinaryLogInfo { SerialNumber = serial_no, Log = bin_log, DateTime = new DateTime(year, mon, day, hour, min, sec) });
                                        any_loaded = true;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    any_loaded = false;
                }
                if (!any_loaded) MessageBox.Show("No logs were loaded from the selected directory...");
                else
                {
                    SerialComboBox.Enabled = true;
                    DateComboBox.Enabled = true;
                    SerialComboBox.Items.Clear();
                    DateComboBox.Items.Clear();
                    var added_serials = new List<string>();
                    BinaryLogs.ForEach(log_info =>
                    {
                        if (!added_serials.Contains(log_info.SerialNumber))
                        {
                            SerialComboBox.Items.Add(log_info.SerialNumber);
                            added_serials.Add(log_info.SerialNumber);
                        }
                    });
                    SerialComboBox.SelectedIndex = 0;
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SerialComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cur_serial = SerialComboBox.Items[SerialComboBox.SelectedIndex] as string;
            DateComboBox.Items.Clear();
            BinaryLogs.ForEach(log_info =>
            {
                if(log_info.SerialNumber == cur_serial)
                {
                    DateComboBox.Items.Add(log_info.DateTime.ToString("G"));
                }
            });
            DateComboBox.SelectedIndex = 0;
        }

        private void DateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cur_serial = SerialComboBox.Items[SerialComboBox.SelectedIndex] as string;
            var date_str = DateComboBox.Items[DateComboBox.SelectedIndex] as string;
            for (var i = 0; i < BinaryLogs.Count; i++)
            {
                var bin_log = BinaryLogs[i];
                if((bin_log.SerialNumber == cur_serial) && (bin_log.DateTime.ToString("G") == date_str))
                {
                    CurrentLogIndex = i;
                    ListLogProcesses(bin_log.Log);
                    ProcessComboBox.SelectedIndex = 0;
                    break;
                }
            }
        }
    }

    public class LogPacketInfo
    {
        [ReadOnly(true)]
        public string ProcessName { get; set; }

        [ReadOnly(true)]
        public string ThreadName { get; set; }

        [ReadOnly(true)]
        public string Timestamp { get; set; }

        [ReadOnly(true)]
        public LogSeverity Severity { get; set; }

        [ReadOnly(true)]
        public string Location { get; set; }

        [ReadOnly(true)]
        public string Function { get; set; }

        [ReadOnly(true)]
        public int PacketCount { get; set; }
    }

    public class BinaryLogInfo
    {
        public string SerialNumber { get; set; }

        public DateTime DateTime { get; set; }

        public BinLog Log { get; set; }
    }

    public class TextLogInfo
    {
        public int StartPosition { get; set; }

        public int EndPosition { get; set; }

        public int PacketCount { get; set; }

        public LogPacket Packet { get; set; }

        public LogDataChunk Chunk { get; set; }

        public TextLogInfo(int start, int end, LogPacket packet, LogDataChunk chunk)
        {
            StartPosition = start;
            EndPosition = end;
            Packet = packet;
            Chunk = chunk;
            PacketCount = 1;
        }
    }
}
