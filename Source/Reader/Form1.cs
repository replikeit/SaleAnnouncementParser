using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Parser.Core;

namespace Parser
{
    public partial class Form1 : Form
    {
        private string selectedGameName;
        private string selectedCategory;
        private string selectedServer;
        private string selectedUnderCategory1;
        private string selectedUnderCategory2;
        

        public static GameInfo[] gamesInfoArrAll;
        private static GameInfo[] gamesInfoArrSelectedCategory;
        private static GameInfo[] gamesInfoArrSelectedServer;
        private static GameInfo[] currentGameInfoArr;

        List<ComboBox> comboBoxesList = new List<ComboBox>();


        public static Dictionary<string, Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>>>
            GameCategoriesDictionary = 
                new Dictionary<string, Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>>>();

        public Form1()
        {
            InitializeComponent();
            gamesInfoArrAll = new GameInfo[0];
        }

        private void GamesWowCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem;
            if (item == null) return;

            selectedGameName = (sender as ComboBox).SelectedItem.ToString();

            selectedUnderCategory1 = null;
            selectedUnderCategory2 = null;

            gameCategoryCombo.Visible = true;
            for (int i = 0; i < comboBoxesList.Count; i++)
            {
                comboBoxesList[i].Visible = false;
            }

            gameCategoryCombo.Items.Clear();
            gameCategoryCombo.Text = "Game Category";
            foreach (var val in GameCategoriesDictionary[selectedGameName])
            {
                gameCategoryCombo.Items.Add(val.Key);
            }
        }

        private void gameCategoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem;
            if (item == null) return;

            selectedCategory = (sender as ComboBox).SelectedItem.ToString();

            selectedUnderCategory1 = null;
            selectedUnderCategory2 = null;

            gamesInfoArrSelectedCategory = gamesInfoArrAll.Where(x => x.GameName == selectedGameName && x.Category == selectedCategory).ToArray();

            if (gamesInfoArrSelectedCategory.Length < 10_000) PrintOnTableAsync(gamesInfoArrSelectedCategory);

            serverLocationCombo.Visible = true;
            for (int i = 1; i < comboBoxesList.Count; i++)
            {
                comboBoxesList[i].Visible = false;
                comboBoxesList[i].Items.Clear();
            }

            serverLocationCombo.Items.Clear();
            serverLocationCombo.Text = "Server Location";
            foreach (var val in GameCategoriesDictionary[selectedGameName][selectedCategory])
            {
                serverLocationCombo.Items.Add(val.Key);
            }

            int size = GameCategoriesDictionary[selectedGameName][selectedCategory].Count;
            if (GameCategoriesDictionary[selectedGameName][selectedCategory].ContainsKey("Any") && size == 1)
            {
                selectedServer = "Any";
                serverLocationCombo.Text = "Any";
                serverLocationCombo.SelectedItem = "Any";
                serverLocationCombo_SelectedIndexChanged(serverLocationCombo, null);
                serverLocationCombo.Visible = false;
            }
            else if (size == 0)
            {
                serverLocationCombo.Visible = false;
            }
        }

        private void serverLocationCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (sender as ComboBox).SelectedItem;
            if (item == null) return;

            selectedServer = (sender as ComboBox).SelectedItem.ToString();

            selectedUnderCategory1 = null;
            selectedUnderCategory2 = null;

            int index = 1;

            for (int i = 1; i < comboBoxesList.Count; i++)
            {
                comboBoxesList[i].Visible = false;
                comboBoxesList[i].Items.Clear();
            }

            gamesInfoArrSelectedServer = gamesInfoArrSelectedCategory.Where(x => x.Server == selectedServer).ToArray();
            PrintOnTableAsync(gamesInfoArrSelectedServer);

            if (!GameCategoriesDictionary[selectedGameName][selectedCategory].ContainsKey(selectedServer))
            {
                return;
            }

            foreach (var val1 in GameCategoriesDictionary[selectedGameName][selectedCategory][selectedServer])
            {
                foreach (var val2 in val1)
                {
                    if (val2.Value.Length == 0)
                    {
                        return;
                    }

                    comboBoxesList[index].Visible = true;
                    comboBoxesList[index].Text = val2.Key;

                    foreach (var strLine in val2.Value)
                    {
                        comboBoxesList[index].Items.Add(strLine);
                    }
                }

                index++;
            }
        }

        private void underCategoryCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            var item = (sender as ComboBox);

            if (item.SelectedItem == null) return; 

            switch (item.Tag.ToString())
            {
                case "1":
                    selectedUnderCategory1 = item.SelectedItem.ToString();
                    break;
                case "2":
                    selectedUnderCategory2 = item.SelectedItem.ToString();
                    break;
            }

            var arr = gamesInfoArrSelectedServer.
                Where(x => 
                    ((selectedUnderCategory1 == null || x.ServerName == selectedUnderCategory1) &&
                     (selectedUnderCategory2 == null || x.Turn == selectedUnderCategory2))
                    ||
                    ((selectedUnderCategory2 == null || x.ServerName == selectedUnderCategory2) && 
                     (selectedUnderCategory1 == null || x.Turn == selectedUnderCategory1)))
                .ToArray();

            PrintOnTableAsync(arr);
        }

    

        private void PrintOnTableAsync(GameInfo[] gamesArr, bool showOnline = false)
        {
            GameInfo[] arr;
            if (showOnlineCheckBox.Checked)
                arr = gamesArr.Where(x => x.IsOnline).ToArray();
            else
                arr = gamesArr;

            DataTable dt = new DataTable();
            if (arr.Length == 0)
            {
               songsDataGridView.DataSource = dt;
               return;
            }

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Date/Time", typeof(DateTime));
            dt.Columns.Add("Url                          ", typeof(string));
            dt.Columns.Add("Game Name", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("User Name", typeof(string));
            dt.Columns.Add("Reviews\nCount", typeof(string));
            dt.Columns.Add("Registered\nTime", typeof(string));
            dt.Columns.Add("Online\nStatus", typeof(string));


            bool[] isAvailableArr = new bool[5];

            var dt1 = dt.DefaultView.ToTable();
            if (arr[0].Server != "-")
            {
                dt.Columns.Add("Server", typeof(string));
                isAvailableArr[0] = true;
            }
            if (arr[0].ServerName != "-")
            {
                dt.Columns.Add("Server Name", typeof(string));
                isAvailableArr[1] = true;
            }
            if (arr[0].Turn != "-")
            {
                dt.Columns.Add("Side", typeof(string));
                isAvailableArr[2] = true;
            }
            if (arr[0].CoinsCount != -1)
            {
                dt.Columns.Add("Amout", typeof(long));
                isAvailableArr[3] = true;
            }
            if (arr[0].Description != "-")
            {
                dt.Columns.Add("Description", typeof(string));
                isAvailableArr[4] = true;

                dt.Columns.Add(arr[0].PriceTitle, typeof(double));
            }
            else
            {
                dt.Columns.Add(arr[0].PriceTitle, typeof(double));
            }


            songsDataGridView.BeginInvoke(new Action(() =>
            {
                songsDataGridView.DataSource = dt;
                dt.BeginLoadData();
                for (int i = 0; i < arr.Length; i++)
                {
                    List<object> tmpList = new List<object>();
                    tmpList.Add(arr[i].ID);
                    tmpList.Add(arr[i].Date);
                    tmpList.Add(arr[i].Url);
                    tmpList.Add(arr[i].GameName);
                    tmpList.Add(arr[i].Category);
                    tmpList.Add(arr[i].Nick);
                    tmpList.Add(arr[i].ReviewsCount);
                    tmpList.Add(arr[i].RegisteredTime);
                    tmpList.Add(arr[i].IsOnline ? "Online" : "Offline");

                    if (isAvailableArr[0])
                        tmpList.Add(arr[i].Server);
                    if (isAvailableArr[1])
                        tmpList.Add(arr[i].ServerName);
                    if (isAvailableArr[2])
                        tmpList.Add(arr[i].Turn);
                    if (isAvailableArr[3])
                        tmpList.Add(arr[i].CoinsCount);
                    if (isAvailableArr[4])
                        tmpList.Add(arr[i].Description);

                    tmpList.Add(arr[i].Cost);

                    dt.Rows.Add(tmpList.ToArray());
                }
                songsDataGridView.DataSource = dt;
                dt.EndLoadData();


                int columnsCount = 9;
                songsDataGridView.Columns[0].Name = "ID";
                songsDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                songsDataGridView.Columns[1].Name = "Date/Time";
                songsDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                songsDataGridView.Columns[2].Name = "Url                          ";
                songsDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                songsDataGridView.Columns[3].Name = "Game Name";
                songsDataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                songsDataGridView.Columns[4].Name = "Category";
                songsDataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                songsDataGridView.Columns[5].Name = "User Name";
                songsDataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                songsDataGridView.Columns[6].Name = "Reviews\nCount";
                songsDataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                songsDataGridView.Columns[7].Name = "Registered\nTime";
                songsDataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                songsDataGridView.Columns[8].Name = "Online\nStatus";
                songsDataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

                if (arr[0].Server != "-")
                {
                    songsDataGridView.Columns[columnsCount++].Name = "Server";
                    songsDataGridView.Columns[columnsCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (arr[0].ServerName != "-")
                {
                    songsDataGridView.Columns[columnsCount++].Name = "Server Name";
                    songsDataGridView.Columns[columnsCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                }
                if (arr[0].Turn != "-")
                {
                    songsDataGridView.Columns[columnsCount++].Name = "Side";

                    songsDataGridView.Columns[columnsCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    isAvailableArr[2] = true;
                }
                if (arr[0].CoinsCount != -1)
                {
                    songsDataGridView.Columns[columnsCount++].Name = "Amout";
                    songsDataGridView.Columns[columnsCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    isAvailableArr[3] = true;
                }
                if (arr[0].Description != "-")
                {
                    songsDataGridView.Columns[columnsCount++].Name = "Description";
                    songsDataGridView.Columns[columnsCount - 1].Width = 200;
                    isAvailableArr[4] = true;

                    songsDataGridView.Columns[columnsCount++].Name = arr[0].PriceTitle;
                    songsDataGridView.Columns[columnsCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                }
                else
                {
                    songsDataGridView.Columns[columnsCount++].Name = arr[0].PriceTitle;
                    songsDataGridView.Columns[columnsCount - 1].Width = 60;
                }
            }));



            if (!showOnline)
            {
                currentGameInfoArr = arr;
            }
        }

        private void showOnlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (currentGameInfoArr != null)
            {
                if ((sender as CheckBox).Checked == true)
                {
                    PrintOnTableAsync(currentGameInfoArr.Where(x => x.IsOnline).ToArray(), true);
                }
                else
                {
                    PrintOnTableAsync(currentGameInfoArr, true);
                }
            }
        }
        private static void SetData(GameInfo[] arr) => gamesInfoArrAll = arr;

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                Type dgvType = songsDataGridView.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(songsDataGridView, true, null);
            }

            using (FileStream fs = new FileStream("data.bin", FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                GameCategoriesDictionary = formatter.Deserialize(fs) as Dictionary<string, Dictionary<string, Dictionary<string, List<Dictionary<string, string[]>>>>>;
            }

            foreach (var val in GameCategoriesDictionary)
                gameNamesCombo.Items.Add(val.Key);

            comboBoxesList.Add(serverLocationCombo);
            comboBoxesList.Add(underCategoryCombo1);
            comboBoxesList.Add(underCategoryCombo2);

            ClientInfoLoader.LoadSuccess += SetData;
        }

        private void refreshDataButton_Click(object sender, EventArgs e) =>
            loadingLabel.Invoke(new Action(async () =>
            {
                loadingLabel.Visible = true;
                refreshDataButton.Enabled = false;
                var task = ClientInfoLoader.LoadPageAsync();
                while (task.Status != TaskStatus.RanToCompletion)
                {
                    loadingLabel.Text = "Loading";
                    await Task.Delay(500);
                    for (int i = 0; i < 3; i++)
                    {
                        loadingLabel.Text += ".";
                        await Task.Delay(500);
                    }
                }
                loadingLabel.Text = "Done";
                await Task.Delay(2000);
                loadingLabel.Visible = false;
                refreshDataButton.Enabled = true;
            }));


    }
}
