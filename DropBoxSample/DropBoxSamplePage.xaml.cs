
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace DropBoxSample
{
    public partial class DropBoxSamplePage : ContentPage
    {

        private static string _GeneratedAccessToken = "Input Generated Access Token";

        #region 接続確認
        /// <summary>
        /// DropBox接続確認
        /// </summary>
        /// <returns>The confirm.</returns>
        private async Task Confirm()
        {
            using (var dbx = new DropboxClient(_GeneratedAccessToken))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                System.Diagnostics.Debug.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
            }
        }
		#endregion


		#region アップロード
		/// <summary>
		/// DropBoxアップロード
		/// </summary>
		/// <returns>The confirm.</returns>
		private async Task Upload()
		{
            string folderName = "datas";
			string fileName = "otameshi.txt";
			string content = entText.Text;

			using (var dbx = new DropboxClient(_GeneratedAccessToken))
			{
               
                using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
				{
					var updated = await dbx.Files.UploadAsync(
						"/" + folderName + "/"  + fileName,
						WriteMode.Overwrite.Instance,
						body: mem);
					System.Diagnostics.Debug.WriteLine("Saved {0} rev {1}", fileName, updated.Rev);
				}
			}
		}
        #endregion

        public DropBoxSamplePage()
        {
            InitializeComponent();


            buttonOK.Clicked += ButtonOK_Clicked;
            buttonConfim.Clicked += ButtonConfim_Clicked;
        }

		void ButtonConfim_Clicked(object sender, EventArgs e)
		{
            var task = Task.Run((Func<Task>)Confirm);
			task.Wait();
		}

		void ButtonOK_Clicked(object sender, System.EventArgs e)
		{
			var task = Task.Run((Func<Task>)Upload);
			task.Wait();
		}
    } 
}